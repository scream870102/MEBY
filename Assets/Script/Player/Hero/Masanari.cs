using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//class for hero masanari
//this class inherit from IPlayer
public class Masanari : IPlayer {
	//ref for skillshun
	protected SkillShun shun = null;
	//to get shun ref
	//set shun.parent and active the skill
	protected void Awake ( ) {
		//set ref for shun
		//set shun.parent and enable it
		shun = GetComponent<SkillShun> ( );
		if (shun != null) {
			shun.Parent = this;
			shun.SetActive (true);
		}
	}
	//set hero type
	//get hero property from Gamemanger instance attribution
	//get rigidbody and set its mass
	protected override void Start ( ) {
		Hero = EHero.MASANARI;
		Props = GameManager.instance.attribution.allHeroProps [(int) EHero.MASANARI];
		base.Start ( );
		rb = GetComponent<Rigidbody2D> ( );
		rb.mass = Props.mass;
	}
	protected override void Update ( ) { }
}
