using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ItemManager : MonoBehaviour {
	//ref for  all item
	public List<IItem> itemList = new List<IItem> ( );
	//ref for item spawnPos must get from map object
	public List<Transform> itemSpawnPos = new List<Transform> ( );
	//a timer to count when to add new item to map
	protected float timer;
	//a const define how long between two item spawn
	protected float intervalTime = 7.0f;
	//field for number of current item num on the map
	protected int currentItemNum;
	//const define max item can appear on the map or be collect by player
	protected int maxItemNum = 6;
	//const for spawn pos offset
	protected float spawnPosOffset = 1.0f;
	//
	//for test
	//
	public IItem testItem;

	//init itemList set manager
	void Start ( ) {
		foreach (IItem item in itemList) {
			item.Manager = this;
			item.InitUnuse ( );
		}
		Transform itemSpawnPoses = GameObject.Find ("Map").transform.Find ("ItemSpawnPos");
		itemSpawnPos.Clear ( );
		if (itemSpawnPoses != null) {
			for (int i = 0; i < itemSpawnPoses.childCount; i++) {
				itemSpawnPos.Add (itemSpawnPoses.GetChild (i));
			}
		}
		timer = 0.0f;
		currentItemNum = 0;
	}

	//define when to set item to map
	void Update ( ) {
		timer += Time.deltaTime;
		if (timer >= intervalTime && currentItemNum <= maxItemNum) {
			SpawnItem ( );
			currentItemNum++;
			timer = 0.0f;
		}
		//-------------------
		//--test
		if(Input.GetKeyDown(KeyCode.F1)){
			SpawnItem(testItem);
		}
		//-------------------
	}
	//spawn item which state is unuse and set its state to pickable
	private void SpawnItem ( ) {
		int index = Random.Range (0, itemList.Count);
		while (itemList [index].State != EItemState.UNUSE)
			index = Random.Range (0, itemList.Count);
		itemList [index].InitPickable ( );
		Vector3 spawnPos = itemSpawnPos [Random.Range (0, itemSpawnPos.Count)].position;
		spawnPos.x += Random.Range (-spawnPosOffset, spawnPosOffset);
		itemList [index].transform.position = spawnPos;
	}
	//----------------------------------
	//test
	private void SpawnItem (IItem testItem ) {
		testItem.InitPickable ( );
		Vector3 spawnPos = itemSpawnPos [Random.Range (0, itemSpawnPos.Count)].position;
		spawnPos.x += Random.Range (-spawnPosOffset, spawnPosOffset);
		testItem.transform.position = spawnPos;
	}
	//---------------------------------
	//public method for item to notify which been use by player
	public void ItemAlreadyUse ( ) {
		currentItemNum--;
	}
}
