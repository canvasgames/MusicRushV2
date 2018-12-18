using UnityEngine;
using System.Collections;
using DG.Tweening;

public class note_trail_behavior : MonoBehaviour {

	public bool active = false;
	// Use this for initialization
	void Start () {
        /*GetComponent<Animator>().Play("note_" + Random.Range(1, 11));
        transform.DOMoveY(transform.position.y + Random.Range(0.3f, 1.2f), 1f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30,30));
        // GetComponent<Animator>().Stop();
        transform.DORotate(new Vector3(0, 0, Random.Range(-30, 30)), 0.35f).OnComplete(() => transform.DORotate(new Vector3(0, 0, Random.Range(-30, 30)), 0.35f));

        Invoke("destroy_me", 0.8f);*/
	
	}

	public void Init(){
		active = true;
		GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);
		GetComponent<Animator>().Play("note_" + Random.Range(1, 11));
		transform.DOMoveY(transform.position.y + Random.Range(0.3f, 1.2f), 1f);
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30,30));
		// GetComponent<Animator>().Stop();
		transform.DORotate(new Vector3(0, 0, Random.Range(-30, 30)), 0.35f).OnComplete(() => transform.DORotate(new Vector3(0, 0, Random.Range(-30, 30)), 0.35f));
		Invoke ("destroy_me", 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void destroy_me() {
		GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() => active = false);
        
    }
}
