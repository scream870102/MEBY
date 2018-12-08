using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }
	[SerializeField]
	protected Animator animator;

	// Use this for initialization
	public void Start ( ) {
		if (parent != null) {
			
		}
	}

	// Update is called once per frame
	void Update ( ) {
		Render();
		switch (parent.PlayerState)
		{
			case EPlayerState.IDLE:
				animator.SetBool("Jump",false);
				break;
			case EPlayerState.WALK:
				break;
			case EPlayerState.JUMP:
				animator.SetBool("Jump",true);
				break;
			case EPlayerState.ATTACK:
				break;
			case EPlayerState.SKILL:
				break;	
			case EPlayerState.PAINTBALL_USING:
				break;	
			default:
				break;
		}
	}

	protected void Render ( ) {
		Vector3 temp = transform.localScale;
		temp.x = parent.IsPlayerFacingRight?-Mathf.Abs (temp.x): Mathf.Abs (temp.x);
		transform.localScale = temp;

	}
}
