using UnityEngine;
using System.Collections;
using DG.Tweening;

public class stage2_behaviour : MonoBehaviour {

	public GameObject light;
	public GameObject my_fg;
	public GameObject my_bg;
	// Use this for initialization
	void Start () {
		my_bg.GetComponent <Animator>().Play ("stage2_bg_0" + Random.Range (0,4));
		my_fg.GetComponent <Animator>().Play ("stage2_fg_0" + Random.Range (0,5));
		change_light_color ();
	
	}

	void change_light_color(){
		float r = Random.Range(0f, 0.8f);
		float g = Random.Range(0f, 0.8f);
		float b = Random.Range(0f, 0.8f);

		light.transform.GetComponent<SpriteRenderer>().color = new Color(r, g, b, 0.25f);
		Invoke("change_light_color", 0.7f);

		//light.transform.GetComponent<SpriteRenderer>().DOColor(new Color(r, g, b), 1f).OnComplete(() => change_light_color());
			//Debug.Log(" MY COLOR : " + transform.GetComponent<SpriteRenderer>().color);

	}


	// Update is called once per frame
	void Update () {
	
	}
}
