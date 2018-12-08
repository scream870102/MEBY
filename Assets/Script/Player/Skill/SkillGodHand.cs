using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public enum GOD_HAND_STATE {
	UNUSE,
	USING,
	GRAB,
	END
}

[System.Serializable]
public enum DIRECTION {
	NONE,
	LEFT = -1,
	RIGHT = 1
}

//skill god hand is the personal skill for MORAL_DEVIL
//this class inherit from ISkill
public class SkillGodHand : ISkill {
	//ref for god hand object
	public GameObject handObject;
	//define how long will target player stop its action
	public float stopActionTime;
	//define how fast does hand move
	public Vector2 velocity;
	//define what is minest distance between player and target
	public float minmusDistance;
	//ref hand object rigidbody
	protected Rigidbody2D rb;
	//ref hand object collider
	protected Collider2D col;
	//field for god hand switch its state
	protected GOD_HAND_STATE state;
	//timer for hand action
	protected float actionTimer;
	//field to save the target player	
	protected Collider2D target = null;
	//field to define what direction should hand move
	protected DIRECTION direction = DIRECTION.NONE;

	protected override void Start ( ) {
		//include init isActive and timer and isInCD
		base.Start ( );
		//Get skill coolDown time Number from GameManager instance attribution
		coolDown = GameManager.instance.attribution.allSkillProps [(int) ESkill.GOD_HAND].skillCD;
		actionTime = GameManager.instance.attribution.allSkillProps [(int) ESkill.GOD_HAND].skillActionTime;
		//Init state to Unuse
		state = GOD_HAND_STATE.UNUSE;
		//Get ref for handObject and disable it
		rb = handObject.GetComponent<Rigidbody2D> ( );
		col = handObject.GetComponent<Collider2D> ( );
		handObject.SetActive (false);
		//reset Action timer
		actionTimer = 0.0f;
	}

	//override useSkill
	protected override void UseSkill ( ) {
		switch (state) {
			case GOD_HAND_STATE.UNUSE:
				//if player use skill enable hand object and reset timer and direction then set state to using
				if (Input.GetButtonDown (Parent.NumPlayer + buttonString)) {
					state = GOD_HAND_STATE.USING;
					handObject.transform.position = transform.position;
					handObject.transform.parent = null;
					handObject.SetActive (true);
					actionTimer = 0.0f;
					direction = DIRECTION.NONE;
				}
				break;

			case GOD_HAND_STATE.USING:
				//set direction first time
				if (direction == DIRECTION.NONE)
					direction = Parent.IsPlayerFacingRight ? DIRECTION.RIGHT : DIRECTION.LEFT;
				//disable player move
				Parent.SetMovement (false);
				//keep accumulation timer
				actionTimer += Time.deltaTime;
				//move the hand object
				rb.MovePosition (rb.position + (int) direction * velocity * Time.deltaTime);
				//if actionTimer>= action time set State to end and set direction
				if (actionTimer >= actionTime) {
					state = GOD_HAND_STATE.END;
					direction = (DIRECTION) ((int) (direction) * -1);
				}
				break;
				//this state will switch by hand object if it grab an player
			case GOD_HAND_STATE.GRAB:
				if (target != null) {
					//move hand object and target until it reach the position then set state to end
					rb.MovePosition (rb.position + velocity * (int) direction * Time.deltaTime);
					target.transform.position = rb.position;
					if (Parent.Rigidbody2D.Distance (target).distance <= minmusDistance)
						state = GOD_HAND_STATE.END;
				}
				break;

			case GOD_HAND_STATE.END:
				rb.MovePosition (rb.position + velocity * (int) direction * Time.deltaTime);
				//if hand object back enter this if statement
				if (Parent.Rigidbody2D.Distance (col).distance <= minmusDistance) {
					//make player move again
					Parent.SetMovement (true);
					//set state to unuse
					state = GOD_HAND_STATE.UNUSE;
					//reset direction
					direction = DIRECTION.NONE;
					//reset handobject parent and disable it
					handObject.transform.parent = this.transform;
					handObject.SetActive (false);
					//make skill enter cd
					ResetCoolDown ( );
				}
				break;
		}
	}

	public void GrabTarget (Collider2D col, IPlayer targetIPlayer) {
		//if get other player set direction
		direction = (DIRECTION) ((int) (direction) * -1);
		//switch state to grab
		state = GOD_HAND_STATE.GRAB;
		target = col;
		//stop target player action 
		targetIPlayer.StopAction (stopActionTime);
	}
}
