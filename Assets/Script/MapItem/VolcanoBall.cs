using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoBall : MonoBehaviour {
	protected EColor paintballColor;
	[SerializeField]
	protected SpriteRenderer sprite;
	protected TilemapManager manager=null;
	void Start () {
		paintballColor = (EColor) Random.Range (0, System.Enum.GetValues (typeof (EColor)).Length - 1);
		sprite.color=GameManager.instance.attribution.allColors[(int)paintballColor];
		manager = GameObject.Find ("Map").GetComponent<TilemapManager> ( );
		Destroy(this.gameObject,5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag=="Ground"){
			manager.SetTile (paintballColor, transform.position);
			Destroy(this.gameObject);
		}
	}
}
