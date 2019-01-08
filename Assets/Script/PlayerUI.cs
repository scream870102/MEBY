using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	protected Slider slider;
	//ref for bulletSprite
	protected RawImage bulletSprite = null;
	//ref for player playerText
	protected Text playerText = null;
	//ref for UI player transform
	protected Transform follower = null;
	//ref for IPlayer which can get player info
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }

	[SerializeField]
	protected Vector3 sliderPosOffset;
	[SerializeField]
	protected Vector3 bulletPosOffset;
	[SerializeField]
	protected Image sliderImage;
	//set bulletSprite and playerText ref
	//if parent already set active the UI Object
	//also set the color and info init
	public virtual void Start ( ) {
		bulletSprite = transform.Find ("BulletImage").GetComponent<RawImage> ( );
		playerText = transform.Find ("Health").GetComponent<Text> ( );
		slider = transform.Find ("Slider").GetComponent<Slider> ( );
		if (parent != null) {
			gameObject.SetActive (true);
			playerText.gameObject.SetActive (false);
			slider.gameObject.SetActive(true);
			follower = parent.gameObject.transform;
			playerText.text = ((int) (parent.GetHealth ( ))).ToString ( );
			playerText.color = GameManager.instance.attribution.allColors [(int) parent.Color];
			bulletSprite.gameObject.SetActive (true);
			bulletSprite.color = GameManager.instance.attribution.allColors [(int) parent.GetNextPaintBallColor ( )];
			sliderImage.color=GameManager.instance.attribution.allColors [(int) parent.Color];
		}
	}

	// Keep Update all UI info
	void Update ( ) {
		if (parent != null) {
			//Get UIPos from player and get the pos from follower position
			Vector3 bulletUIPos = Camera.main.WorldToScreenPoint (follower.position + bulletPosOffset);
			Vector3 sliderUIPos = Camera.main.WorldToScreenPoint (follower.position + sliderPosOffset);
			playerText.transform.position = bulletUIPos;
			playerText.text = ((int) (parent.GetHealth ( ))).ToString ( );
			bulletSprite.transform.position = bulletUIPos;
			bulletSprite.color = GameManager.instance.attribution.allColors [(int) parent.GetNextPaintBallColor ( )];
			slider.transform.position=sliderUIPos;
			slider.value=parent.GetHealth();
		}
	}

}
