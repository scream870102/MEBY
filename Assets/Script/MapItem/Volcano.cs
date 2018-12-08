using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour {
	[SerializeField]
	private GameObject paintBall;
	private float timer=0.0f;
	[SerializeField]
	private float paintBallTimeInterval;
	[SerializeField]
	private Transform spawnPos;
	[SerializeField]
	private Vector2 force;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		if(timer>=paintBallTimeInterval){
			SpawnPaintball();
			timer=0.0f;
		}
		
	}

	private void SpawnPaintball(){
		GameObject temp=Instantiate(paintBall,spawnPos);
		Rigidbody2D rb=temp.GetComponent<Rigidbody2D>();
		if(rb!=null){
			Vector2 tempForce=new Vector2(Random.Range(-force.x,force.x),Random.Range(force.y/2,force.y));
			rb.AddForce(tempForce);
		}
	}
}
