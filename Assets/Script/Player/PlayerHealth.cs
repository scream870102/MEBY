using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	//ref for IPlayer
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }

	// const for basichealth get from parent.props
	protected float basicHealth;
	//field to store current health
	protected float currentHealth;

	//call by IPlayer after setting parent
	public virtual void Start ( ) {
		if (parent != null)
			this.basicHealth = parent.Props.basicHealth;
		currentHealth = basicHealth;
	}

	//call when player get damage if cureen health<=0 enter death state
	public virtual void TakeDamage (float damage) {
		currentHealth -= damage;
		if (currentHealth <= .0f) {
			OnDeath ( );
		}
	}

	//define death state
	protected virtual void OnDeath ( ) {
		Debug.Log (parent.gameObject.name + "death");
	}

	public float GetHealth ( ) {
		return currentHealth;
	}
}
