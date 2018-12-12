using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimeBomb : IItem {
	//enum for time bomb state
	protected enum TIME_BOMB_STATE {
		//setting the bomb in position
		SETTING,
		//start to count down
		COUNTING,
		//bomb
		BOMBING,
	}
	//field define when time bomb explode
	protected float limitTime;
	//field store time bomb current state
	protected TIME_BOMB_STATE timeBombState = TIME_BOMB_STATE.SETTING;
	//define hwo mnau damage will cause when other player got hit
	[SerializeField]
	protected float damage;
	//ref for bomb detect area collider
	[SerializeField]
	protected Collider2D bombDetectArea = null;
	//const define offset when player set the bomb
	[SerializeField]
	protected Vector3 posOffset;
	//field to store where is player set the bomb
	protected Vector3 setPos;
	//get item props from global attribution
	protected override void Start ( ) {
		props = GameManager.instance.attribution.allItemProps [(int) EItem.TIME_BOMB];
		limitTime = props.continuousTime - 0.5f;
		bombDetectArea.enabled = false;
	}

	public override void UsingItem ( ) {
		switch (timeBombState) {
			//set the position and enable sprite disable collider
			case TIME_BOMB_STATE.SETTING:
				transform.position = Owner.transform.position + (Owner.IsPlayerFacingRight?posOffset: -posOffset);
				setPos = transform.position;
				rend.enabled = true;
				col.enabled = false;
				timeBombState = TIME_BOMB_STATE.COUNTING;
				Owner.Item = null;
				break;
				//if time to explode the bomb set state to bombing then enable bombDetectArea
			case TIME_BOMB_STATE.COUNTING:
				transform.position = setPos;
				if (timer >= limitTime) {
					bombDetectArea.enabled = true;
					timeBombState = TIME_BOMB_STATE.BOMBING;
				}
				break;
		}
	}

	//if other player enter bomb detect area attack the player
	protected override void OnTriggerEnter2D (Collider2D other) {
		base.OnTriggerEnter2D (other);
		if (timeBombState == TIME_BOMB_STATE.BOMBING) {
			if (other.gameObject.tag == "Player" && other.gameObject != Owner.gameObject) {
				IPlayer hitObject = other.GetComponent<IPlayer> ( );
				if (hitObject != null)
					hitObject.UnderAttack (damage);
			}
		}

	}

	//disable bomb detect area and reset state to setting
	protected override void BeforeEndState ( ) {
		bombDetectArea.enabled = false;
		timeBombState = TIME_BOMB_STATE.SETTING;
	}
}
