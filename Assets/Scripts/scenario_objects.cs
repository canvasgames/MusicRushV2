using UnityEngine;
using System.Collections;
using DG.Tweening;


public class scenario_objects : MonoBehaviour {
    public int my_floor;
    public int count_blink = 16;
    public bool already_blinked = false;
    public bool i_am_floor = false;
	public int myId = 0;

	void Awake(){
		myId = globals.s.obstacleId;
		globals.s.obstacleId++;
	}

    public void try_blink(int floor)
    {
        if (floor == my_floor)
        {
            already_blinked = true;
            blink_color();
        }
    }
    void blink_color()
    {
		if(gameObject.GetComponent<SpriteRenderer>()) gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        Invoke("blink_back", 0.1f);
    }
    void blink_back()
    {
        count_blink -= 1;
		if(gameObject.GetComponent<SpriteRenderer>()) gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        if (count_blink > 0)
            Invoke("blink_color", 0.1f);
    }

    public void unactivate_collider()
    {
        if(transform.GetComponent<Collider2D>() != null)
            transform.GetComponent<Collider2D>().isTrigger = true;
    }

    public void new_color(float r, float g, float b, float time) {
        // float r = Random.Range(0f, 0.8f);
        //float g = Random.Range(0f, 0.8f);
        //float b = Random.Range(0f, 0.8f);

        //transform.GetComponent<SpriteRenderer>().color = new Color(r, g, b);

        //Invoke("new_color", 0.2f);
        transform.GetComponent<SpriteRenderer>().DOColor(new Color(r, g, b), time);
        //Debug.Log(" MY COLOR : " + transform.GetComponent<SpriteRenderer>().color);
    }
}
