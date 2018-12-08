using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Bomb : IItem {
	//ref for rigidbody which can set body type to kinematic when state equals to unuse,pickable,pickup
	[SerializeField]
	protected Rigidbody2D rb;
	//force to add to the bomb when player throw it
	public Vector2 force;
	//define how many damage will add to player
	public float attackPoint;
	//const for position offset prevent bomb collide with owner when owenr hit use button 
	protected float posOffset = 0.5f;
	protected override void Start ( ) {
		//set attribution --get the data from gamemanager
		props = GameManager.instance.attribution.allItemProps [(int) EItem.BOMB];
		if (rb == null)
			rb = GetComponent<Rigidbody2D> ( );
	}

	//override UsingItem 
	//define how item process when player hit use item
	public override void UsingItem ( ) {
		//if get owner and col disable
		//add force to rb and set bodyType to Dynamic
		//enable render and collider
		if (owner != null && col.enabled == false) {
			rb.velocity = new Vector2 (0.0f, 0.0f);
			Vector3 pos = owner.transform.position;
			//get owner direction and set force to rigidbody
			if (owner.IsPlayerFacingRight) {
				pos.x += posOffset;
				transform.position = pos;
				rb.AddForce (force, ForceMode2D.Impulse);
			}
			else {
				Vector2 temp = force;
				temp.x *= -1;
				pos.x -= posOffset;
				transform.position = pos;
				rb.AddForce (temp, ForceMode2D.Impulse);
			}
			rend.enabled = true;
			col.enabled = true;
		}

	}
	//this function call when bomb being thrown and hit other palyer
	protected override void OnCollisionEnter2D (Collision2D other) {
		base.OnCollisionEnter2D (other);
		if (other.gameObject.tag == "Player" && state == EItemState.USING && other.gameObject != owner.gameObject) {
			other.gameObject.GetComponent<IPlayer> ( ).UnderAttack (attackPoint);
			InitUnuse ( );
		}
	}

	//reset	roatation and set bodyType to Kinematic also set collider to trigger
	public override void InitUnuse ( ) {
		base.InitUnuse ( );
		transform.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
	}
}
