using System.Collections;
using System.Collections.Generic;

using SubjectNerd.Utilities;

using UnityEngine;

public class GameManager : MonoBehaviour {
	//static ref for this class
	[HideInInspector]
	public static GameManager instance = null;
	//ref for sceneController
	protected SceneController sceneController = null;
	public SceneController SceneController { get { return sceneController; } }
	//attribution include all map character and props of all object
	public Attribution attribution;
	void Awake ( ) {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}
	protected virtual void Start ( ) {
		sceneController = GetComponent<SceneController> ( );
	}
	protected virtual void Update ( ) { }

}
