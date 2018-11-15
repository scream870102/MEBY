using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class EndScene : IScene {
	//ref for winner Text
	public Text winnerText;
	protected override void Awake ( ) {
		base.Awake ( );
		sceneName = "EndScene";
	}

	//set winner text
	protected override void Start ( ) {
		winnerText.text = "Winner: " + GameManager.instance.SceneController.winner;
	}

	//if player1 get jump button enter titleScene
	protected override void Update ( ) {
		if (Input.GetButtonDown ("Player1Jump")) {
			GameManager.instance.SceneController.SetScene ("TitleScene");
		}
	}
}
