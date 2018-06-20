using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class new_external_link_bt : MonoBehaviour {
    bool video_revive = false;
    bool video_activate_pw = false;
    int video_played = 0;

    public Text seconds_left;

    public GameObject close_bt;
    bool close_started = false;
    bool can_close = false;
    float close_timer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(close_started == true)
        {
            seconds_left.text = "Click on the add to close or wait " + ((int)(close_timer - Time.time)) + " seconds";
            if(close_timer - Time.time <= 0)
            {
                can_close = true;
                close_started = false;
                appear_and_set_close_bt();
                seconds_left.text = "Click on the add or X to close";
            }
        }
    }

    public void set_variables(bool revive_case, bool activate_pw_case)
    {
        video_revive = revive_case;
        video_activate_pw = activate_pw_case;
        video_played = Random.Range(0, 2);
        globals.s.ad_type = video_played;

        close_timer = Time.time  + 5f;
        close_started = true;
        can_close = false;

        if (video_played == 0)
        {
            GetComponent<Animator>().Play("battle_pegs");
        }
        else
        {
            GetComponent<Animator>().Play("bomblast");
        }
        
        
    }

    public void click()
    {
        if (video_played == 0)
        {

        #if !UNITY_EDITOR
		    openWindow("https://www.facebook.com/battlepegsmultiplayer/");
        #else
            Application.OpenURL("https://www.facebook.com/battlepegsmultiplayer/");
#endif

            AnalyticController.s.ReportAdAction("battlepegs", "clicked");

        }
        else
        {
            // 
        #if !UNITY_EDITOR
		    openWindow("https://play.google.com/store/apps/details?id=mominis.Generic_Android.Bomblast");
        #else
            Application.OpenURL("https://play.google.com/store/apps/details?id=mominis.Generic_Android.Bomblast");
        #endif
            AnalyticController.s.ReportAdAction("bomblast", "clicked");

        }
        //      if (can_close == true)
        if (1 == 1)
        {
            if (video_revive == true)
            {
                hud_controller.si.watched_the_video_revive();
            }
            else
            {
                hud_controller.si.watched_the_video_pw();
            }
            close_bt.gameObject.SetActive(false);
            transform.gameObject.SetActive(false);
        }

    }
    [DllImport("__Internal")]
    private static extern void openWindow(string url);

    void appear_and_set_close_bt()
    {
        close_bt.gameObject.SetActive(true);
        close_bt.GetComponent<button_close_video_ended>().set_variables(video_revive, video_activate_pw);
    }
}
