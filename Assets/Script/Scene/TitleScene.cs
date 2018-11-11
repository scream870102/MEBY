using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TitleScene : IScene {
	protected override void Awake ( ) {
		base.Awake ( );
		sceneName = "TitleScene";
	}

	// Use this for initialization
	protected override void Start ( ) {

	}

	// Update is called once per frame
	protected override void Update ( ) {
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameManager.instance.SceneController.SetScene ("SelectScene");
		}
	}
}
