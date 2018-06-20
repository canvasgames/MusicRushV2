using UnityEngine;
using System.Collections;

public class close_bt_revive : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        hud_controller.si.close_revive_menu();
        globals.s.CAN_RESTART = true;
    }
}
