using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Depon : IItem {
	//get playerController ref
	private PlayerController playerController;
	//all player in the playScene include Owner
	private List<IPlayer> players = new List<IPlayer> ( );
	//how long will depon will react to other player
	[SerializeField]
	private float effectTime;

	//Get props from gameManager and fine playerController
	protected override void Start ( ) {
		props = GameManager.instance.attribution.allItemProps [(int) EItem.DEPON];
		playerController = GameObject.Find ("PlaySceneController").GetComponent<PlayerController> ( );
		players.AddRange (playerController.GetAllPlayers ( ));
	}

	//Add all players if players.count equals to zero then call setStray if player not equal to owner
	public override void UsingItem ( ) {
		if (players.Count == 0)
			players.AddRange (playerController.GetAllPlayers ( ));
		foreach (IPlayer player in players) {
			if (player != Owner)
				player.SetStray (true, effectTime);
		}
	}

	protected override void BeforeEndState ( ) {

	}
}
