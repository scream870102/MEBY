using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	//
	//
	//drag ref
	public LayerMask groundLayer;
	//
	//
	//const
	[SerializeField]
	private float smoothDamp = .05f;
	private Vector2 refVelocity = Vector2.zero;

	//
	//ref const
	protected float basicSpeed;
	protected float airSpeed;
	protected int numMaxJump;
	protected float jumpForce;
	protected float groundRadius = .2f;
	private bool isAirControl;
	//
	//referrence
	protected IPlayer parent = null;
	protected Rigidbody2D rb = null;
	public Transform detectGround = null;
	public IPlayer Parent {
		set { if (parent == null) parent = value; }
	}
	//
	//
	//field
	protected bool isJump = false;
	protected bool isGround = false;
	protected int numNowJump;
	protected float moveHorizontal;

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
		Debug.Log (isGround);
		if (isAirControl || isGround)
			moveHorizontal = Input.GetAxisRaw (parent.NumPlayer + "Horizontal") * (isGround?basicSpeed : airSpeed);
		if (Input.GetButtonDown (parent.NumPlayer + "Jump")) {
			Jump ( );
		}

	}

	protected virtual void Jump ( ) {
		if (numNowJump < numMaxJump) {
			isJump = true;
			isGround = false;
			numNowJump++;
		}

	}
	protected virtual void Move ( ) {
		Vector2 targetVelocity = new Vector2 (moveHorizontal * Time.fixedDeltaTime, rb.velocity.y);
		rb.velocity = Vector2.SmoothDamp (rb.velocity, targetVelocity, ref refVelocity, smoothDamp);
	}

	protected virtual void InJump ( ) {
		if (isJump) {
			rb.AddForce (new Vector2 (0f, jumpForce));
			isJump = false;
		}
	}

	protected virtual void FixedUpdate ( ) {
		IsGrounded ( );
		if (rb != null) {
			Move ( );
			InJump ( );
		}

	}

	protected void IsGrounded ( ) {
		if (detectGround != null) {
			Collider2D [ ] colliders = Physics2D.OverlapCircleAll (detectGround.position, groundRadius, groundLayer);
			foreach (Collider2D collider in colliders) {
				if (collider != gameObject) {
					numNowJump = 0;
					isGround = true;
				}
				else
					isGround = false;
			}
		}
	}

}
