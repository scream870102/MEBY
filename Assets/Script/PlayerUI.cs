using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	Vector3 UIPos;

	protected RawImage bulletSprite = null;
	protected Text healthText = null;
	protected Transform follower = null;
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }
	// Use this for initialization
	public virtual void Start ( ) {
		bulletSprite = transform.Find ("BulletImage").GetComponent<RawImage> ( );
		healthText = transform.Find ("Health").GetComponent<Text> ( );
		if (parent != null) {
			gameObject.SetActive (true);
			healthText.gameObject.SetActive (true);
			follower = parent.gameObject.transform;
			healthText.text = ((int) (parent.GetHealth ( ))).ToString ( );
			healthText.color = GameManager.instance.attribution.allColors [(int) parent.Color];
			bulletSprite.gameObject.SetActive (true);
			bulletSprite.color = GameManager.instance.attribution.allColors [(int) parent.GetNextPaintBallColor ( )];
		}

	}

	// Update is called once per frame
	void Update ( ) {
		if (parent != null) {
			UIPos = Camera.main.WorldToScreenPoint (follower.position);
			healthText.transform.position = UIPos;
			healthText.text = ((int) (parent.GetHealth ( ))).ToString ( );
			bulletSprite.transform.position = UIPos;
			bulletSprite.color = GameManager.instance.attribution.allColors [(int) parent.GetNextPaintBallColor ( )];
		}
	}

}
