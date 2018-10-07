using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	//const
	protected float basicSpeed;
	protected int numMaxJump;
	protected int numNowJump;
	protected float jumpForce;
	//referrence
	protected IPlayer parent = null;
	protected Rigidbody2D rb = null;
	public IPlayer Parent {
		set { if (parent == null) parent = value; }
	}

	public DetectGround detectGround = null;
	//field
	private Vector2 velocity = new Vector2 ( );

	public void Start ( ) {
		if (parent != null) {
			basicSpeed = parent.Props.basicSpeed;
			numMaxJump = parent.Props.numMaxJump;
			jumpForce = parent.Props.jumpForce;
			numNowJump = 0;
			rb = GetComponent<Rigidbody2D> ( );

		}

	}

	void Update ( ) {
		float moveHorizontal = Input.GetAxis (parent.NumPlayer + "Horizontal");
		if (!IsJumping ( ))
			velocity.x = moveHorizontal;
		if (Input.GetButtonDown (parent.NumPlayer + "Jump"))
			Jump ( );

	}

	protected virtual void Jump ( ) {
		if (numNowJump >= numMaxJump) {
			if (IsJumping ( ))
				return;
			else if (!IsJumping ( ))
				numNowJump = 0;
		}
		velocity.x = 0.0f;
		velocity.y = jumpForce;
		numNowJump++;
	}

	protected virtual void FixedUpdate ( ) {
		if (rb != null) {
			rb.AddForce (velocity * basicSpeed);
			velocity.y = 0.0f;
			//velocity=new Vector2(0.0f,0.0f);
			//rb.AddForce(velocity);
		}

	}

	protected bool IsJumping ( ) {
		if (detectGround != null) {
			return !detectGround.IsGround;
		}
		else if (detectGround == null) {
			if (parent.FootObject != null)
				detectGround = parent.FootObject.GetComponent<DetectGround> ( );
		}
		return true;

	}

}
