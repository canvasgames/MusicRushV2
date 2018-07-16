using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class RewardSparkleBehaviour : MonoBehaviour {
//	[SerializeField] Animator myAnimator;
	Image  myImage;
	// Use this for initialization
	void Awake () {	
		myImage = GetComponent<Image> ();
	}
	
	void OnEnable(){
//		myAnimator.enabled = false;
		Invoke("StartAnimation", Random.Range (0.31f, 0.8f));
//		myImage = GetComponent<Image> ();
//		StartCoroutine (ChangePosition ());
	}

	void StartAnimation(){
//		myAnimator.Play ("RewardSparkAnim");
		if(isActiveAndEnabled) StartCoroutine (ChangePosition ());

	}

	IEnumerator ChangePosition(){

		transform.localPosition = new Vector2(Random.Range(-250,250), Random.Range(-350,350));
		myImage.DOFade (1, 0.5f);
		yield return new WaitForSeconds (0.5f);
		myImage.DOFade (0, 0.5f);
		yield return new WaitForSeconds (0.5f);

//		if (globals.s.curGameScreen == GameScreen.RewardNotes || globals.s.curGameScreen == GameScreen.RewardCharacter) 
			StartCoroutine (ChangePosition ());

	}
}
