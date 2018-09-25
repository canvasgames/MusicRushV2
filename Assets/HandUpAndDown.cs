using UnityEngine;
using System.Collections;
using DG.Tweening;
public class HandUpAndDown : MonoBehaviour {

	// Use this for initialization
	void OnEnable() {
        goUp();

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
