using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//interface for scene
//every scene must have a class which inherit from this class
public class IScene : MonoBehaviour {
	//field store sceneName
	[SerializeField]
	protected string sceneName;
	//property make sceneName readonly
	public string SceneName { get { return sceneName; } }
	//store scenecontroller
	private SceneController sceneController = null;
	//property for scenecontroller
	public SceneController SceneController {
		set { if (sceneController == null) sceneController = value; }
		get { return sceneController; }
	}
	protected virtual void Start ( ) { }
	protected virtual void Update ( ) { }
}
