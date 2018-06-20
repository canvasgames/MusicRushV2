using UnityEngine;
using System.Collections;

public class Stage3Base2Behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().Play ("stage3_base2_anim_" + Random.Range (1, 6));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
