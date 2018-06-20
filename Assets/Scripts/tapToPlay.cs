using UnityEngine;
using System.Collections;
using DG.Tweening;

public class tapToPlay : MonoBehaviour {

    // Use this for initialization
    void Start() {
        fadeout();
    }

    void fadeout() {
        GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => fadein());
    }

    void fadein() {
       GetComponent<CanvasGroup>().DOFade(1, 0.7f).OnComplete(() => fadeout());
    }


    // Update is called once per frame
    void Update() {

    }
}
