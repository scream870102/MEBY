using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : IItem {
	protected override void Start(){
		props=GameManager.instance.attribution.allItemProps[(int)EItem.HEALTH_KIT];
	}
	public override void UsingItem(){
		if(owner!=null){
			owner.UnderAttack(-50.0f);
		}
	}
}
