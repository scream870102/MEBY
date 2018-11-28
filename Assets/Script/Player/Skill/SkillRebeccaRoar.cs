using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//skill rebeccaRoar is the personal skill for REBECCA
//this class inherit from ISkill
public class SkillRebeccaRoar : ISkill {
	//const define how big does roar can effect other player
	[SerializeField]
	protected float roarRadius;
	//const define max force to add to the player
	[SerializeField]
	protected float force;
	protected override void Start ( ) {
		//include init isActive and timer and isInCD
		base.Start ( );
		//Get skill coolDown time Number from GameManager instance attribution
		coolDown = GameManager.instance.attribution.allSkillProps [(int) ESkill.REBECCA_ROAR].skillCD;
		actionTime = GameManager.instance.attribution.allSkillProps [(int) ESkill.REBECCA_ROAR].skillActionTime;
	}

	//override useSkill
	protected override void UseSkill ( ) {
		//if player hit skill button
		//find all collider in radius
		//if collider has player tag then add force to it
		if (Input.GetButtonDown (Parent.NumPlayer + buttonString)) {
			Collider2D [ ] colliders = Physics2D.OverlapCircleAll (transform.position, roarRadius);
			foreach (Collider2D collider in colliders) {
				Rigidbody2D rb=null;
				if(collider.gameObject.tag=="Player"&&collider.gameObject!=gameObject)
					rb=collider.GetComponentInParent<Rigidbody2D>();
					if(rb!=null){
						//get vector by minus player and target position then normalized it
						Vector2 vector=(rb.position-Parent.Rigidbody2D.position).normalized;
						//calculate how large force will add to the target 
						//if they have longer distance between them then target have smaller force to add to itself 
						float offset=roarRadius-Vector2.Distance(rb.position,Parent.Rigidbody2D.position);
						rb.AddForceAtPosition(vector*force*offset,Parent.Rigidbody2D.position);
					}		
			}
			ResetCoolDown ( );
		}
	}
}
