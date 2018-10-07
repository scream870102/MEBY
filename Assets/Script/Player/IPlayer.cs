using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IPlayer : MonoBehaviour {
	[SerializeField]
	private EHero hero = EHero.NONE;
	protected EHero Hero {
		get { return hero; }
		set { if (hero == EHero.NONE) hero = value; }
	}
	protected PlayerMovement movement = null;
	protected GameObject footObject = null;
	public GameObject FootObject { get { return footObject; } }
	private PlayerProps props;
	private string numPlayer;
	public string NumPlayer { get { return numPlayer; } }
	public PlayerProps Props {
		set { this.props = value; }
		get { return this.props; }
	}
	// Use this for initialization
	protected virtual void Start ( ) {
		movement = GetComponent<PlayerMovement> ( );
		movement.Parent = this;
		movement.Start ( );
		footObject = transform.Find ("Foot").gameObject;
	}

	// Update is called once per frame
	protected virtual void Update ( ) {

	}

	public void SetNumPlayer (int num) {
		numPlayer = "Player" + num.ToString ( );
	}

}
