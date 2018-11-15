using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerPaintball : MonoBehaviour {
	//ref for player
	protected IPlayer parent = null;
	public IPlayer Parent { get { return parent; } set { if (parent == null) parent = value; } }
	//ref for tile manager which to change the tile color
	public TilemapManager manager = null;
	//ref for player color
	protected EColor playerColor;
	//field for the current paintball color
	protected EColor paintballColor;
	public void Start ( ) {
		if (parent != null) {
			manager = GameObject.Find ("Map").GetComponent<TilemapManager> ( );
			playerColor = parent.Color;
			ResetPaintball ( );
		}
	}

	void Update ( ) {
		//if player hit use button set the tile behind it also reset paintball color
		if (manager != null && Input.GetButtonDown (parent.NumPlayer + "Fire3")) {
			manager.SetTile (paintballColor, transform.position);
			ResetPaintball ( );
		}
	}

	//reset current paintball color
	private void ResetPaintball ( ) {
		EColor preColor = paintballColor;
		paintballColor = (EColor) Random.Range (0, System.Enum.GetValues (typeof (EColor)).Length - 1);
		while (paintballColor == preColor || paintballColor == playerColor)
			paintballColor = (EColor) Random.Range (0, System.Enum.GetValues (typeof (EColor)).Length - 1);
	}

	//public method for other to get paintball color
	public EColor GetPaintballColor ( ) {
		return paintballColor;
	}
}
