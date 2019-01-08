using System.Collections;
using System.Collections.Generic;

using UnityEngine;
// playercontroller is a class to controll all player
//include how to spawn hero and set info for them
public class PlayerController : MonoBehaviour {
	//a playScene ref
	public PlayScene playScene = null;
	//define what hero to spawn
	protected List<EHero> heroType = new List<EHero> ( );
	//define hero color
	protected List<EColor> heroColor = new List<EColor> ( );
	//store ref for hero gameobject after spawn
	[SerializeField]
	protected List<GameObject> heroes = new List<GameObject> ( );
	protected List<IPlayer> players = new List<IPlayer> ( );
	//define how many hero need to spawn
	protected int numOfPlayers = 0;
	protected int survivalPlayer = 0;
	protected virtual void Start ( ) { }
	protected virtual void Update ( ) { }
	//public method make other class can set play info
	public void SetInfo (PlayState state) {
		numOfPlayers = state.numOfPlayers;
		heroType.AddRange (state.heroes);
		heroColor.AddRange (state.heroesColor);
	}
	//
	//position is temp random
	//
	//public method to make other class to spawn all player
	public void SpawnAllPlayer ( ) {
		survivalPlayer = numOfPlayers;
		for (int i = 0; i < numOfPlayers; i++)
			heroes.Add (SpawnPlayer (heroType [i], transform, i + 1, heroColor [i]));
	}
	//method define how to spawn player
	//after spawn get Iplayer or its derived class
	//set which num is player belong
	//return gameobject
	private GameObject SpawnPlayer (EHero hero, Transform spawnPos, int numPlayer, EColor color) {
		GameObject tempObject;
		tempObject = Instantiate (GameManager.instance.attribution.allHeroPrefab [(int) hero], spawnPos);
		IPlayer temp = null;
		temp = tempObject.GetComponent<IPlayer> ( );
		if (temp != null) {
			players.Add (temp);
			temp.SetNumPlayer (numPlayer);
			temp.Color = color;
			temp.Init = true;
			temp.PlayerController = this;
			temp.Start ( );
		}
		tempObject.name = temp.NumPlayer;
		tempObject.layer = LayerMask.NameToLayer ("Player" + color.ToString ( ));
		return tempObject;
	}

	//call when playerDead remove player and if only one survive call playScene the game is end
	//if want to remove one item in a list can't use iterator need to use for and iterate them from last item
	//https://reurl.cc/MA46W
	public void PlayerDead (IPlayer player) {
		for (int i = heroes.Count - 1; i >= 0; i--) {
			if (player.gameObject == heroes [i]) {
				heroes.Remove (player.gameObject);
				survivalPlayer--;
			}
		}
		if (survivalPlayer == 1 && playScene != null) {
			playScene.GameEnd (heroes [0].GetComponent<IPlayer> ( ));
		}
	}

	//public method for other class to get all player in the playScene
	public List<IPlayer> GetAllPlayers ( ) {
		return players;
	}
	
	//public method for other class to get all player transform in the playScene
	public List<Transform> GetAllPlayersTransfrom ( ) {
		List<Transform> transforms = new List<Transform> ( );
		foreach (IPlayer player in players) {
			transforms.Add (player.transform);
		}
		return transforms;
	}

}
