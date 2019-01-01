using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ArtBomb : IItem {
	//enum define art bomb state
	enum ART_BOMB_STATE {
		//setting position and enable collider , renderer and trigger
		SETTING,
		//waiting for other player enter the trigger
		WAITING,
		//set near tiles transform to other color randomly
		BOMBING,
	}
	//field to save the ref for bombstate
	private ART_BOMB_STATE bombState;
	//ref for trigger which to detect if there is other player enter the detect area
	[SerializeField]
	private Collider2D trigger;
	//ref for tilemap manager which to change tile color
	private TilemapManager tilemapManager = null;
	//how large will the art bomb effect
	[SerializeField]
	private Vector2 posOffset;
	//Get props from gameManager 
	protected override void Start ( ) {
		props = GameManager.instance.attribution.allItemProps [(int) EItem.ART_BOMB];
		bombState = ART_BOMB_STATE.SETTING;
		trigger.enabled = false;

	}

	//Add all players if players.count equals to zero then call setStray if player not equal to owner
	public override void UsingItem ( ) {
		switch (bombState) {
			//get tilemapmanager and also set position and enable collider trigger and renderer then set state to WAITTING
			case ART_BOMB_STATE.SETTING:
				if (tilemapManager == null)
					tilemapManager = GameObject.Find ("Map").GetComponent<TilemapManager> ( );
				transform.position = Owner.transform.position;
				col.enabled = true;
				trigger.enabled = true;
				rend.enabled = true;
				bombState = ART_BOMB_STATE.WAITING;
				break;
				//keep wait for other player to enter trigger area
			case ART_BOMB_STATE.WAITING:
				break;
				//after bombing reset art bomb
			case ART_BOMB_STATE.BOMBING:
				BeforeEndState ( );

				break;

		}
	}

	//disable trigger and reset bombState to SETTING
	protected override void BeforeEndState ( ) {
		trigger.enabled = false;
		bombState = ART_BOMB_STATE.SETTING;
		InitUnuse ( );
	}

	protected override void OnTriggerEnter2D (Collider2D other) {
		base.OnTriggerEnter2D (other);
		//if bombstate is WAITING and other player enter the area find 10 random position and color then try to set tiles
		if (bombState == ART_BOMB_STATE.WAITING && other.tag == "Player" && other.GetComponent<IPlayer> ( ) != Owner) {
			for (int i = 0; i < 10; i++) {
				Vector3 pos = new Vector3 (transform.position.x + Random.Range (-posOffset.x, posOffset.x), transform.position.y + Random.Range (-posOffset.y, posOffset.y), transform.position.z);
				EColor paintballColor = (EColor) Random.Range (0, System.Enum.GetValues (typeof (EColor)).Length - 1);
				tilemapManager.SetTile (paintballColor, pos);
			}
			bombState = ART_BOMB_STATE.BOMBING;
		}
	}
}
