using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct heroInfo {
	public Texture texture;
	public string name;
}
public class SelectScene : IScene {
	public List<heroInfo> heroInfo;
	public List<RawImage> heroAvatar;
	public List<Text> heroName;
	private bool [ ] bJoin = { false, false, false, false };
	private bool [ ] bLock = { false, false, false, false };
	private float [ ] nowSelectedHero = { .0f, .0f, .0f, .0f };
	private float nowSelecteMap = 0.0f;
	private PlayState playState;
	protected override void Awake ( ) {
		base.Awake ( );
		sceneName = "SelectScene";
	}

	// Use this for initialization
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

	// Update is called once per frame
	protected override void Update ( ) {
		//Debug.Log(playState.numOfPlayers);
		SelectHero ("Player1", 0);
		SelectHero ("Player2", 1);
		SelectHero ("Player3", 2);
		SelectHero ("Player4", 3);
		MapSelected ( );
		PressStart ( );
	}

	private void SelectHero (string player, int playerNum) {

		if (Input.GetButtonUp (player + "Jump") && !bJoin [playerNum]) {
			bJoin [playerNum] = true;
			playState.numOfPlayers++;
		}
		else if (bJoin [playerNum] && !bLock [playerNum]) {
			float selectedHero = Input.GetAxisRaw (player + "Horizontal");
			nowSelectedHero [playerNum] += selectedHero;
			if ((int) nowSelectedHero [playerNum] >= (int) EHero.NONE)
				nowSelectedHero [playerNum] = (float) EHero.MASANARI;
			if (nowSelectedHero [playerNum] < 0.0f)
				nowSelectedHero [playerNum] = (float) (System.Enum.GetNames (typeof (EHero)).Length - 2);
			heroAvatar [playerNum].texture = heroInfo [(int) nowSelectedHero [playerNum]].texture;
			heroName [playerNum].text = heroInfo [(int) nowSelectedHero [playerNum]].name;
			if (Input.GetButtonUp (player + "Jump"))
				bLock [playerNum] = true;
		}

		else if (bLock [playerNum]) {
			playState.heroes [playerNum] = (EHero) ((int) nowSelectedHero [playerNum]);
		}
	}

	private void MapSelected ( ) {
		float selectedMap = Input.GetAxisRaw ("Player1Vertical");
		nowSelecteMap += selectedMap;
		if (nowSelecteMap > (float) (System.Enum.GetNames (typeof (EMap)).Length - 2))
			nowSelecteMap = 0.0f;
		if (nowSelecteMap < 0.0f)
			nowSelecteMap = (float) ((int) System.Enum.GetNames (typeof (EMap)).Length - 2);

	}

	private void PressStart ( ) {
		if (Input.GetButtonDown ("Player1Option")) {
			bool allCheck = false;
			for (int i = 0; i < playState.numOfPlayers; i++) {
				allCheck = bLock [i] ? true : false;
			}
			if (allCheck) {
				this.playState.map = (EMap) nowSelecteMap;
				GameManager.instance.SceneController.playState = this.playState;
				GameManager.instance.SceneController.SetScene ("PlayScene");
			}
		}
	}
}
