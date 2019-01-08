using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MoralDevil : IPlayer {
	//set hero type
	//get hero property from Gamemanger instance attribution
	//get rigidbody and set its mass
	private List<string> MORAL_DEVIL_ANIM_STATE = new List<string> ( );

	public override void Start ( ) {
		MORAL_DEVIL_ANIM_STATE.Add ("IDLE");
		MORAL_DEVIL_ANIM_STATE.Add ("WALK");
		MORAL_DEVIL_ANIM_STATE.Add ("JUMP");
		MORAL_DEVIL_ANIM_STATE.Add ("ATTACK");
		MORAL_DEVIL_ANIM_STATE.Add ("ATTACK_END");
		MORAL_DEVIL_ANIM_STATE.Add ("SKILL_START");
		MORAL_DEVIL_ANIM_STATE.Add ("SKILL_TOUCH");
		MORAL_DEVIL_ANIM_STATE.Add ("SKILL_END");
		MORAL_DEVIL_ANIM_STATE.Add ("PAINTBALL_USING");
		Hero = EHero.MORAL_DEVIL;
		Props = GameManager.instance.attribution.allHeroProps [(int) EHero.MORAL_DEVIL];
		base.Start ( );
		rb = GetComponent<Rigidbody2D> ( );
		rb.mass = Props.mass;
	}
	// Update is called once per frame
	protected override void Update ( ) {
		base.Update();
		switch (state) {
			case "IDLE":
				animator.SetBool ("Walk", false);
				animator.SetBool ("Jump", false);
				animator.SetBool ("SkillEnd", false);
				break;
			case "WALK":
				animator.SetBool ("Walk", true);
				animator.SetBool ("Jump", false);
				break;
			case "JUMP":
				animator.SetBool ("Jump", true);
				break;
			case "ATTACK":
				animator.SetBool ("Attack", true);
				break;
			case "ATTACK_END":
				animator.SetBool ("Attack", false);
				break;
			case "SKILL_START":
				animator.SetBool ("Skill", true);
				break;
			case "SKILL_TOUCH":
				animator.SetBool ("Skill", false);
				animator.SetBool ("SkillEnd", true);
				break;
			case "SKILL_END":
				animator.SetBool ("SkillEnd", false);
				animator.SetBool ("Skill", false);
				break;
			case "PAINTBALL_USING":
				animator.SetBool("Paint",true);
				break;
			case "PAINTBALL_FIN":
				animator.SetBool("Paint",false);
				break;
			case "EMPTY":
				break;
			default:
				break;
		}
	}


}
