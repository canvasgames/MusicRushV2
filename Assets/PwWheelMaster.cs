using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class PwWheelMaster : MonoBehaviour {
	public GameObject title, top, bottom, tampa, youNowHaveMenu;
    public Text youNowHaveText;
    public pizza_char pitissa;
	public GameObject haste;
	public Button myBack;
	int rewardToGive = 0;
	// Use this for initialization

    void OnEnable()
    {
		pitissa.hand.SetActive(true);
//        if (hud_controller.si.CAN_ROTATE_ROULETTE == true)
//        {
//            openTampa();
////			transform.DORotate(transform.eulerAngles(transform.localRotation) + 66)
//        }
//        else
//        {
////            pitissa.hand.SetActive(false);
//        }
    }
    void OnDisable()
    {
//        tampa.transform.DOKill();
//        tampa.transform.localPosition = new Vector3(tampa.transform.localPosition.x,6,tampa.transform.localPosition.z);
    }
    public void openTampa()
    {
        pitissa.openingTampa = true;
//        tampa.transform.DOLocalMoveY(tampa.transform.localPosition.y, 1).OnComplete(realOpen);
    }

    void realOpen()
    {
//        tampa.transform.DOLocalMoveY(-436, 2).OnComplete(canRotate);
    }

	public void openRewardMenu(float value)
	{
		globals.s.curGameScreen = GameScreen.RewardNotes;
		rewardToGive = (int)value;

		youNowHaveMenu.SetActive (true);
		if (value > 0) {
			youNowHaveText.gameObject.SetActive (true);
			youNowHaveMenu.GetComponent<RewardScreen> ().myNotesRewardIcon.gameObject.SetActive (true);
			youNowHaveText.text = value.ToString ();
			youNowHaveMenu.GetComponent<RewardScreen> ().myDiamond.SetActive (false);

		} else {
			youNowHaveMenu.GetComponent<RewardScreen> ().myNotesRewardIcon.gameObject.SetActive (false);
			youNowHaveMenu.GetComponent<RewardScreen> ().myDiamond.SetActive (true);
			youNowHaveText.gameObject.SetActive (false);
		}
	}

    public void closeRewardMenu()
    {
        youNowHaveMenu.SetActive(false);

		if(rewardToGive > 0)
			USER.s.AddNotes (rewardToGive);
		else
			USER.s.AddDiamonds (1, "SpinDisk");

		hud_controller.si.PowerUpsMenuClose ();

		if (globals.s.GAME_OVER == 1 || globals.s.curGameScreen == GameScreen.LevelEnd) {
			globals.s.NOTES_COLLECTED += rewardToGive;
			globals.s.NOTES_COLLECTED_JUKEBOX += rewardToGive;
			Debug.Log (" CLOSE REWARD MENU !!!! ");

			if(FTUController.s.canIntroduceStore == 0) FTUController.s.AllowIntroduceStore ();

			GameOverController.s.Init ();
		} else {
//			hud_controller.si.activate_pw_bt.GetComponent<Button> ().interactable = false;
		}
		if(QA.s.SPIN_INFINITE == false) hud_controller.si.activate_pw_bt.GetComponent<Button> ().interactable = false;

//        Invoke("closeTampa",0.3f);
    }

    void closeTampa()
    {

//        tampa.transform.DOLocalMoveY(6, 2);
    }
    
    void canRotate()
    {
        pitissa.openingTampa = false;
        pitissa.hand.SetActive(true);

    }
    public IEnumerator Entrance(){
		//Debug.Log ("EEEEEENTRANCE ");
//		float y_pos = title.transform.localPosition.y;
//		title.transform.localPosition = new Vector2 (title.transform.localPosition.x, title.transform.localPosition.y + title.GetComponent <RectTransform> ().rect.height);
//		title.transform.DOLocalMoveY(y_pos
//			, 0.5f).SetEase (Ease.OutQuart);
		
//		title.transform.DOLocalMoveY(title.GetComponent <RectTransform> ().rect.height
//			, 0.5f).SetEase (Ease.OutQuart);
		yield return new WaitForSeconds (0.14f);

//		header.transform.DOLocalMoveY (game_title.transform.localPosition.y + 500
//			, 0.5f).SetEase (Ease.OutQuart);
//		yield return new WaitForSeconds (0.2f);
	}

	public void ReSpinVideoWatched(){
		globals.s.curGameScreen = GameScreen.SpinDisk;
		youNowHaveMenu.SetActive (false);
		canRotate ();
		myBack.interactable = true;
//		closeRewardMenu ();
	}
}
