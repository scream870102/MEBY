using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//skill shun is the personal skill for MASANARI
//this class inherit from ISkill
public class SkillDingShan : ISkill {
	//arrowObject will shoot if player hit skill button
	public GameObject arrowObject;
	//flare arrow ref
	protected FlareArrow arrow;
	protected override void Start ( ) {
		//include init isActive and timer and isInCD
		base.Start ( );
		//Get skill coolDown time Number from GameManager instance attribution
		coolDown = GameManager.instance.attribution.allSkillProps [(int) ESkill.DING_SHAN].skillCD;
		actionTime = GameManager.instance.attribution.allSkillProps [(int) ESkill.DING_SHAN].skillActionTime;
		//set about arrow Object
		arrowObject.SetActive (false);
		arrow = arrowObject.GetComponent<FlareArrow> ( );
		arrow.arrowActionTime = actionTime;
	}

	//override useSkill
	protected override void UseSkill ( ) {
		if (Input.GetButtonDown (Parent.NumPlayer + buttonString)) {
			arrowObject.SetActive (true);
			arrow.Use ( );
			ResetCoolDown ( );
		}
	}
}
