using UnityEngine;
using System.Collections;

public class NewStageStarBehaviour : MonoBehaviour {
	float randStart;
	// Use this for initialization
	void Start () {
		randStart = Random.Range (0.2f, 0.5f);
		StartCoroutine (ChangePos ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator ChangePos(){
//		Debug.Log ("ranaaaaaaaaadasd " + randStart);
		yield return new WaitForSeconds (randStart);
//		yield return new WaitForSeconds (0.23f);

		transform.localPosition = new Vector2 (transform.localPosition.x + 0.3f, transform.localPosition.y);

		yield return new WaitForSeconds (0.1f);

		transform.localPosition = new Vector2 (transform.localPosition.x - 0.15f, transform.localPosition.y);

		yield return new WaitForSeconds (0.10f);

		transform.localPosition = new Vector2 (transform.localPosition.x - 0.15f, transform.localPosition.y);

		randStart = 0.15f;


		StartCoroutine (ChangePos ());
	}
}
