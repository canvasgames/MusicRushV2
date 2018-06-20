using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class activate_pw_button : MonoBehaviour {
	#region === VArs ===
	public GameObject my_time, my_spin_now, myReadyInText; //, my_glow, my_icon_countdown, my_icon_spinnow;
	public GameObject HandTut;
	[SerializeField] GameObject myBar, myReadyEffect;
	Button myBt;
	bool interactable = false;
	#endregion

	// Use this for initialization
	void Awake () {
		myBt = GetComponent<Button> ();

//		SetCountownState ();
//		Invoke ("SetSPinNowState", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCountownState(){
		interactable = false;
		GetComponent<Button> ().interactable = false;

		if (QA.s.TRACE_PROFUNDITY > 0) Debug.Log ("SET COUNTDOWN STATEWEEE");

		// deactivate light effect
		if(myReadyEffect) myReadyEffect.SetActive (false);

		//deactivate bar text
		if(my_spin_now) my_spin_now.SetActive(false);

		// activate bar texts
		if(myReadyInText) myReadyInText.SetActive (true);
		if(my_time) my_time.SetActive (true);
	
//		HandTut.SetActive (false);
		//myBt.interactable = false;

		if (myBar != null) {
			myBar.GetComponent<Animator> ().Play ("GreenBarStaticAnim");
		}
	}

	public void SetSPinNowState(){

		if (myBar) myBar.GetComponent<Animator> ().Play ("GreenBarReadyAnim");

		if(myReadyInText) myReadyInText.SetActive(false);
		if(my_time) my_time.SetActive(false);

		if(my_spin_now) my_spin_now.SetActive(true);

		Debug.Log ("SET SPIIIIIIIIN NOW STATE");

		StartCoroutine (ReadyAnimation ());

//		my_time.SetActive (false);
//		my_spin_now.SetActive (true);
//		my_glow.SetActive (true);

//		my_icon_countdown.SetActive (false);
//		my_icon_spinnow.SetActive (true);


		//blinkText ();
		//myBt.interactable = true;

	}

	IEnumerator ReadyAnimation(){
		if (myReadyEffect) {
			myReadyEffect.SetActive (true);
			myReadyEffect.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			myReadyEffect.GetComponent<Animator> ().Play ("GlowReadyAnim");
		}

		yield return new WaitForSeconds (0.57f);

		if(myReadyEffect) myReadyEffect.GetComponent<Image> ().DOFade (0, 0.2f);

		GetComponent<Button> ().interactable = true;
		interactable = true;

	}

	public void HandTutLogic(){
		if(PlayerPrefs.GetInt("PwHandTutShowed", 0) == 0){
//			HandTut.SetActive (true);
			PlayerPrefs.SetInt ("PwHandTutShowed", 1); 
		}
//		else
//			HandTut.SetActive (false);
	}

	void blinkText(){
		if(interactable)
		{
//			Color rand_color = new Color(Random.Range(0f,1f), Random.Range(0.9f,1f),Random.Range(0f,1f));
//			my_spin_now.GetComponent<Text> ().DOColor (rand_color, 0.5f).OnComplete (blinkText);
		}
	}
		
    public void click()
    {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];
        foreach (ball_hero b in bolas)
        {
            //Destroy(b.gameObject);
            b.send_actual_balls();
            break;
        }

       // hud_controller.si.HUD_BUTTON_CLICKED = true;

		hud_controller.si.RodaMenu ();
//
    }

//	public void SetCountownState(){
//		Debug.Log ("SET COUNTDOWN STATE");
//		my_time.SetActive (true);
//		my_spin_now.SetActive (false);
//		my_glow.SetActive (false);
//
//		my_icon_countdown.SetActive (true);
//		my_icon_spinnow.SetActive (false);
//
//		GetComponent<Button> ().interactable = false;
//		interactable = false;
//		HandTut.SetActive (false);
//		//myBt.interactable = false;
//	}
//
//	public void HandTutLogic(){
//		if(PlayerPrefs.GetInt("PwHandTutShowed", 0) == 0){
//			HandTut.SetActive (true);
//			PlayerPrefs.SetInt ("PwHandTutShowed", 1);
//		}
//		else
//			HandTut.SetActive (false);
//	}
//
//	public void SetSPinNowState(){
//
//
//
//
//		Debug.Log ("SET SPIIIIIIIIN NOW STATE");
//
//		my_time.SetActive (false);
//		my_spin_now.SetActive (true);
//		my_glow.SetActive (true);
//
//		my_icon_countdown.SetActive (false);
//		my_icon_spinnow.SetActive (true);
//
//		GetComponent<Button> ().interactable = true;
//
//		interactable = true;
//
//		//blinkText ();
//		//myBt.interactable = true;
//
//	}
//
//	void blinkText(){
//		if(interactable)
//		{
//			Color rand_color = new Color(Random.Range(0f,1f), Random.Range(0.9f,1f),Random.Range(0f,1f));
//			my_spin_now.GetComponent<Text> ().DOColor (rand_color, 0.5f).OnComplete (blinkText);
//		}
//	}
//
//
//	public void click()
//	{
//		ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];
//		foreach (ball_hero b in bolas)
//		{
//			//Destroy(b.gameObject);
//			b.send_actual_balls();
//			break;
//		}
//
//		// hud_controller.si.HUD_BUTTON_CLICKED = true;
//
//		hud_controller.si.RodaMenu ();
//		//

//	}



}
