using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//class for hero masanari
//this class inherit from IPlayer
public class Masanari : IPlayer {
	//set hero type
	//get hero property from Gamemanger instance attribution
	//get rigidbody and set its mass
	private List<string> MASANARI_ANIM_STATE = new List<string> ( );
	public override void Start ( ) {
		MASANARI_ANIM_STATE.Add("IDLE");
		MASANARI_ANIM_STATE.Add("WALK");
		MASANARI_ANIM_STATE.Add("JUMP");
		MASANARI_ANIM_STATE.Add("ATTACK");
		MASANARI_ANIM_STATE.Add("ATTACK_END");
		MASANARI_ANIM_STATE.Add("SKILL_START");
		MASANARI_ANIM_STATE.Add("SKILL_FIRST_FIN");
		MASANARI_ANIM_STATE.Add("SKILL_END");
		MASANARI_ANIM_STATE.Add("PAINBALL_USING");
		Hero = EHero.MASANARI;
		Props = GameManager.instance.attribution.allHeroProps [(int) EHero.MASANARI];
		base.Start ( );
		rb = GetComponent<Rigidbody2D> ( );
		rb.mass = Props.mass;
	}

		protected override void Update ( ) {
		base.Update();
		switch (state) {
			case "IDLE":
				animator.SetBool ("Walk", false);
				animator.SetBool ("Jump", false);
				animator.SetBool("SkillEnd",false);
				animator.SetBool("Paint",false);
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
			case "SKILL_FIRST_FIN":
				animator.SetBool ("Skill", false);
				break;
			case "SKILL_END":
				animator.SetBool ("Skill", false);
				animator.SetBool("SkillEnd",true);
				break;
			case "PAINTBALL_USING":
				animator.SetBool("Paint",true);
				break;
			case "PAINTBALL_FIN":
				animator.SetBool("Paint",false);
				break;
			default:
				break;
		}
	}
	public void ShunAnimEnd ( ) {
        State = "SKILL_FIRST_FIN";
    }
	public void ShunAfterAnimEnd(){
		animator.SetBool("SkillEnd",false);
	}
}
