using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	//ref for IPlayer
	private IPlayer parent = null;
	public IPlayer Parent { set { if (parent == null) parent = value; } get { return parent; } }

	//ref for attack hitcollider to detect what hit
	[SerializeField]
	protected Collider2D hitCollider = null;

	//bool go true during attack
	protected bool bAttacknig = false;

	//bool go true when attak in CD
	protected bool bInCD = false;

	//const for attack continued time get from parent props
	protected float basicAttackSpeed = 0.0f;
	//const for basic attack point get from parent props
	protected float basicAttackPoint = 0.0f;
	//const for time between attack and next attack
	protected float basicAttackInterval = 0.0f;
	//timer to count cd and attack speed
	protected float timer = 0.0f;

	//set ref call in IPlayer after setting parent
	public virtual void Start ( ) {
		if (hitCollider != null)
			hitCollider.enabled = false;
		bAttacknig = false;
		if (parent != null) {
			basicAttackSpeed = parent.Props.basicAttackSpeed;
			basicAttackPoint = parent.Props.basicAttackPoint;
			basicAttackInterval = parent.Props.basicAttackInterval;
		}

	}

	protected virtual void Update ( ) {
		//when player hit attack button set bool and timer
		if (Input.GetButtonDown (parent.NumPlayer + "Fire1") && hitCollider != null && !bInCD) {
			bAttacknig = true;
			hitCollider.enabled = true;
		}

		//if in attacking state accumulation timer and enter cd state when timer>=basicAttackSpeed
		if (bAttacknig) {
			timer += Time.deltaTime;
			if (timer >= basicAttackSpeed) {
				bAttacknig = false;
				bInCD = true;
				timer = .0f;
				hitCollider.enabled = false;
			}
		}

		//if attack in CD accumulate timer and reset when timer>=basicAttackInterval
		if (bInCD) {
			timer += Time.deltaTime;
			if (timer >= basicAttackInterval) {
				bInCD = false;
				timer = .0f;
			}
		}

	}

	//OnTriggerEnter call when attack collider trigger something
	private void OnTriggerEnter2D (Collider2D other) {
		//check if the object is other player if true Attack the player
		if (other.gameObject.tag == "Player" && other.gameObject != this.transform.parent.gameObject) {
			OnAttack (other.gameObject.GetComponent<IPlayer> ( ));
		}
	}

	//call other.gameobject.player.underattack
	private void OnAttack (IPlayer player) {
		player.UnderAttack (this.basicAttackPoint);

	}
}
