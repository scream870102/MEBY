using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//神奇嗨螺
public class Conch : IItem {
	//Enum for conch state
	enum CONCH_STATE
	{
		//temp state for conch to set mermaidman position
		INIT,
		//state when mermaidman active
		FLY,
	}
	//ref for main camera 
	private new Camera camera;
	//field for store current conch state
	private CONCH_STATE conchState;
	//ref for the mermaidman which belongs to this conch
	[SerializeField]
	private MermaidMan mermaidMan;
	//Find the main camera 
	protected override void Awake ( ) {
		base.Awake ( );
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ( );
	}
	//get props from gameManager
	protected override void Start ( ) {
		props = GameManager.instance.attribution.allItemProps [(int) EItem.CONCH];
		conchState=CONCH_STATE.INIT;
		mermaidMan.gameObject.SetActive(false);
	}

	public override void UsingItem ( ) {
		switch (conchState)
		{
			//set mermaid man position and active it
			//change conch state to FLY
			case CONCH_STATE.INIT:
				Vector3 initPos=camera.ViewportToWorldPoint(new Vector3(1.0f,Random.Range(0.0f,1.0f),camera.nearClipPlane));
				mermaidMan.SetPos(initPos);
				mermaidMan.gameObject.SetActive(true);
				conchState=CONCH_STATE.FLY;
				break;
			//keep tracking mermaid man position if out of viewport deactivate it 
			case CONCH_STATE.FLY:
				if(camera.WorldToViewportPoint(mermaidMan.Position).x<=-0.5f){
					mermaidMan.gameObject.SetActive(false);
					mermaidMan.Init();
					BeforeEndState();
				}
				break;
		}

	}
	//reset conch state to init and init the conch
	protected override void BeforeEndState ( ) {
		conchState=CONCH_STATE.INIT;
		InitUnuse ( );
	}
}



