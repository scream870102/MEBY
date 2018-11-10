using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Bomb : IItem {
	//ref for rigidbody which can set body type to kinematic when state equals to unuse,pickable,pickup
	protected Rigidbody2D rb;
	//force to add to the bomb when player throw it
	public Vector2 force;
	public float attackPoint;
	protected override void Start ( ) {
		//set attribution --get the data from gamemanager
		props = GameManager.instance.attribution.allItemProps [(int) EItem.BOMB];
		rb = GetComponent<Rigidbody2D> ( );
		//set rigidbody type to kinematic cause state is unuse
		rb.bodyType = RigidbodyType2D.Kinematic;
	}

	//override UsingItem 
	//define how item process when player hit use item
	public override void UsingItem ( ) {
		//if get owner and col disable
		//add force to rb and set bodyType to Dynamic
		//enable render and collider
		if (owner != null && col.enabled == false) {
			rb.velocity = new Vector2 (0.0f, 0.0f);
			rb.bodyType = RigidbodyType2D.Dynamic;
			transform.position = owner.transform.position;
			rend.enabled = true;
			col.enabled = true;
			col.isTrigger = false;
			//get owner direction and set force to rigidbody
			if (owner.IsPlayerFacingRight ( )) {
				rb.AddForce (force, ForceMode2D.Impulse);
			}
			else {
				Vector2 temp = force;
				temp.x *= -1;
				rb.AddForce (temp, ForceMode2D.Impulse);
			}
		}

	}
	//this function call when bomb being thrown and hit other palyer
	protected void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" && state == EItemState.USING && other.gameObject != owner.gameObject) {
			other.gameObject.GetComponent<IPlayer> ( ).UnderAttack (attackPoint);
			InitUnuse ( );
		}
	}

	//reset	roatation and set bodyType to Kinematic also set collider to trigger
	public override void InitUnuse ( ) {
		base.InitUnuse ( );
		rb.bodyType = RigidbodyType2D.Kinematic;
		col.isTrigger = true;
		transform.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
	}
}
