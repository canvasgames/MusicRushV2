using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GiftButton : MonoBehaviour {
	public GameObject my_time, my_open_now_text ,my_glow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCountownState(){
//		Debug.Log ("gift button: SET COUNTDOWN STATE");
		my_time.SetActive (true);
		my_open_now_text.SetActive (false);
//		my_glow.SetActive (false);

		GetComponent<Button> ().interactable = false;

		//myBt.interactable = false;
	}

	public void SetGetNowState(){
//		Debug.Log ("gift button: SET GET NOW STATE");

		my_time.SetActive (false);
		my_open_now_text.SetActive (true);
//		my_glow.SetActive (true);

		GetComponent<Button> ().interactable = true;


		//myBt.interactable = true;

	}
}
