using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Masanari : IPlayer {

	// Use this for initialization
	protected override void Start ( ) {
		Hero = EHero.MASANARI;
		Props = GameManager.instance.attribution.allHeroProps [(int) EHero.MASANARI];
		base.Start ( );

	}

	// Update is called once per frame
	protected override void Update ( ) {

	}
}
