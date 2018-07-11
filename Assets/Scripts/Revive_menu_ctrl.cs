using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Revive_menu_ctrl : MonoBehaviour {
    public GameObject my_time_bar;
    public Text my_text;
    float duration_time_bar = 5f;
    float actual_duration;
    float end_time;

    // Use this for initialization
    public void Init () {
        //my_time_bar.GetComponent<Image>().color = new Color(70f, 157f, 44f);
		duration_time_bar = 5f;
        my_time_bar.transform.localScale = new Vector3(1f, 1f, 1f);
        end_time = duration_time_bar + Time.time;
//        my_time_bar.transform.DOScaleX(0, duration_time_bar).SetEase(Ease.Linear);
		my_time_bar.GetComponent<Image>().fillAmount = 1f;
		my_time_bar.GetComponent<Image>().color = Color.white;
		my_time_bar.GetComponent<Image>().DOFillAmount(0,duration_time_bar).SetEase(Ease.Linear);
        Invoke("change_bar_2_red", 1.8f);
	}
	
	// Update is called once per frame
	void Update () {
        actual_duration = end_time - Time.time;
		my_text.text = Mathf.CeilToInt (actual_duration).ToString ();

        if(actual_duration <= 0)
        {
			actual_duration = 5f;
            hud_controller.si.close_revive_menu();
            globals.s.CAN_RESTART = true;
        }
    }

    void change_bar_2_red()
    {
        my_time_bar.GetComponent<Image>().color = Color.red;
    }
}
