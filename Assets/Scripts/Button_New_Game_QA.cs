using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Button_New_Game_QA : MonoBehaviour {
    int cont_click = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//        if (globals.s.GAME_STARTED)
//            Invoke("destroy_me", 1f);
    }

    void destroy_me() {
        Destroy(gameObject);
    }

    public void click()
    {

        hud_controller.si.HUD_BUTTON_CLICKED = true;
        cont_click++;
        if (cont_click >= 1)
        {
			DataRecorderController.s.userSessionGames = 0;
			DataRecorderController.s.userSessionHighscore = 0;


			USER.s.FIRST_HOLE_CREATED = 0;
            cont_click = 0;
			USER.s.TOTAL_GAMES_WITH_TUTORIAL = 0;
            USER.s.TOTAL_GAMES = 0;

			PlayerPrefs.SetInt ("diskIntroduced", 0);
			PlayerPrefs.SetInt ("firstSongPurchased", 0);


			PlayerPrefs.SetInt("total_games", 0);
            PlayerPrefs.SetInt("total_games_whitout_tutorial", 0);
            PlayerPrefs.SetInt("day_best", 0);
            PlayerPrefs.SetInt("best", 0);
            PlayerPrefs.SetInt("last_score", 0);
            PlayerPrefs.SetInt("first_pw_created", 0);
            PlayerPrefs.SetInt("first_wall_created", 0);
			PlayerPrefs.SetInt("first_hole_created", 0);
			PlayerPrefs.SetInt("pw_introduced", 0);
			PlayerPrefs.SetInt("gift_introduced", 0);
			PlayerPrefs.SetInt("newbie_player", 1);
			PlayerPrefs.SetInt ("PwHandTutShowed", 0);
			PlayerPrefs.SetInt ("GiftHandTutShowed", 0);
            transform.GetComponent<Image>().color = Color.blue;
            PlayerPrefs.DeleteAll();
            hud_controller.si.display_best(0);
            Invoke("back_to_white", 2);
			sound_controller.s.StopAndReleaseCurMusic ();

            //Destroy(gameObject);
            SceneManager.LoadScene("Gameplay Final");
        }
    }

    void back_to_white()
    {
        transform.GetComponent<Image>().color = Color.white;
    }
}
