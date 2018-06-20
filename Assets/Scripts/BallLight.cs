using UnityEngine;
using System.Collections;

public class BallLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(globals.s.BALL_X, globals.s.BALL_Y);
	}
}
