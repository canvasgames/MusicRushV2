using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class start_bt : MonoBehaviour {

	bool canTap = false;

	// Use this for initialization
	void Start () {
		Invoke ("AllowTapToStartGame", globals.s.TIME_TO_ALLOW_TAP);
	}

	void AllowTapToStartGame(){
		canTap = true;
	}
	
	// Update is called once per frame
//	void Update () {
//		#if UNITY_EDITOR
////			GetComponent<Button>().interactable = false;
//		if (1==2 && canTap == true && Input.GetMouseButtonDown(0) && globals.s.GAME_STARTED == false && globals.s.MENU_OPEN == false)
//			{
//				globals.s.GAME_STARTED = true;
//				hud_controller.si.start_game();
//			}
//		#endif
//	}


    public void click()
    {
		if (canTap == true && globals.s.curGameScreen == GameScreen.MainMenu && globals.s.GAME_STARTED == false && globals.s.MENU_OPEN == false && globals.s.GIFT_ANIMATION == false)
        {
			if (BallMaster.s.CheckIfBallAreGrounded ()) {
				Debug.Log ("START BT CLICK: START THE GAME!!!");
				globals.s.GAME_STARTED = true;
				hud_controller.si.start_game_coroutine ();
				BallMaster.s.BallFirstJump ();
			} else {
				Debug.Log ("START BT: BALL IS NOT GROUNDED");
			}
		}
		else{
			Debug.Log ("CAN TAP " + canTap   );
		}

    }
}
