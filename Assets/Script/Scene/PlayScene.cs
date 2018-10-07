using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayScene : IScene {
	protected PlayerController playerController;
	protected PlayState playState;
	protected void Awake ( ) {
		sceneName = "PlayScene";
	}
	// Use this for initialization
	protected override void Start ( ) {
		playerController = GetComponent<PlayerController> ( );
		playState = SceneController.playState;
		playerController.SetInfo (playState);
		playerController.SpawnAllPlayer ( );

	}

	// Update is called once per frame
	protected override void Update ( ) {

	}

}
