using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	//a field for UI Position
	protected Vector3 UIPos;
	//ref for bulletSprite
	protected RawImage bulletSprite = null;
	//ref for player playerText
	protected Text playerText = null;
	//ref for UI player transform
	protected Transform follower = null;
	//ref for IPlayer which can get player info
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }
	//set bulletSprite and playerText ref
	//if parent already set active the UI Object
	//also set the color and info init
	public virtual void Start ( ) {
		bulletSprite = transform.Find ("BulletImage").GetComponent<RawImage> ( );
		playerText = transform.Find ("Health").GetComponent<Text> ( );
		if (parent != null) {
			gameObject.SetActive (true);
			playerText.gameObject.SetActive (true);
			follower = parent.gameObject.transform;
			playerText.text = ((int) (parent.GetHealth ( ))).ToString ( );
			playerText.color = GameManager.instance.attribution.allColors [(int) parent.Color];
			bulletSprite.gameObject.SetActive (true);
			bulletSprite.color = GameManager.instance.attribution.allColors [(int) parent.GetNextPaintBallColor ( )];
		}
	}

	// Keep Update all UI info
	void Update ( ) {
		if (parent != null) {
			//Get UIPos from player and get the pos from follower position
			UIPos = Camera.main.WorldToScreenPoint (follower.position);
			playerText.transform.position = UIPos;
			playerText.text = ((int) (parent.GetHealth ( ))).ToString ( );
			bulletSprite.transform.position = UIPos;
			bulletSprite.color = GameManager.instance.attribution.allColors [(int) parent.GetNextPaintBallColor ( )];
		}
	}

}
