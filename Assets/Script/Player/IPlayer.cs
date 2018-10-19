using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//a interface for all player
//every hero class must inherit from this class
public class IPlayer : MonoBehaviour {
	//field to save which hero it is
	[SerializeField]
	private EHero hero = EHero.NONE;
	//property for hero
	protected EHero Hero {
		get { return hero; }
		set { if (hero == EHero.NONE) hero = value; }
	}
	//ref for player movement
	//player movement define how character move
	protected PlayerMovement movement = null;
	//ref for player attack
	//player attack define how character attack
	protected PlayerAttack attack = null;
	//ref for player health
	//player health define about health for character
	protected PlayerHealth health=null;
	//ref for footobject make playermovement can detect ground
	protected GameObject footObject = null;
	//property for footobject make it readonly
	public GameObject FootObject { get { return footObject; } }
	//props store all player property from game manager attribution
	private PlayerProps props;
	//property for props
	public PlayerProps Props {
		set { this.props = value; }
		get { return this.props; }
	}
	//string store number of player make player can get right button
	private string numPlayer;
	//property for numplayer make it readonly
	public string NumPlayer { get { return numPlayer; } }
	//ref for rigidbody
	protected Rigidbody2D rb;
	//to get player movement and set its parent
	//call movement start method
	//find foot object
	protected virtual void Start ( ) {
		movement = GetComponent<PlayerMovement> ( );
		attack = transform.Find ("Hand").GetComponent<PlayerAttack> ( );
		health=GetComponent<PlayerHealth>();
		health.Parent=this;
		attack.Parent = this;
		movement.Parent = this;
		attack.Start ( );
		movement.Start ( );
		health.Start();
		footObject = transform.Find ("Foot").gameObject;
	}
	protected virtual void Update ( ) { }
	//public method to set player num string 
	public void SetNumPlayer (int num) {
		numPlayer = "Player" + num.ToString ( );
	}

	public virtual void UnderAttack (float damage) {
		Debug.Log (this.gameObject.name + " get " + damage);
		health.TakeDamage(damage);
	}

}
