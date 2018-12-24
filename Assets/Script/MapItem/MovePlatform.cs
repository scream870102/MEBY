using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MovePlatform : MonoBehaviour {
	public Vector2 moveRange;
	public float velocity;
	protected Vector2 targetPos;
	protected Vector2 startPos;
	protected bool bChangeDirection;
	protected Vector2 direction;
	protected Rigidbody2D rb;
	// Use this for initialization
	void Start ( ) {
		rb=GetComponent<Rigidbody2D>();
		bChangeDirection = false;
		startPos = rb.position;
		targetPos = rb.position + new Vector2 (moveRange.x, moveRange.y);
		direction=targetPos-startPos.normalized;
	}
	// Update is called once per frame
	void FixedUpdate ( ) {
		rb.velocity=(bChangeDirection?-direction:direction)*velocity*Time.fixedDeltaTime;
		if (transform.position.x >= targetPos.x)
			bChangeDirection = true;
		else if (transform.position.x <= startPos.x)
			bChangeDirection = false;
	}
}
