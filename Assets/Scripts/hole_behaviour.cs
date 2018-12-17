using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class hole_behaviour : MonoBehaviour
{
    public static readonly List<hole_behaviour> All = new List<hole_behaviour>();
    public GameObject my_skin;
	public SpriteRenderer my_skin_sprite;
    public int my_floor;
    public GameObject colliderPW;
    bool hidden = true;
    bool already_alerted = false;
    public GameObject my_glow;
	public bool repositionable = false;
	public bool already_placed = false;
	public GameObject floor_left, floor_right;
	public GameObject my_vision_effect;
	bool already_fallen = false;

    void OnEnable()
    {
        All.Add(this);
    }

    void OnDisable()
    {
        All.Remove(this);
    }

    // Use this for initialization
    void Start()
    {
		my_skin.GetComponent<hole_skin_behaviour>().my_floor = my_floor;
		my_skin_sprite = my_skin.GetComponent<SpriteRenderer> ();
        //glow_animation_start();

        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            show_me_pw_sight();
		}

//		my_skin.GetComponent<SpriteRenderer> ().color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (hidden && !already_alerted && 
            globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f) {
            already_alerted = true;
			BallMaster.s.IncreaseBallAlertN ("hole");
        }

		if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
			destroy_me_baby ();
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            //my_skin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -15f);
            Invoke("start_falling", 0.01f);

            // gameObject.SetActive(false);

        }
    }

    void start_falling() {
		BallMaster.s.DeacreaseBallAlertN ("hole");


		my_skin.transform.DOMoveY(my_skin.transform.position.y - 8f, 0.8f).OnComplete(() => GetBacktoMeBaby());
        GetComponent<BoxCollider2D>().enabled = false;
		already_fallen = true;

		my_vision_effect.GetComponent<SpriteRenderer> ().DOFade (0, 0.35f).OnComplete(() => my_vision_effect.SetActive (false));
		//my_vision_effect

		sound_controller.s.PlaySfxTrapHiddenHoleFall ();
//		my_vision_effect.SetActive (false);
    }

	void GetBacktoMeBaby(){
//		Invoke ("MissedYouSoMuch", 3f);
	}
	void MissedYousoMuch(){
//		my_skin.transform.position.y + 8f;
		gameObject.SetActive(false);
//		my_skin.transform.localPosition = new Vector2 (0, 0.8000183f);
//		already_fallen = false;
//		GetComponent<BoxCollider2D>().enabled = true;
	}


    public void activate_squares()
    {
        if (colliderPW) colliderPW.SetActive(true);
    }

    public void destroy_pw_super_under_floors(float y_pos_ball)
    {
        if(colliderPW != null)
            colliderPW.SetActive(false);

        if (y_pos_ball > transform.position.y)
        {
            transform.DOScaleZ(1, 0.3f).OnComplete(destroy_me_baby); 
        }
    }

    void destroy_me_baby()
    {
        Destroy(gameObject);
    }

    public void show_me_pw_sight()
    {
		if (!already_fallen) {
			my_vision_effect.SetActive (true);
			my_vision_effect.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
			my_vision_effect.GetComponent<SpriteRenderer> ().DOFade (1, 0.25f);
		}
//        if (my_skin != null)
//            my_skin.transform.GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    public void back_original_color_pw_sight()
    {
		my_vision_effect.SetActive (false);

        if(my_skin != null)
            //my_skin.transform.GetComponent<SpriteRenderer>().color = Color.white;
            my_skin.transform.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void colidded_super_pw()
    {
        my_skin.transform.GetComponent<SpriteRenderer>().enabled = false;
    }

	public void reposite_me_at_the_other_corner(float ball_position, int floor)
	{
		if (repositionable && floor == my_floor)
		{
			if (ball_position > 0) {
				transform.position = new Vector2 (0 - Mathf.Abs (0 - transform.position.x), transform.position.y);
			}
			else
				transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
//			if (floor_left != null) Debug.Log("REPOSITIONING FLOOR LEFT.." + floor_left.transform.position.x );
//			else Debug.Log("FLOOR LEFT IS NUL!! ");
			floor_left.transform.position = new Vector2 (transform.position.x - game_controller.s.hole_size/2 - + floor_left.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, floor_left.transform.position.y);
			floor_right.transform.position = new Vector2 (transform.position.x + game_controller.s.hole_size/2 + floor_left.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, floor_left.transform.position.y);
//			if (floor_left != null)
//				Debug.Log ("REPOSITIONED.." + floor_left.transform.position.x);

		}
		//if (my_hole != null)
		//my_hole.transform.position = transform.position;
	}
}

