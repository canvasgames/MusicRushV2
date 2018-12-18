using UnityEngine;
using System.Collections;

public class Stage3Base2Behaviour : MonoBehaviour {
	[SerializeField] Sprite[] mySprites;
	// Use this for initialization
	void Start () {
//		GetComponent<Animator> ().Play ("stage3_base2_anim_" + Random.Range (1, 6));
		GetComponent<SpriteRenderer>().sprite = mySprites[Random.Range(0,mySprites.Length)];
	
	}
	
	// Update is called once per frame
}
