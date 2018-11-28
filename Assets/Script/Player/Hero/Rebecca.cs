using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Rebecca : IPlayer {
	//set hero type
	//get hero property from Gamemanger instance attribution
	//get rigidbody and set its mass
	public override void Start ( ) {
		Hero = EHero.REBECCA;
		Props = GameManager.instance.attribution.allHeroProps [(int) EHero.REBECCA];
		base.Start ( );
		rb = GetComponent<Rigidbody2D> ( );
		rb.mass = Props.mass;
	}
}
