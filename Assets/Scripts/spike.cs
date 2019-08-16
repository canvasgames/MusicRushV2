using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class spike : scenario_objects {

    public static readonly List<spike> All = new List<spike>();

	public GameObject my_vision_effect;
    public string wave_name;
    public bool hidden = false;
    public bool manual_trigger = false;
	public bool corner_repositionable = false;
    public bool repositionable = false;
    bool already_appeared = false;
    bool already_alerted = false;
	public bool triple_spk = false;

	public GameObject my_glow, my_glow_off;
    public GameObject my_skin;
	float my_skin_scale  = 1.042153f;

    float timer = 0;
    float target_y;
    public PolygonCollider2D my_collider;

    private void OnEnable(){
        All.Add(this);
		AllScenery.Add (this);
    }

    private void OnDisable() {
        All.Remove(this);
		AllScenery.Remove (this);
    }

	public override void TurnTheLightsOnForThePartyHard(){
//		Debug.Log (myId + "  DOING SOMETHING!! for the dance ");
		my_glow.SetActive (true);
		my_glow_off.SetActive (false);
	}

    // Use this for initialization
    void Awake()
    {
        my_collider = GetComponent<PolygonCollider2D>();
		my_vision_effect.SetActive (false);
		//my_skin_scale = my_skin.transform.localScale.y;
    }
	void Start () {

        //GetComponent<SpriteRenderer>().color = Color.green;
        //if(QA.s.INVENCIBLE == true)
        //my_collider.enabled = false;
       // glow_animation_wait();
    }
		
    // Update is called once per frame
    void Update()
    {
        //alert ball check
        if (!manual_trigger && !already_alerted && hidden == true && already_appeared == false
            && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f && my_floor <= globals.s.BALL_FLOOR + 1) {
            already_alerted = true;
			BallMaster.s.IncreaseBallAlertN ("spk");
        }

        //hidden spike distance check (no manual trigger)
        if (!manual_trigger && hidden == true && already_appeared == false
            && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f
			&& Mathf.Abs(globals.s.BALL_X - transform.position.x) < 4.5f) {
            
			show_me();
			BallMaster.s.DeacreaseBallAlertN ("spk");
		}

        //Destroy check
        if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
        {
            if(gameObject!=null)
            {
               // Destroy(gameObject);
				gameObject.SetActive(false);
            }
        }
            
    }
    
    public void show_me_by_floor(int n)
    {
        if (my_floor == n && hidden && manual_trigger) show_me();
    }

    // method for hidden spikess
    void show_me()
    {
        Debug.Log("[SPK HIDDEN] show me called !!!! ");
		sound_controller.s.PlaySfxTrapHiddenSpikeAppear ();
        //transform.GetComponent<SpriteRenderer>().color = Color.white;
        //transform.GetComponent<SpriteRenderer>().color = Color.black;
        //target_y = transform.position.y + transform.GetComponent<SpriteRenderer>().bounds.size.y;
       // target_y = transform.position.y + 0.5f;
		//my_skin.transform.DOLocalMoveY(0, 0.14f);
		my_skin.transform.DOScaleY(my_skin_scale, 0.14f);
        already_appeared = true;
        my_collider.enabled = true;
        hidden = false;

		//if(globals.s.PW_SIGHT_BEYOND_SIGHT) my_vision_effect.SetActive (false);

        //if(QA.s.INVENCIBLE == false)
        //my_collider.enabled = true;
        /*rb.velocity = new Vector2(0, 20f);
        timer = (transform.GetComponent<SpriteRenderer>().bounds.size.y ) / 20f ;
        Debug.Log(" MMMMMMMMMMMM MOVE SPIKE! TIMER: " + timer);
        ;*/

    }

    // Check if a hole can be created on top of me
    public bool check_range_for_hole(int floor_n, float x)
    {
        if (floor_n == my_floor)
        {
			if ((!triple_spk && Mathf.Abs(x - transform.position.x) < globals.s.HOLE_SPK_DIST) ||
				(triple_spk && Mathf.Abs(x - transform.position.x) < globals.s.HOLE_SPK_DIST + 0.15f)) return false;
            else return true;
        }
        else
        {
            Debug.Log(" TTTTTTTTTTTT THIS SHOULD NEVER HAPPEN! M FLOOR: " + my_floor + " FLOOR PARAM: " + floor_n); return true;
        }
    }

    public void manual_trigger_cancel(float position, int floor)
    {
        if (manual_trigger && floor == my_floor && ((position > 0 && transform.position.x > 0) || (position < 0 && transform.position.x < 0)))
        {
            manual_trigger = false;
        }
    }

	public void reposite_me_for_FTU(float wall_position, int floor)
	{
		if (repositionable && floor == my_floor)
		{
			if (wall_position > 0)
				transform.position = new Vector2(0 - Mathf.Abs(0 - transform.position.x), transform.position.y);
			else
				transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
		}
	}

    public void reposite_me_at_the_other_corner(float wall_position, int floor)
    {
        if (corner_repositionable && floor == my_floor)
        {
            if (wall_position > 0)
                transform.position = new Vector2(0 - Mathf.Abs(0 - transform.position.x), transform.position.y);
            else
                transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
        }
    }


	public void destroy_throwed_spikes (float y_pos_ball, float y_screen_limit)
    {
//		Debug.Log (" TRYING TO DESTROY SPIKE: " + y_pos_ball + " MY POS " + transform.position.y);
//		if( y_pos_ball > transform.position.y && y_screen_limit < transform.position.y)
		if( y_pos_ball > transform.position.y)
        {
//			Debug.Log (" DESTROY SUCCESS !!: " + y_pos_ball + " MY POS " + transform.position.y);
            /// transform.DOScale(0, 0.3f);//.OnComplete(destroy_me_baby);
            transform.position = new Vector3(transform.position.x - Random.Range(50,150), transform.position.y - Random.Range(50, 150), transform.position.z);
        }
    }


    public void remove_spikes_revive(int floor)
    {
        if (floor == my_floor || floor+1 == my_floor || floor+2 == my_floor)
        {
			gameObject.SetActive (false);
			transform.position = new Vector3(transform.position.x - Random.Range(50, 150), transform.position.y - Random.Range(50, 150), transform.position.z);
        }
    }

    void destroy_me_baby()
    {
        //Destroy(gameObject);
    }

    public void show_me_pw_sight()
    {
		if (hidden == true && manual_trigger == false) {
			//transform.GetComponent<SpriteRenderer>().color = Color.magenta;
			my_vision_effect.SetActive (true);
			my_vision_effect.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
			my_vision_effect.GetComponent<SpriteRenderer> ().DOFade (1, 0.25f);
		} else if (hidden == true && manual_trigger == true) {
			my_vision_effect.SetActive (true);
			my_vision_effect.GetComponent<SpriteRenderer> ().color = Color.red;
			my_vision_effect.GetComponent<SpriteRenderer> ().DOFade (1, 0.25f);
		}
            
                //transform.GetComponent<SpriteRenderer>().color = Color.magenta;
        //transform.GetComponent<Animator>().Play("red");
    }

    public void back_original_color_pw_sight()
    {
        //transform.GetComponent<Animator>().Play("blue");
        //transform.GetComponent<SpriteRenderer>().color = Color.white;
       	//transform.GetComponent<SpriteRenderer>().color = Color.black;
		my_vision_effect.SetActive (false);

    }

    public void clear_flags_reposite()
    {
        hidden = false;
        manual_trigger = false;
		corner_repositionable = false;
        repositionable = false;
        already_appeared = false;
		already_alerted = false;
        my_vision_effect.SetActive(false);

        timer = 0;
//        GetComponent<SpriteRenderer>().color = Color.black;
		my_glow.SetActive(false);
		my_glow_off.SetActive(true);
        transform.localScale = new Vector3(globals.s.SPK_SCALE, globals.s.SPK_SCALE, globals.s.SPK_SCALE);
        my_skin.transform.localScale = new Vector3(my_skin_scale, my_skin_scale, my_skin_scale);
        
        count_blink = 16;
        //transform.DOScale(0.7f, 0.1f);
    }
}
