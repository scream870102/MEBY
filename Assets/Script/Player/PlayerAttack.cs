using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	private IPlayer parent = null;
	public IPlayer Parent { set { if (parent == null) parent = value; } get { return parent; } }

	[SerializeField]
	protected Collider2D hitCollider = null;
	protected bool bAttacknig = false;
	protected bool bInCD = false;
	protected float basicAttackSpeed = 0.0f;
	protected float basicAttackPoint = 0.0f;
	protected float basicAttackInterval = 0.0f;
	protected float timer = 0.0f;

	public virtual void Start ( ) {
		if (hitCollider != null)
			hitCollider.enabled = false;
		bAttacknig = false;
		if (parent != null) {
			basicAttackSpeed = parent.Props.basicAttackSpeed;
			basicAttackPoint = parent.Props.basicAttackPoint;
			basicAttackInterval = parent.Props.basicAttackInterval;
		}

	}

	// Update is called once per frame
	protected virtual void Update ( ) {
		if (Input.GetButtonDown (parent.NumPlayer + "Fire1") && hitCollider != null && !bInCD) {
			bAttacknig = true;
			hitCollider.enabled = true;
		}
		if (bAttacknig) {
			timer += Time.deltaTime;
			if (timer >= basicAttackSpeed) {
				bAttacknig = false;
				bInCD = true;
				timer = .0f;
				hitCollider.enabled = false;
			}
		}
		if (bInCD) {
			timer += Time.deltaTime;
			if (timer >= basicAttackInterval) {
				bInCD = false;
				timer = .0f;
			}
		}

	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && other.gameObject != this.transform.parent.gameObject) {
			OnAttack (other.gameObject.GetComponent<IPlayer> ( ));
		}
	}

	private void OnAttack (IPlayer player) {
		player.UnderAttack (this.basicAttackPoint);

	}
}
