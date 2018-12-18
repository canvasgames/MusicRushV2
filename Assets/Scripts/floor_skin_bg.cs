using UnityEngine;
using System.Collections;
using DG.Tweening;

public class floor_skin_bg : MonoBehaviour {

	// Use this for initialization
	void Start () {
       // new_color();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void new_color(float r, float g, float b, float time) {
       // float r = Random.Range(0f, 0.8f);
        //float g = Random.Range(0f, 0.8f);
        //float b = Random.Range(0f, 0.8f);

        //transform.GetComponent<SpriteRenderer>().color = new Color(r, g, b);

        //Invoke("new_color", 0.2f);
        transform.GetComponent<SpriteRenderer>().DOColor(new Color(r, g, b), time);
        //Debug.Log(" MY COLOR : " + transform.GetComponent<SpriteRenderer>().color);
    }
}
