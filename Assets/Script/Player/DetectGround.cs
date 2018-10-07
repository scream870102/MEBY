using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DetectGround : MonoBehaviour {
	private bool isGround = false;
	public bool IsGround { get { return isGround; } }
	// Use this for initialization

	private void OnTriggerEnter2D (Collider2D other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Ground")
			isGround = true;
	}
	private void OnTriggerExit2D (Collider2D other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Ground")
			isGround = false;
	}
}
