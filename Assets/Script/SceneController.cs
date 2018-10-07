using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
	private string sceneName;
	[SerializeField]
	private IScene currentScene = null;
	private bool bGetScene;
	public PlayState playState;
	void Start ( ) {
		bGetScene = false;
		InitScene ("PlayScene");
	}

	// Update is called once per frame
	void Update ( ) {
		if (!bGetScene && currentScene != null && currentScene.SceneName != sceneName)
			GetIScene ( );

	}
	public void SetScene (string sceneName) {
		SceneManager.LoadScene (sceneName);
		this.sceneName = sceneName;
		bGetScene = false;

	}
	private void GetIScene ( ) {
		currentScene = GameObject.Find (sceneName + "Controller").GetComponent<IScene> ( );
		if (currentScene != null && currentScene.name == sceneName)
			bGetScene = true;
	}

	public void InitScene (string sceneName) {
		this.sceneName = sceneName;
		currentScene = GameObject.Find (this.sceneName + "Controller").GetComponent (sceneName) as IScene;
		currentScene.SceneController = this;
		//test
		playState = Test ( );
	}

	//for test
	private PlayState Test ( ) {
		PlayState temp;
		temp.gameMode = EGameMode.SURVIVOR;
		temp.map = EMap.SUNNY_LAND;
		temp.numOfPlayers = 1;
		temp.heroes = new List<EHero> ( );
		temp.heroes.Add (EHero.MASANARI);
		return temp;
	}
}
