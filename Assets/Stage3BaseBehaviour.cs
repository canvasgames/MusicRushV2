using UnityEngine;
using System.Collections;

public class Stage3BaseBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int anim = Random.Range (0, 7);
		if(anim !=0)
			GetComponent<SimpleAnimation> ().Play ("Default_" + Random.Range (1, 7));
//		else
//			GetComponent<SimpleAnimation> ().Play ("Default_" + Random.Range (1, 7));

//		GetComponent<SimpleAnimation> ().Play(Random.Range(0,7).ToString());
	}
}
