using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;
//this is a class to manage all tiles
public class TilemapManager : MonoBehaviour {
	//grid is are all tile parent which is to use WorldToCell method
	public GridLayout grid = null;
	//tilemaps is to save all color tilemap ref
	public List<Tilemap> tilemaps = new List<Tilemap> ( );
	//public method which is to change tile under original pos 's color
	public void SetTile (EColor color, Vector3 originalPos) {
		if (grid != null) {
			//get the tile to change pos
			Vector3Int pos = grid.WorldToCell (originalPos);
			pos.y -= 1;
			TileBase temp = null;
			Tilemap beforeMap = null;
			//find the tile to change is belong to which tilemap
			foreach (Tilemap tilemap in tilemaps) {
				if (tilemap.GetTile (pos) != null) {
					temp = tilemap.GetTile (pos);
					beforeMap = tilemap;
				}
			}
			//if after color != before color then change the color
			if (temp != null && beforeMap != null && beforeMap != tilemaps [(int) color]) {
				tilemaps [(int) color].SetTile (pos, temp);
				beforeMap.SetTile (pos, null);
			}
		}
	}

}
