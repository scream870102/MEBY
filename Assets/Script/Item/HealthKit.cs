using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealthKit : IItem {
	//define how many cure effect can add to owner
	public float amountOfCure;
	//get item props from global attribution
	protected override void Start ( ) {
		props = GameManager.instance.attribution.allItemProps [(int) EItem.HEALTH_KIT];
	}
	public override void UsingItem ( ) {
		if (owner != null) {
			owner.UnderAttack (-amountOfCure);
		}
	}
}
