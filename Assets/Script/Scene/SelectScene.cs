using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

//heroInfo include texture and name
[System.Serializable]
public struct heroInfo {
	public Texture texture;
	public string name;
}
//select scene is scene for select hero and map
public class SelectScene : IScene {
	//const hero info for all hero
	public List<heroInfo> heroInfo;
	//ref for heroAvatar
	public List<RawImage> heroAvatar;
	//ref for heroName
	public List<Text> heroName;
	//field to detect if player join the game
	private bool [ ] bJoin = { false, false, false, false };
	//field to detect if joined player already select hero and locked
	private bool [ ] bLock = { false, false, false, false };
	//field to store palyer now select hero
	private float [ ] nowSelectedHero = { .0f, .0f, .0f, .0f };
	//field to store for now select map
	private float nowSelecteMap = 0.0f;
	//field to store playstate which will diliver to next scene when enter the game
	private PlayState playState;
	protected override void Awake ( ) {
		base.Awake ( );
		sceneName = "SelectScene";
	}

	protected override void Start ( ) {
		playState.heroes = new List<EHero> ( );
		playState.heroesColor = new List<EColor> ( );
		playState.gameMode = EGameMode.SURVIVOR;
		playState.numOfPlayers = 0;
		for (int i = 0; i < 4; i++) {
			playState.heroes.Add (EHero.NONE);
			playState.heroesColor.Add ((EColor) i);
		}

	}

	//Kepp traing all player input
	protected override void Update ( ) {
		SelectHero ("Player1", 0);
		SelectHero ("Player2", 1);
		SelectHero ("Player3", 2);
		SelectHero ("Player4", 3);
		MapSelected ( );
		PressStart ( );
	}

	//get player enter due to its name
	private void SelectHero (string player, int playerNum) {
		//Enter Jump first will join the game
		//also add numOfPlayers
		if (Input.GetButtonUp (player + "Jump") && !bJoin [playerNum]) {
			bJoin [playerNum] = true;
			playState.numOfPlayers++;
		}
		//if player already join enter hero select
		else if (bJoin [playerNum] && !bLock [playerNum]) {
			//choose hero by use horizontal key
			if (Input.GetButtonDown (player + "Horizontal")) {
				float selectedHero = Input.GetAxisRaw (player + "Horizontal");
				nowSelectedHero [playerNum] += selectedHero;
			}
			//check if player chosen is in the range
			if ((int) nowSelectedHero [playerNum] >= (int) EHero.NONE)
				nowSelectedHero [playerNum] = (float) EHero.MASANARI;
			if (nowSelectedHero [playerNum] < 0.0f)
				nowSelectedHero [playerNum] = (float) (System.Enum.GetNames (typeof (EHero)).Length - 2);
			//set avater and name UI component
			heroAvatar [playerNum].texture = heroInfo [(int) nowSelectedHero [playerNum]].texture;
			heroName [playerNum].text = heroInfo [(int) nowSelectedHero [playerNum]].name;
			//if player enter jump key again means lock 
			if (Input.GetButtonUp (player + "Jump"))
				bLock [playerNum] = true;
		}
		//if player lock hero set hero into playState
		else if (bLock [playerNum]) {
			playState.heroes [playerNum] = (EHero) ((int) nowSelectedHero [playerNum]);
		}
	}

	//get player1 input which can choose map
	private void MapSelected ( ) {
		float selectedMap = Input.GetAxisRaw ("Player1Vertical");
		nowSelecteMap += selectedMap;
		if (nowSelecteMap > (float) (System.Enum.GetNames (typeof (EMap)).Length - 2))
			nowSelecteMap = 0.0f;
		if (nowSelecteMap < 0.0f)
			nowSelecteMap = (float) (System.Enum.GetNames (typeof (EMap)).Length - 2);

	}

	//if every player already lock hero and player1 push start button go to playscene and set playstate to GameManager.instance.SceneController.playState
	private void PressStart ( ) {
		if (Input.GetButtonDown ("Player1Option")) {
			bool allCheck = false;
			for (int i = 0; i < playState.numOfPlayers; i++) {
				allCheck = bLock [i] ? true : false;
			}
			if (allCheck) {
				this.playState.map = (EMap) nowSelecteMap;
				this.playState.gameSetTime = 3000.0f;
				GameManager.instance.SceneController.playState = this.playState;
				GameManager.instance.SceneController.SetScene ("PlayScene");
			}
		}
	}
}
