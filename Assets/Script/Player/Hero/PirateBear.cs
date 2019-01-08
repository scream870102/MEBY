using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PirateBear : IPlayer {
	//set hero type
	//get hero property from Gamemanger instance attribution
	//get rigidbody and set its mass
	private List<string> PIRATE_BEAR_STATE= new List<string>();
	public override void Start ( ) {
		PIRATE_BEAR_STATE.Add("IDLE");
		PIRATE_BEAR_STATE.Add("WALK");
		PIRATE_BEAR_STATE.Add("JUMP");
		PIRATE_BEAR_STATE.Add("ATTACK");
		PIRATE_BEAR_STATE.Add("ATTACK_END");
		PIRATE_BEAR_STATE.Add("SKILL");
		PIRATE_BEAR_STATE.Add("SKILL_END");
		PIRATE_BEAR_STATE.Add("PAINTBALL_USING");
		Hero = EHero.PIRATE_BEAR;
		Props = GameManager.instance.attribution.allHeroProps [(int) EHero.PIRATE_BEAR];
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
				animator.SetBool ("Skill", false);
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
			case "SKILL_END":
				animator.SetBool ("Skill", false);
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

	public void PirateBearSkillAnimEnd(){
		state="SKILL_END";
	}
}
