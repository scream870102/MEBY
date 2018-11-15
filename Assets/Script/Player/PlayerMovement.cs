using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//class define how player move
public class PlayerMovement : MonoBehaviour {
	//
	//
	//drag ref
	//
	//define which layer is ground
	public List<LayerMask> groundLayer = new List<LayerMask> ( );
	//
	//
	//const
	//
	//damp for move
	[SerializeField]
	private float smoothDamp = .05f;
	//ref velocity for smooth damp
	private Vector2 refVelocity = Vector2.zero;

	//
	//ref const
	//these const get from parent Props
	protected float basicSpeed;
	protected float airSpeed;
	protected int numMaxJump;
	protected float jumpForce;
	protected float groundRadius;
	private bool isAirControl;

	//
	//referrence
	protected IPlayer parent = null;
	protected Rigidbody2D rb = null;
	//transform for foot
	public Transform detectGround = null;
	public IPlayer Parent {
		set { if (parent == null) parent = value; }

	}
	//
	//
	//field
	//
	//bool for jumping state
	protected bool bJump;
	//bool for on ground
	protected bool bGround;
	//store current jump time
	protected int numNowJump;
	//store horizontal velocity
	protected float moveHorizontal;
	//stoer what direction is player facing true=facing right direction false=facing left direction
	[SerializeField]
	protected bool bFacingRight;
	//set all info from props
	//set detectGround
	public void Start ( ) {
		if (parent != null) {
			basicSpeed = parent.Props.basicSpeed;
			airSpeed = parent.Props.airSpeed;
			numMaxJump = parent.Props.numMaxJump;
			jumpForce = parent.Props.jumpForce;
			groundRadius = parent.Props.groundRadius;
			isAirControl = parent.Props.isAirControl;
			numNowJump = 0;
			rb = GetComponent<Rigidbody2D> ( );
			detectGround = transform.Find ("Foot");
		}
	}

	void Update ( ) {
		//if player can controll get horizontal move
		if (isAirControl || bGround)
			moveHorizontal = Input.GetAxisRaw (parent.NumPlayer + "Horizontal") * (bGround?basicSpeed : airSpeed);
		//if player hit jump button call jump method
		if (Input.GetButtonDown (parent.NumPlayer + "Jump")) {
			Jump ( );
		}
		//Render player animation
		//include what direction is player facing
		Render ( );
	}

	//keep detect ground and call Move and Jump function 
	protected virtual void FixedUpdate ( ) {
		IsGrounded ( );
		Move ( );
		InJump ( );
	}

	//if can jump set bJump to true bGround false plus numNowJump
	protected virtual void Jump ( ) {
		if (numNowJump < numMaxJump) {
			bJump = true;
			bGround = false;
			numNowJump++;
		}
	}

	//get horiziontal velocity and move rigidbody call in fixed update
	protected virtual void Move ( ) {
		if (moveHorizontal > 0)
			bFacingRight = true;
		else if (moveHorizontal < 0)
			bFacingRight = false;
		Vector2 targetVelocity = new Vector2 (moveHorizontal * Time.fixedDeltaTime, rb.velocity.y);
		rb.velocity = Vector2.SmoothDamp (rb.velocity, targetVelocity, ref refVelocity, smoothDamp);
	}

	//if can jump add force to rigidbody  call in fixed update
	protected virtual void InJump ( ) {
		if (bJump) {
			rb.AddForce (new Vector2 (0f, jumpForce));
			bJump = false;
		}
	}

	//keep detect if player is on ground if true set numOnojump to zero bGround = true
	protected void IsGrounded ( ) {
		if (detectGround != null) {
			foreach (LayerMask layer in groundLayer) {
				Collider2D [ ] colliders = Physics2D.OverlapCircleAll (detectGround.position, groundRadius, layer);
				foreach (Collider2D collider in colliders) {
					if (collider != gameObject) {
						numNowJump = 0;
						bGround = true;
					}
					else
						bGround = false;
				}
			}
		}
	}

	//change player scale if player face different direction
	protected void Render ( ) {
		Vector3 temp = transform.localScale;
		temp.x = bFacingRight?Mathf.Abs (temp.x): -Mathf.Abs (temp.x);
		transform.localScale = temp;

	}

	//public method for other class to get Player state
	public bool IsPlayerFacingRight ( ) {
		return bFacingRight;
	}

}
