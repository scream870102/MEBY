using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ItemManager : MonoBehaviour {
	public List<IItem> itemList = new List<IItem> ( );
	public List<Transform> itemSpawnPos=new List<Transform>();
	
	void Start ( ) {
		foreach(IItem item in itemList){
			item.Manager=this;
			item.InitUnuse();
		}
	}

	void Update ( ) {
		//time to add item to map
		if(Input.GetButtonDown("Spawn")){
			int index=Random.Range(0,itemList.Count);
			for(int i=0;i<itemList.Count;i++){
				if(itemList[index].State==EItemState.UNUSE)
					break;
				index=Random.Range(0,itemList.Count);
			}
			if(itemList[index].State==EItemState.UNUSE){
				itemList[index].InitPickable();
				itemList[index].transform.position=itemSpawnPos[Random.Range(0,itemSpawnPos.Count)].position;
			}
		}
	}
}
