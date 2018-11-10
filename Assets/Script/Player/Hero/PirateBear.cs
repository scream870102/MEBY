using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBear : IPlayer {
	//set hero type
	//get hero property from Gamemanger instance attribution
	//get rigidbody and set its mass
	public override void Start ( ) {
		Hero = EHero.PIRATE_BEAR;
		Props = GameManager.instance.attribution.allHeroProps [(int) EHero.PIRATE_BEAR];
		base.Start ( );
		rb = GetComponent<Rigidbody2D> ( );
		rb.mass = Props.mass;
	}
}
