using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MermaidMan : MonoBehaviour {
	//define mermaid man state
	enum MERMAID_MAN_STATE {
		//init state which is to set position
		SETTING,
		//state after SETTING then mermaid man will keep move 
		FLYING,
	}
	//field which store current state for mermaidMan
	private MERMAID_MAN_STATE state;
	//const define how fast will mermaidMan move
	[SerializeField]
	private float velocity;
	//const define how many damage will cause to other player
	[SerializeField]
	private float attackPoint;
	//ref for rigidbody
	private Rigidbody2D rb;
	//ref for collider which is trigger can detect what enter the detectable area
	private Collider2D col;
	//ref for sprite renderer
	private SpriteRenderer rend;
	//ref for mermaid man parent
	[SerializeField]
	private Conch parent;
	//public property for parent to get the mermaid man position
	public Vector3 Position { get { return this.transform.position; } }
	
	// Get ref and init state and position
	void Start ( ) {
		rb = GetComponent<Rigidbody2D> ( );
		col=GetComponent<Collider2D>();
		rend=GetComponent<SpriteRenderer>();
		Init ( );
	}

	//if state is flying keep move the mermaid man
	void FixedUpdate ( ) {
		if (state == MERMAID_MAN_STATE.FLYING) {
			Vector2 newPos = new Vector2 (rb.position.x - velocity * Time.fixedDeltaTime, rb.position.y);
			rb.MovePosition (newPos);
		}
	}

	//public method for conch call this if be called change state to flying and set position
	public void SetPos (Vector3 pos) {
		transform.position = pos;
		col.enabled=true;
		rend.enabled=true;
		transform.parent=null;
		state = MERMAID_MAN_STATE.FLYING;
	}
	//public method for conch class to call if be called will reset the state and disable collider and renderer
	public void Init ( ) {
		state = MERMAID_MAN_STATE.SETTING;
		col.enabled = false;
		rend.enabled = false;
		transform.parent=parent.transform;
	}

	//if collider enter it and which is other player cause damage to the player
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player" && other.gameObject != parent.Owner.gameObject) {
			other.GetComponent<IPlayer>().UnderAttack(attackPoint);
		}
	}
}
