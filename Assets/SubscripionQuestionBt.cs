using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscripionQuestionBt : MonoBehaviour {


	public Text myText;

	void OnEnable(){
		if (PlayerPrefs.GetInt ("subs_clicked", 0) == 0) {

		} else {
			myText.text = "Thank you very much for your feedback!!";
			GetComponent<Button> ().interactable = false;
		}
	}

	public void OnMyselfClicked(){
//		AnalyticController.s.ReportAdAction (); // tbd
		myText.text = "Thank you very much for your feedback!!";
		GetComponent<Button> ().interactable = false;
		PlayerPrefs.SetInt ("subs_clicked", 1);
	}

}
