using UnityEngine;
using System.Collections;
using DG.Tweening;
public class HandUpAndDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
        goUp();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void goUp()
    {
        transform.DOLocalMoveY(transform.localPosition.y + 22, 0.4f).OnComplete(goDown);
    }

    void goDown()
    {
        transform.DOLocalMoveY(transform.localPosition.y - 22, 0.4f).OnComplete(goUp); ;

    }
}
