using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public Image BlackBG;

	// Use this for initialization
	void Start () {
		
	}
	
    void Awake()
    {
        BGAlphaOn();
    }

	// Update is called once per frame
	void Update () {
    }

    void BGAlphaOn()
    {
        BlackBG.DOColor(new Color(BlackBG.color.r, BlackBG.color.g, BlackBG.color.b, 23),2).SetUpdate(false);
    }

    public void BGAlphaOff()
    {
        BlackBG.DOColor(new Color(BlackBG.color.r, BlackBG.color.g, BlackBG.color.b, 0), 0.2f).SetUpdate(false).OnComplete(DesativaMenu);
    }
    
    void DesativaMenu()
    {
        transform.gameObject.SetActive(false);
    }
}
