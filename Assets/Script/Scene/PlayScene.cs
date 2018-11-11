using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class PlayScene : IScene {
	//playercontroller can spawnplayer
	protected PlayerController playerController;
	//playstate store all need info for game
	//include how many players and hero type
	[SerializeField]
	protected PlayState playState;
	//set sceneName to playscene
	protected override void Awake ( ) {
		base.Awake ( );
		sceneName = "PlayScene";

	}
	//get playercontroller ref
	//call playercontroller to spawn player
	//set info for playercontroller
	protected override void Start ( ) {
		playerController = GetComponent<PlayerController> ( );
		playState = SceneController.playState;
		SpawnMap();
		playerController.SetInfo (playState);
		playerController.SpawnAllPlayer ( );
		
	}
	protected override void Update ( ) { }
	private void SpawnMap(){
		GameObject temp=Instantiate(GameManager.instance.attribution.allMapPrefab[(int)playState.map]);
		temp.name="Map";
	}
}
