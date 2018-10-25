using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerPaintball : MonoBehaviour {
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }
	public TilemapManager manager = null;
	protected EColor paintballColor;
	protected EColor playerColor;
	public void Start ( ) {
		if (parent != null) {
			manager = GameObject.Find ("Grid").GetComponent<TilemapManager> ( );
			playerColor = parent.Color;
			ResetPaintball ( );
		}

	}

	// Update is called once per frame
	void Update ( ) {
		if (manager != null && Input.GetButtonDown (parent.NumPlayer + "Fire3")) {
			manager.SetTile (paintballColor, transform.position);
			ResetPaintball ( );
		}
	}

	private void ResetPaintball ( ) {
		EColor preColor = paintballColor;
		paintballColor = (EColor) Random.Range (0, System.Enum.GetValues (typeof (EColor)).Length-1);
		while (paintballColor == preColor || paintballColor == playerColor)
			paintballColor = (EColor) Random.Range (0, System.Enum.GetValues (typeof (EColor)).Length-1);
	}
	public EColor GetPaintballColor ( ) {
		return paintballColor;
	}
}
