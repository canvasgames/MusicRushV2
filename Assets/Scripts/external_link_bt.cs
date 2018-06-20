using UnityEngine;
using System.Collections;


public class external_link_bt : MonoBehaviour {
    bool video_revive = false;
    bool video_activate_pw = false;
    int video_played = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void set_variables(bool revive_case, bool activate_pw_case, int video_played_num)
    {
        video_revive = revive_case;
        video_activate_pw = activate_pw_case;
        video_played = video_played_num;
    }

    public void click()
    {
        Debug.Log("click");
        /*if(video_played == 0)
        {
            Application.OpenURL("https://www.facebook.com/battlepegsmultiplayer/");
        }
        else
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=mominis.Generic_Android.Bomblast");
        }*/
        
        if (video_revive == true)
        {
            hud_controller.si.watched_the_video_revive();
        }
        else
        {
            //hud_controller.si.watched_the_video_pw();
        }
        transform.gameObject.SetActive(false);
    }


}
