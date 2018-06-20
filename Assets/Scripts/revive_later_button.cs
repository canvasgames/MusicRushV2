using UnityEngine;
using System.Collections;

public class revive_later_button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        hud_controller.si.revive_menu_start();
    }
}
