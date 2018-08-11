using UnityEngine;
using System.Collections;
using DG.Tweening;

public class note_behaviour : MonoBehaviour {
    public bool the_first_note = false;
	public bool active = false;
	public int state = 0;
    float init_y;
	// Use this for initialization
    void Awake() {
        
    }
       
	public void Start () {

        if (the_first_note && USER.s.BEST_SCORE > 3) {
            int i = Random.Range(1,100);
            if (i < 90) Destroy(gameObject);
			init_y = transform.position.y;
			active = true;
			floating_animation_up();
        }
    }

	public void Init(){
		DOTween.KillAll ();
		active = true;

		init_y = transform.position.y;
		state = 2;
		floating_animation_up();
	}

    void floating_animation_up() {
		if (active && state == 2) {
			state = 1;
			transform.DOMoveY (transform.position.y + 0.5f, 1f).SetEase (Ease.InOutCubic).OnComplete (() => floating_animation_down ());
		}
	}

    void floating_animation_down() {
		if (active && state == 1) {
			state = 2;
			transform.DOMoveY (transform.position.y - 0.5f, 1f).SetEase (Ease.InOutCubic).OnComplete (() => floating_animation_up ());
		}
	}

    void new_color() {
        float r = Random.Range(0f, 0.8f);
        float g = Random.Range(0f, 0.8f);
        float b = Random.Range(0f, 0.8f);

        //transform.GetComponent<SpriteRenderer>().color = new Color(r, g, b);

        //Invoke("new_color", 0.2f);
        transform.GetComponent<SpriteRenderer>().DOColor(new Color(r, g, b), 0.5f).OnComplete(() => new_color());
        //Debug.Log(" MY COLOR : " + transform.GetComponent<SpriteRenderer>().color);
    }
}
