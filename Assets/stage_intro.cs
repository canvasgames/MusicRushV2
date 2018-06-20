using UnityEngine;
using System.Collections;
using DG.Tweening;
//using UnityEngine.UI

public class stage_intro : MonoBehaviour {

	public static stage_intro s;
	public GameObject bg;
	public GameObject[] title;
	public GameObject[] stars;

	[SerializeField] TextMesh newTitle, nStage;
	int star_i;
	int title_i = 0;

	// Use this for initialization
	void Start () {
		//Invoke ("StartEntering", 0.6f);
		s = this;
		//StartEntering (1);

	}

	public void StartEntering(int stage){
		title_i = stage - 1;
		//Debug.Log ("TITLE I: " + title_i);
//		if (title_i < title.Length) {
//
//			foreach (GameObject t in title)
//				t.SetActive (false);
//			star_i = 0;
//			foreach (GameObject st in stars)
//				st.SetActive (false);
//
//			transform.localPosition = new Vector3 (0, 9.6f, 1);
//			transform.DOLocalMoveY (5.1f, 0.35f).SetEase (Ease.OutCubic).OnComplete (EnteringFinished);
//		}

		transform.localPosition = new Vector3 (0, 9.6f, 1);
		transform.DOLocalMoveY (4.9f, 0.3f).SetEase (Ease.OutCubic).OnComplete (EnteringFinished);

		nStage.text = stage.ToString ();
		if (stage == 1) {
			newTitle.text = "GARAGE\nBAND";
		}
		if (stage == 2) {
			newTitle.text = "STUDIO\nTRIP";
		}
		if (stage == 3) {
			newTitle.text = "SHOW\nBAR";
		}
		if (stage == 4) {
			newTitle.text = "DISCO\nPARTY";
		}
		if (stage == 5) {
			newTitle.text = "MEGA\nSHOW";
		}

		newTitle.gameObject.SetActive (false);
	}

	void EnteringFinished(){
//		title[title_i].SetActive (true);
		//Debug.Log ("LOCAL X " + title[title_i].transform.localPosition.x +  " SIZE X: "  + title[title_i].GetComponent<SpriteRenderer> ().bounds.size.x );
//		title[title_i].transform.localPosition = new Vector3 (title[title_i].transform.localPosition.x + 2,
//			title[title_i].transform.localPosition.y, title[title_i].transform.localPosition.z);

		newTitle.gameObject.SetActive (true);

		newTitle.transform.localPosition = new Vector3 (newTitle.transform.localPosition.x + 2,
			newTitle.transform.localPosition.y, newTitle.transform.localPosition.z);

		newTitle.transform.DOLocalMoveX (newTitle.transform.localPosition.x  -2, 0.23f).SetEase (Ease.OutCubic);
		ShowStars ();
		Invoke ("LeaveScreen", 1.1f);
	}

	void ShowStars(){
//		Invoke ("ShowSingleStar", 0.25f);
//		Invoke ("ShowSingleStar", 0.4f);
//		Invoke ("ShowSingleStar", 0.55f);
	}

	void ShowSingleStar(){
		stars [star_i].SetActive (true);
		star_i++;
	}


	void LeaveScreen(){
		transform.DOLocalMoveY (10f, 0.5f);
	}

}
