using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SuperJumpEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//GetComponent<Animator> ().Stop();
		//Invoke ("InitMe", Random.Range(0f, 0.5f));
		//GetComponent<Animator> ().Play ("SuperJumpEffectAnimation", 0, Random.Range(0f, 1f));
		float random = Random.Range(0f, 1f);
		//Debug.Log ("random: " + random);
		Invoke("restart", random);
	}

	public void restart(){
		transform.localPosition = new Vector2 (transform.localPosition.x, 0);
		transform.DOLocalMoveY(transform.localPosition.y -10f, 0.3f).OnComplete(restart);

	}

	public void InitMe(){
		//Debug.Log ("start engine");
		//GetComponent<Animator> ().SetTrigger ("play");
		GetComponent<Animator> ().SetBool("AndaCavalo", true);

		//GetComponent<Animator> ().Play ("SuperJumpEffectAnimation");
		//GetComponent<Animator> ().pl;

	}
	// Update is called once per frame
	void Update () {
	
	}
}