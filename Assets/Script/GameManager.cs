using System.Collections;
using System.Collections.Generic;

using SubjectNerd.Utilities;

using UnityEngine;

public class GameManager : MonoBehaviour {
	[HideInInspector]
	public static GameManager instance = null;
	protected SceneController sceneController = null;
	public Attribution attribution;

	// Use this for initialization
	void Awake ( ) {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	void Start ( ) {
		sceneController = GetComponent<SceneController> ( );
	}

	// Update is called once per frame
	void Update ( ) {

	}
}
