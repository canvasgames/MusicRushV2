using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewHighscoreScreen : MonoBehaviour {

	[SerializeField] Text HighscoreText;
	[SerializeField] GameObject myBlackBg;
	// Use this for initialization
	bool canClick = false;

	void OnEnable(){ // DEBUG
//		Init ();
	}

	public void Init(){
		canClick = false;
		HighscoreText.text = (globals.s.BALL_FLOOR + 1).ToString ();
		Invoke ("AllowClick", 1f);
	}

	void AllowClick(){
		canClick = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			gameObject.SetActive (false);
			hud_controller.si.appear_game_over ();
		}
	
	}
}
