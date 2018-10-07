using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IScene : MonoBehaviour {
	[SerializeField]
	protected string sceneName;
	public string SceneName { get { return sceneName; } }
	private SceneController sceneController = null;

	public SceneController SceneController {
		set { if (sceneController == null) sceneController = value; }
		get { return sceneController; }
	}
	// Use this for initialization
	protected virtual void Start ( ) {

	}

	// Update is called once per frame
	protected virtual void Update ( ) {

	}
}
