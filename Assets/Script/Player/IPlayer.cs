using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//a interface for all player
//every hero class must inherit from this class
public class IPlayer : MonoBehaviour {
	//ref for footobject make playermovement can detect ground
	//protected GameObject footObject = null;
	//property for footobject make it readonly
	//public GameObject FootObject { get { return footObject; } }
	private bool bInit = false;
	public bool Init { set { if (bInit == false) bInit = true; } }
	//field to save which hero it is
	private EHero hero = EHero.NONE;
	//property for hero
	protected EHero Hero {
		get { return hero; }
		set { if (hero == EHero.NONE) hero = value; }
	}
	private EColor color = (EColor) 1000;
	public EColor Color {
		get { return color; }
		set { if ((int) color == 1000) color = value; }
	}
	//ref for player movement
	//player movement define how character move
	protected PlayerMovement movement = null;
	//ref for player attack
	//player attack define how character attack
	protected PlayerAttack attack = null;
	//ref for player health
	//player health define about health for character
	protected PlayerHealth health = null;
	//ref for player paintball
	//define how paintball react
	protected PlayerPaintball paintball = null;
	//ref for player Skill
	[SerializeField]
	protected ISkill skill=null;
	//ref for playerUI
	protected PlayerUI UI = null;
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
	public virtual void Start ( ) {
		if (bInit) {
			movement = GetComponent<PlayerMovement> ( );
			attack = GetComponent<PlayerAttack> ( );
			health = GetComponent<PlayerHealth> ( );
			paintball = GetComponent<PlayerPaintball> ( );
			skill = GetComponent<ISkill> ( );
			UI = GameObject.Find (numPlayer + "UI").GetComponent<PlayerUI> ( );
			skill.Parent = this;
			skill.SetActive(true);
			UI.Parent = this;
			health.Parent = this;
			attack.Parent = this;
			movement.Parent = this;
			paintball.Parent = this;
			UI.Start ( );
			attack.Start ( );
			movement.Start ( );
			health.Start ( );
			paintball.Start ( );
			//footObject = transform.Find ("Foot").gameObject;
		}

	}
	protected virtual void Update ( ) { }

	//public method to set player num string 
	public void SetNumPlayer (int num) {
		numPlayer = "Player" + num.ToString ( );
	}

	//public method make other objects call when player get damage
	public virtual void UnderAttack (float damage) {
		health.TakeDamage (damage);
	}

	//public method for other class to get palyer cureent health
	public virtual float GetHealth ( ) {
		return health.GetHealth ( );
	}

	//public method for other class to get next painball Color
	public EColor GetNextPaintBallColor ( ) {
		return paintball.GetPaintballColor ( );
	}

	//public method for other class to get what direction is player current facing
	public bool IsPlayerFacingRight ( ) {
		return movement.IsPlayerFacingRight ( );
	}

	//Stop Player attack movement paintball and skill action for specific seconds
	public void StopAction(float second){
		attack.enabled=false;
		movement.enabled=false;
		paintball.enabled=false;
		skill.enabled=false;
		StartCoroutine(StopActionCoroutine(second));
	}

	//Coroutine for stop player action
	IEnumerator StopActionCoroutine ( float stopSecond) {
		yield return new WaitForSeconds (stopSecond);
		attack.enabled=true;
		movement.enabled=true;
		paintball.enabled=true;
		skill.enabled=true;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {
		if(other.name=="DeadZone"){
			PlayerDead();
		}
	}
	public virtual void PlayerDead(){
		gameObject.SetActive(false);
		UI.gameObject.SetActive(false);
		Debug.Log(gameObject.name+"Dead");
	}


}
