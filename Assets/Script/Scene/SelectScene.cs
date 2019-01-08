using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

//select scene is scene for select hero and map
public class SelectScene : IScene {
	//heroInfo include texture and name
	[System.Serializable]
	struct HERO_INFO {
		public Sprite texture;
		public string name;
		public float attack;
		public float attackSpeed;
		public float speed;
		public float healthPoint;
	}

	[System.Serializable]
	struct SLIDER {
		public Slider attack;
		public Slider attackSpeed;
		public Slider speed;
		public Slider healthPoint;
	}
	//const hero info for all hero
	[SerializeField]
	private List<HERO_INFO> heroInfo;
	//ref for heroAvatar
	public List<Image> heroAvatar;
	//ref for heroName
	public List<Text> heroName;
	[SerializeField]
	private List<SLIDER> sliders;
	//field to detect if player join the game
	private bool [ ] bJoin = { false, false, false, false };
	//field to detect if joined player already select hero and locked
	private bool [ ] bLock = { false, false, false, false };
	private float [ ] beforeSelectHero = { .0f, .0f, .0f, .0f };
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
			float selectedHero = Input.GetAxisRaw (player + "Horizontal");
			if (selectedHero != beforeSelectHero [playerNum]) {
				nowSelectedHero [playerNum] += selectedHero;
				ResetSlider (playerNum);
			}
			beforeSelectHero [playerNum] = selectedHero;
			//check if player chosen is in the range
			if ((int) nowSelectedHero [playerNum] >= (int) EHero.NONE)
				nowSelectedHero [playerNum] = (float) EHero.MASANARI;
			else if (nowSelectedHero [playerNum] < 0.0f)
				nowSelectedHero [playerNum] = (float) (System.Enum.GetNames (typeof (EHero)).Length - 2);
			//set avater and name UI component
			Render (playerNum);
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
	//render avtar and set name
	//keep all slider value until it reach the setting value
	private void Render (int playerNum) {
		heroAvatar [playerNum].sprite = heroInfo [(int) nowSelectedHero [playerNum]].texture;
		heroName [playerNum].text = heroInfo [(int) nowSelectedHero [playerNum]].name;
		if (sliders [playerNum].attack.value <= heroInfo [(int) nowSelectedHero [playerNum]].attack) {
			sliders [playerNum].attack.value += heroInfo [(int) nowSelectedHero [playerNum]].attack*2.0f*Time.deltaTime;
		}
		if (sliders [playerNum].attackSpeed.value <= heroInfo [(int) nowSelectedHero [playerNum]].attackSpeed) {
			sliders [playerNum].attackSpeed.value += heroInfo [(int) nowSelectedHero [playerNum]].attackSpeed*2.0f*Time.deltaTime;
		}
		if (sliders [playerNum].speed.value <= heroInfo [(int) nowSelectedHero [playerNum]].speed) {
			sliders [playerNum].speed.value += heroInfo [(int) nowSelectedHero [playerNum]].speed*2.0f*Time.deltaTime;
		}
		if (sliders [playerNum].healthPoint.value <= heroInfo [(int) nowSelectedHero [playerNum]].healthPoint) {
			sliders [playerNum].healthPoint.value += heroInfo [(int) nowSelectedHero [playerNum]].healthPoint*2.0f*Time.deltaTime;
		}
	}
	
	//reset all slider value to zero call if player select another hero
	private void ResetSlider (int playerNum) {
		sliders [playerNum].attack.value = .0f;
		sliders [playerNum].attackSpeed.value = .0f;
		sliders [playerNum].speed.value = .0f;
		sliders [playerNum].healthPoint.value = .0f;
	}
}
