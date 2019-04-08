using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[RequireComponent (typeof (Camera))]
public class MultipleTargetCamera : MonoBehaviour {
	//ref for camera
	private new Camera camera;
	//ref for targets
	[SerializeField]
	private List<Transform> targets;
	//tje position offset for camera between targets' center point
	[SerializeField]
	private Vector3 offset;
	//ref for smoothDamp
	private Vector3 velocity;
	//move smoothTime default is 0.5f
	[SerializeField]
	private float smoothTime;
	//max of camera orthograpgic size default is 3.0f
	[SerializeField]
	private float maxZoom;
	//min of camera orthographic size default is 10.0f
	[SerializeField]
	private float minZoom;
	[SerializeField]
	private float zoomLimiter;
	//ref for playerController which is purpose to get all player ref
	[SerializeField]
	private PlayerController playerController;
	//get camera component and get all player transform ref from playerController then add to targets
	void Start ( ) {
		camera = GetComponent<Camera> ( );
		targets.AddRange (playerController.GetAllPlayersTransfrom ( ));
	}

	private void LateUpdate ( ) {
		//if it is not already add try to get all player transform from playerController
		if (targets.Count == 0) {
			targets.AddRange (playerController.GetAllPlayersTransfrom ( ));
			return;
		}
		Move ( );
		Zoom ( );
	}

	//move the camera smoothly to the center point between all targets
	private void Move ( ) {
		Vector3 centerPoint = GetCenterPoint ( );
		Vector3 newPos = Vector3.SmoothDamp (transform.position, centerPoint, ref velocity, smoothTime) + offset;
		transform.position = newPos;
	}

	//calculate the camera orthographic size and set it
	private void Zoom ( ) {
		float newZoom = Mathf.Lerp (maxZoom, minZoom, GetGreatestDistance ( ) / zoomLimiter);
		camera.orthographicSize = Mathf.Lerp (camera.orthographicSize, newZoom, Time.deltaTime);
	}

	//use bound to get center point between all targets
	private Vector3 GetCenterPoint ( ) {
		Bounds bound = new Bounds (targets [0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++) {
			bound.Encapsulate (targets [i].position);
		}
		return bound.center;
	}

	//use bound to get width which represent the greatest distance between two targets
	private float GetGreatestDistance ( ) {
		Bounds bound = new Bounds (targets [0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++) {
			bound.Encapsulate (targets [i].position);
		}
		if(bound.size.x>bound.size.y)
			return bound.size.x;
		else
			return bound.size.y;
	}
}
