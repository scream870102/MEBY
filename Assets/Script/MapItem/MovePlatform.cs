using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MovePlatform : MonoBehaviour {
	public Vector2 moveRange;
	public float velocity;
	protected Vector3 targetPos;
	protected Vector3 startPos;
	protected bool bChangeDirection;
	// Use this for initialization
	void Start ( ) {
		bChangeDirection = false;
		startPos = transform.position;
		targetPos = transform.position + new Vector3 (moveRange.x, moveRange.y, 0.0f);
	}

	// Update is called once per frame
	void Update ( ) {
		float step = velocity * Time.deltaTime;
		if (transform.position == targetPos)
			bChangeDirection = true;
		else if (transform.position == startPos)
			bChangeDirection = false;
		if (!bChangeDirection)
			transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
		else
			transform.position = Vector3.MoveTowards (transform.position, startPos, step);
	}
}
