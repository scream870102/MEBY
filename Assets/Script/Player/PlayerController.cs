using System.Collections;
using System.Collections.Generic;

using UnityEngine;
// playercontroller is a class to controll all player
//include how to spawn hero and set info for them
public class PlayerController : MonoBehaviour {
	//define what hero to spawn
	[SerializeField]
	protected List<EHero> heroType = new List<EHero> ( );
	//store ref for hero gameobject after spawn
	protected List<GameObject> heroes = new List<GameObject> ( );
	//define how many hero need to spawn
	protected int numOfPlayers = 0;
	protected virtual void Start ( ) { }
	protected virtual void Update ( ) { }
	//public method make other class can set play info
	public void SetInfo (PlayState state) {
		numOfPlayers = state.numOfPlayers;
		heroType.AddRange (state.heroes);
	}
	//
	//position is temp random
	//
	//public method to make other class to spawn all player
	public void SpawnAllPlayer ( ) {
		for (int i = 0; i < heroType.Count; i++)
			heroes.Add (SpawnPlayer (heroType [i], transform, i + 1));
	}
	//method define how to spawn player
	//after spawn get Iplayer or its derived class
	//set which num is player belong
	//return gameobject
	private GameObject SpawnPlayer (EHero hero, Transform spawnPos, int numPlayer) {
		GameObject tempObject;
		tempObject = Instantiate (GameManager.instance.attribution.allHeroPrefab [(int) hero], spawnPos);
		IPlayer temp = null;
		temp = tempObject.GetComponent<IPlayer> ( );
		if (temp != null)
			temp.SetNumPlayer (numPlayer);
		tempObject.name = temp.NumPlayer;
		return tempObject;
	}
}
