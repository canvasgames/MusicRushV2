using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFloorCollider : MonoBehaviour {
	public ball_hero myMonball;

//	void OnCollisionEnter2D(Collision2D coll){
////	void OnTriggerEnter2D(Collider2D coll){
//		myMonball.OnMyFloorCollider (coll);
//	}

	void OnTriggerEnter2D(Collider2D coll){
		myMonball.OnCircleColliderTriggered(coll);
	}
}
