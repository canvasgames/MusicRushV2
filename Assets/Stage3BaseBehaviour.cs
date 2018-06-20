using UnityEngine;
using System.Collections;

public class Stage3BaseBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().Play ("stage3_base_animation_" + Random.Range (1, 7));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
