using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ModeScene : IScene {
	//enum define mode
	enum MODE {
		FIGHTING = 0,
		RACING = 1,
		STORY = 2,
		EXIT = 3
	}
	//field to save current select mode
	private MODE mode;
	//field to save pre Input
	private float preChosen;
	//field to save current Input
	private float nowChosen;
	//ref for mode Text
	[SerializeField]
	private List<Text> modeText;
	//ref for option image RectTransform
	[SerializeField]
	private RectTransform optionTransform;
	//ref for negative option color
	[SerializeField]
	private Color negativeColor;
	//ref for positive option color
	[SerializeField]
	private Color positiveColor;
	protected override void Awake ( ) {
		base.Awake ( );
		sceneName = "ModeScene";
		mode = MODE.FIGHTING;
	}

	protected override void Start ( ) { }
	//if space key got down enter to select scene
	protected override void Update ( ) {
		//get player Input 
		GetInput ( );
		//render all ui
		Render ( );
		if (Input.GetButtonDown ("Player1Jump")&&mode==MODE.EXIT) {
			Application.Quit();
		}
		else if(Input.GetButtonDown("Player1Jump")){
			GameManager.instance.SceneController.SetScene ("SelectScene");
		}
		
	}

	private void GetInput ( ) {
		//get player input from getAxisRaw and if it is diffrenet from pre frame then change the mode
		nowChosen = Input.GetAxisRaw ("Player1Horizontal");
		if (preChosen != nowChosen) {
			mode = mode + (int) nowChosen;
		}
		//check if player chosen is in the range
		if ((int) mode > (int) MODE.EXIT)
			mode = MODE.FIGHTING;
		else if ((int) mode < 0)
			mode = (MODE) (System.Enum.GetNames (typeof (MODE)).Length - 1);
		preChosen = nowChosen;
	}

	//render all text color and move option button to current select option
	private void Render ( ) {
		for (int i = 0; i < modeText.Count; i++) {
			if ((int) mode != i) {
				modeText [i].color = negativeColor;
			}
			else {
				modeText [i].color = positiveColor;
				optionTransform.position = new Vector3 (optionTransform.position.x, modeText [i].rectTransform.position.y, 0.0f);
			}
		}
	}
}
