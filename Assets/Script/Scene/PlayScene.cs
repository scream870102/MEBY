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
	//field for remain playTime
	protected float remainGameTime;
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
		playerController.playScene = this;
		playState = SceneController.playState;
		remainGameTime = playState.gameSetTime;
		SpawnMap ( );
		playerController.SetInfo (playState);
		playerController.SpawnAllPlayer ( );

	}

	protected override void Update ( ) {
		remainGameTime -= Time.deltaTime;
		if (remainGameTime <= .0f)
			GameEnd ( );
	}

	//spawn map accordind to playState
	private void SpawnMap ( ) {
		GameObject temp = Instantiate (GameManager.instance.attribution.allMapPrefab [(int) playState.map]);
		temp.name = "Map";
	}
	//if Game End set winner and enter to EndScene
	public void GameEnd (IPlayer winner = null) {
		if (winner != null) {
			GameManager.instance.SceneController.winner = winner.name;
		}
		else {
			GameManager.instance.SceneController.winner = "Game Developer";
		}
		GameManager.instance.SceneController.SetScene ("EndScene");
	}
}
