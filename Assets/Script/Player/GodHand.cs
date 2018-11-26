using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class GodHand : MonoBehaviour {
	//ref for parent
	[SerializeField]
	protected IPlayer parent;
	//ref for SkillGodHand
	[SerializeField]
	protected SkillGodHand godHand;
	//if enter some collider and that collider has IPlayer also not equal to parent
	//then call grabTarget
	private void OnTriggerEnter2D (Collider2D other) {
		IPlayer target = other.GetComponentInParent<IPlayer> ( );
		if (target != null && target != parent) {
			godHand.GrabTarget (other, target);
		}
	}
}
