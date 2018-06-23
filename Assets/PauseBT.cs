using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseBT : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}

    public void PointerEnter()
    {
        Debug.Log("aaaaaaaaaaaa");
        globals.s.CURSOR_IN_PAUSE_BT = true;
    }


    public void PointerExit()
    {
        Debug.Log("ccccccccccccccc");

        globals.s.CURSOR_IN_PAUSE_BT = false;
    }
}
