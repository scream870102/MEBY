using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bomb : IItem {
	protected Rigidbody2D rb;
	public Vector2 force;
	protected override void Start () {
		props=GameManager.instance.attribution.allItemProps[(int)EItem.BOMB];
		rb=GetComponent<Rigidbody2D>();
		rb.bodyType=RigidbodyType2D.Kinematic;
	}
	
	public override void UsingItem(){
		if(owner!=null&&col.enabled==false){
			rb.velocity=new Vector2(0.0f,0.0f);
			rb.bodyType=RigidbodyType2D.Dynamic;
			transform.position=owner.transform.position;
			rend.enabled=true;
			col.enabled=true;
			//get owner direction
			if(owner.transform.localScale.x>0){
				rb.AddForce(force,ForceMode2D.Impulse);
			}
			else{
				Vector2 temp=force;
				force.x*=-1;
				rb.AddForce(force,ForceMode2D.Impulse);
			}
		}

	}
	protected override void OnTriggerEnter2D(Collider2D other) {
		base.OnTriggerEnter2D(other);
		if(other.gameObject.tag=="Player"&&state==EItemState.USING&&other.gameObject!=owner.gameObject){
			other.GetComponent<IPlayer>().UnderAttack(100.0f);
			InitUnuse();
		}

	}
	public override void InitUnuse(){
		base.InitUnuse();
		rb.bodyType=RigidbodyType2D.Kinematic;
	}
}
