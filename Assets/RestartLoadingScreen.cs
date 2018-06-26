using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class RestartLoadingScreen : MonoBehaviour {

	[SerializeField] GameObject loadingSymbol;
	[SerializeField] Text motivationalPhrase;
	[SerializeField] Text movivationalPhraseAuthor;
	// Use this for initialization

	public void InitMe(bool firstPhrase = false){
		float tempo = UnityEngine.Random.Range (2f, 2.6f);
		float angle = UnityEngine.Random.Range  (-1, -360);
		//		float force = UnityEngine.Random.Range (1,2);
		float force = 60f;
		angle = angle * (force);
		loadingSymbol.transform.DORotate (new Vector3 (0, 0, angle), 4f, RotateMode.WorldAxisAdd);

		DecidePhrase ();

	}

	void DecidePhrase(bool firstPhrase = false){
		int i = Random.Range (1, 6);
		string text = "";
		string author = "";
			

		if (i == 1) {
			text = "Get up! Stand up! Don't give up the fight!";
			author = "Bob Marley";
		} else if (i == 2) {
			text = "Never gonna give you up! Never gonna let you down!";
			author = "ddd";
		} else if (i == 3) {
			text = "I will survive! I will survive! Eh-he!";
			author = "Gloria Gaynor";
		} else if (i == 4) {
			text = "I will survive! I will survive! Eh-he!";
			author = "Gloria Gaynor";
		} else if (i == 5) {
			text = "Work it, make it do it, makes us harder, better, stronger";
			author = "Daft Punkr";
		}

		motivationalPhrase.text = text;
		movivationalPhraseAuthor.text = author;
	}

}
