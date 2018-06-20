using UnityEngine;
using System.Collections;

public class button_close_video_ended : MonoBehaviour {
    bool video_revive = false;
    bool video_activate_pw = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void set_variables(bool revive_case, bool activate_pw_case)
    {
        video_revive = revive_case;
        video_activate_pw = activate_pw_case;
    }

    public void click()
    {
        if (video_revive == true)
        {
            hud_controller.si.watched_the_video_revive();
        }
        else
        {
            hud_controller.si.watched_the_video_pw();
        }

        if (globals.s.ad_type == 0) AnalyticController.s.ReportAdAction("battlepegs", "closed");
        else if (globals.s.ad_type == 1) AnalyticController.s.ReportAdAction("bomblast", "closed");

        transform.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}
