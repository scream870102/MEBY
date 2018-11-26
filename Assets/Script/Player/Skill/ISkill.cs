using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//interface for skill
//every skill must to inherit from this class
public class ISkill : MonoBehaviour {
	//define how long does this skill act
	protected float actionTime;
	//A const float to define how long the skill cooldown time
	protected float coolDown;
	//define which button is skill button
	protected string buttonString;
	//To determine if player can use this skill
	protected bool bActive;
	//A time for cooldown time
	protected float timer;
	//A bool to store if skill is in cooldown state
	protected bool bInCD;
	//field ref for IPlayer
	private IPlayer parent = null;
	//Property for parent
	public IPlayer Parent {
		get { return parent; }
		set { if (parent == null) parent = value; }
	}

	protected virtual void Start ( ) {
		timer = .0f;
		bInCD = false;
		buttonString = "Fire2";
	}
	protected virtual void Update ( ) {
		//every frame add deltaTime to timer
		if(bInCD){
			timer += Time.deltaTime;
			if (timer >= coolDown)
				bInCD = false;
		}
			
		//if timer > cool down time set isInCD to false
		
		if (bActive && !bInCD) {
			UseSkill ( );
		}
	}
	//set if this skill can use
	public virtual void SetActive (bool active) {
		this.bActive = active;
	}
	//reset cooldown timer and isInCD
	protected virtual void ResetCoolDown ( ) {
		bInCD = true;
		timer = 0.0f;
	}

	protected virtual void UseSkill ( ) { }
}
