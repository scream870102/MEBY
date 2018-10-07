using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	protected List<EHero> heroType = new List<EHero> ( );
	protected List<GameObject> heroes = new List<GameObject> ( );
	protected int numOfPlayers = 0;
	void Start ( ) {

	}

	// Update is called once per frame
	void Update ( ) {

	}

	public void SetInfo (PlayState state) {
		numOfPlayers = state.numOfPlayers;
		heroType.AddRange (state.heroes);
	}

	//position is temp random
	public void SpawnAllPlayer ( ) {
		for (int i = 0; i < heroType.Count; i++) {
			heroes.Add (SpawnPlayer (heroType [i], transform, i + 1));
		}
	}

	private GameObject SpawnPlayer (EHero hero, Transform spawnPos, int numPlayer) {
		GameObject tempObject;
		tempObject = Instantiate (GameManager.instance.attribution.allHeroPrefab [(int) hero], spawnPos);
		IPlayer temp = null;
		temp = tempObject.GetComponent<IPlayer> ( );
		if (temp != null) {
			temp.SetNumPlayer (numPlayer);
		}
		return tempObject;
	}
}
