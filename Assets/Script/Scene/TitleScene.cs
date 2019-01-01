using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//TitleScene is the scene for game title
public class TitleScene : IScene {
	protected override void Awake ( ) {
		base.Awake ( );
		sceneName = "TitleScene";
	}

	protected override void Start ( ) { }
	//if space key got down enter to mode scene
	protected override void Update ( ) {
		if (Input.GetButtonDown ("Player1Jump")) {
			GameManager.instance.SceneController.SetScene ("ModeScene");
		}
	}
}
