using UnityEngine;
using System.Collections;
using DG.Tweening;

public class trail_behaviour : MonoBehaviour {

    float start_y;

    void move_up() {
        transform.DOLocalMoveY(start_y + 0.1f, 0.075f).OnComplete(move_down);
    }

    void move_down() {
        transform.DOLocalMoveY(start_y -0.1f, 0.075f).OnComplete(move_up); 

    }

    // Use this for initialization
    void Start () {

        TrailRenderer tr = this.GetComponent<TrailRenderer>();
        tr.sortingLayerName = "Hole";

        start_y = transform.localPosition.y;
        move_up();
	
	}
	
	// Update is called once per frame
	void Update () {
        

    }
}
