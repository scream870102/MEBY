using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FastFruit : IItem {
	//const define how much bonus will multiply to player move speed
	[SerializeField]
	protected float speedBonus;
	//const define how long will fast fruit effect owner
	[SerializeField]
	protected float effectTime;
	//get props from gameManager
	protected override void Start ( ) {
		props = GameManager.instance.attribution.allItemProps [(int) EItem.FAST_FRUIT];
	}
	
	public override void UsingItem ( ) {
		Owner.SetMoveSpeed (speedBonus, true, effectTime);
	}
}
