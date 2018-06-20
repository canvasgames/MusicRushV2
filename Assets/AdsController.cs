using UnityEngine;
using System.Collections;

public class AdsController : MonoBehaviour {
	public static AdsController s;
	// Use this for initialization
	void Awake () {
		s = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowAd(){
        hud_controller.si.show_video_pw();
        USER.s.TOTAL_VIDEOS_WATCHED++;
        PlayerPrefs.SetInt("total_videos_watched", USER.s.TOTAL_VIDEOS_WATCHED);

        AnalyticController.s.ReportVideoWatchedForPowerUps();
	}
}
