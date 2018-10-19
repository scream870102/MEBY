using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }

	protected float basicHealth;
	protected float currentHealth;
	// Use this for initialization
	public virtual void Start ( ) {
		if(parent!=null)
			this.basicHealth=parent.Props.basicHealth;
		currentHealth=basicHealth;
	}


	public virtual void TakeDamage(float damage){
		currentHealth-=damage;
		if(currentHealth<=.0f){
			Debug.Log(parent.gameObject.name+"death");
		}
	}
}
