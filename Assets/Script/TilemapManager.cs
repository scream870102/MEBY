using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour {
	public GridLayout grid=null;
	public List<Tilemap> tilemaps=new List<Tilemap>();

	public void SetTile(EColor color,Vector3 originalPos){
		if(grid!=null){
			Vector3Int pos=grid.WorldToCell(originalPos);
			pos.y-=1;
			TileBase temp=null;
			Tilemap beforeMap=null;
			foreach(Tilemap tilemap in tilemaps){
				if(tilemap.GetTile(pos)!=null){
					temp=tilemap.GetTile(pos);
					beforeMap=tilemap;
				}
			}
			if(temp!=null && beforeMap!=null&&beforeMap!=tilemaps[(int)color]){
				tilemaps[(int)color].SetTile(pos,temp);
				beforeMap.SetTile(pos,null);
			}
		}
	}
}
