using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FlareArrow : MonoBehaviour {
	//ref for flare parent (Player)
	[SerializeField]
	private GameObject parent;
	//ref for parent IPlayer 
	[SerializeField]
	private IPlayer parentIPlyaer;
	//ref for self rigidbody
	[SerializeField]
	private Rigidbody2D rb;
	//define how the force add to arrow when parent use skill
	public Vector2 force;
	//define how long does this debuff exist to other player
	public float arrowActionTime;
	//define arrow origial scale
	public Vector3 originalScale;

	//when player hit use 
	//reset position and set scale due to player's direction also add force to arrow
	//and let arrow parent not equal to player
	public void Use ( ) {
		if (rb != null) {
			transform.position = parent.transform.position;
			transform.parent = null;
			rb.velocity = new Vector2 (0.0f, 0.0f);
			if (parentIPlyaer.IsPlayerFacingRight ) {
				transform.localScale = originalScale;
				rb.AddForce (force);
			}
			else {
				Vector2 temp;
				Vector3 tempScale = originalScale;
				tempScale.x *= -1;
				transform.localScale = tempScale;
				temp = force;
				temp.x *= -1;
				rb.AddForce (temp);
			}
		}

	}

	//when arrow hit player stop that player action also reset arrow player
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && other.gameObject != parent) {
			//freeze other player
			other.gameObject.GetComponent<IPlayer> ( ).StopAction (arrowActionTime);
			transform.parent = parent.transform;
			this.gameObject.SetActive (false);
		}
		else if (other.gameObject != parent) {
			transform.parent = parent.transform;
			this.gameObject.SetActive (false);
		}
	}
}
