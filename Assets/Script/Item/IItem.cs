using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//need to set button
//base class for all item
[RequireComponent (typeof (Collider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class IItem : MonoBehaviour {
	//ref for collider
	//collider is for detecting which object touch the item 
	protected Collider2D col = null;
	//ref for spriterenderer
	//item in other state except for pickable state disable spriterender
	protected SpriteRenderer rend = null;
	//store the ref for itmer owner
	[SerializeField]
	protected IPlayer owner = null;
	public IPlayer Owner { get { return owner; } set { if (owner == null) owner = value; } }
	//store ref for itemManager
	//which define when to spawn item
	protected ItemManager manager = null;
	public ItemManager Manager { set { if (manager == null) manager = value; } }
	//itemProps save if this itm is an instant item or how long is its effect time
	protected ItemProps props;
	//timer to count item effect time
	protected float timer = 0.0f;
	//store currnet item state
	[SerializeField]
	protected EItemState state;
	public EItemState State { get { return state; } }
	protected string buttonString = "Fire2";

	//Get componenet and set state to UNUSE
	protected virtual void Awake ( ) {
		col = GetComponent<Collider2D> ( );
		rend = GetComponent<SpriteRenderer> ( );
		col.enabled = false;
		rend.enabled = false;
		state = EItemState.UNUSE;
	}

	protected virtual void Start ( ) { }

	protected virtual void Update ( ) {
		//if player press use button and already have item use it
		if (state == EItemState.PICK_UP && owner != null && Input.GetButtonDown (owner.NumPlayer + buttonString)) {
			InitUse ( );
		}
		//if state equal using start to count time and use the Item after using init it to Unuse
		if (state == EItemState.USING) {
			timer += Time.deltaTime;
			if (timer >= props.continuousTime && !props.bInstantItem) {
				InitUnuse ( );
				return;
			}
			UsingItem ( );
			if (props.bInstantItem)
				InitUnuse ( );
		}
	}

	//if player touch the item which state equal pickable
	protected virtual void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" && state == EItemState.PICKABLE) {
			IPlayer owner = other.gameObject.GetComponent<IPlayer> ( );
			if (owner.Item == null)
				InitPickUp (owner);
		}

	}

	//MUST OVERRIDE
	//define what is item action
	public virtual void UsingItem ( ) { }

	//init state to using--when player use item 
	protected virtual void InitUse ( ) {
		timer = 0.0f;
		state = EItemState.USING;
	}

	//init state to pick_up--disable collider and sprite and set owner 
	protected void InitPickUp (IPlayer owner) {
		this.owner = owner;
		owner.Item = this;
		col.enabled = false;
		rend.enabled = false;
		state = EItemState.PICK_UP;
	}

	//init state to unuse--item doesn't show on the map just keep ref in the ItemManager
	public virtual void InitUnuse ( ) {
		if (owner != null)
			owner.Item = null;
		owner = null;
		col.enabled = false;
		rend.enabled = false;
		gameObject.SetActive (false);
		state = EItemState.UNUSE;
		manager.ItemAlreadyUse ( );
	}

	//init state to pickable--item show on the map and can be picked up by player
	public void InitPickable ( ) {
		gameObject.SetActive (true);
		col.enabled = true;
		rend.enabled = true;
		state = EItemState.PICKABLE;
	}
}
