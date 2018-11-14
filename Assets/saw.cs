using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class saw : scenario_objects
{
    public static readonly List<saw> All = new List<saw>();

    public GameObject my_vision_effect;
    public GameObject my_skin;

    public bool hidden = false;
    public bool manual_trigger = false;
    bool already_placed = false;
    public bool corner_repositionable = false;
    public bool repositionable = false;
    bool already_appeared = false;
    bool already_alerted = false;
    public bool triple_spk = false;
    float timer = 0;
 	public string wave_name = "";
    public float distance2Appear = 0;

    private void OnEnable()
    {
        All.Add(this);
    }

    private void OnDisable()
    {
        All.Remove(this);
    }
    // Use this for initialization
    void Start () {
        //my_skin.transform.localScale = new Vector3(1, 0, 0);
        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            show_me_pw_sight();
        }
//        else
//            my_vision_effect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (already_appeared == false && !already_alerted &&
        globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 2f && my_floor <= globals.s.BALL_FLOOR + 1)
        {
            already_alerted = true;
			BallMaster.s.IncreaseBallAlertN ("saw", this.myId);
        }

        if ( already_appeared == false &&
           globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 2f && my_floor <= globals.s.BALL_FLOOR + 1)
        {
            if (already_placed && Mathf.Abs(transform.position.x - globals.s.BALL_X) < distance2Appear)
            {
                show_me();
				BallMaster.s.DeacreaseBallAlertN ("saw");
            }
        }
    }

    public void place_me_at_the_other_corner(float ball_x, int floor)
    {
        if (floor == my_floor)
        {
            //GetComponent<CircleCollider2D>().enabled = true;
            if ((ball_x > 0 ) )
            {
                transform.position = new Vector2(0 - Mathf.Abs(0 - transform.position.x), transform.position.y);
            }
            else
            {
                transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
            }

            already_placed = true;


			// Deactivate the manual trigger from spikes that are at the same side as I am
			spike[] spks = objects_pool_controller.s.spikes_scripts.ToArray();

			for (int i=0; i< spks.Length; i++) {
				//spk.manual_trigger_cancel(transform.position.x, my_floor);
				if(spks[i].isActiveAndEnabled) spks[i].reposite_me_at_the_other_corner(-transform.position.x, my_floor);
			}

        }
    }


    void show_me()
    {
		sound_controller.s.PlaySfxTrapHiddenSawAppear ();

        already_appeared = true;
        transform.GetComponent<CircleCollider2D>().enabled = true;
        back_original_color_pw_sight();

        transform.DOLocalMoveY((transform.localPosition.y + 0.64f), 0.1f).SetEase(Ease.Linear); 
        if(transform.position.x < 0)
            transform.DOMoveX((transform.position.x + 11f), 1.8f).SetEase(Ease.Linear);
        else
            transform.DOMoveX((transform.position.x - 11f), 1.8f).SetEase(Ease.Linear);
    }

    public void unshow_me()
    {
        my_skin.transform.DOScaleY(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));

    }

    public void show_me_pw_sight()
    {
        //transform.GetComponent<SpriteRenderer>().color.a = 100;
        // transform.GetComponent<SpriteRenderer>().color = Color.magenta;
        //GameObject instance = Instantiate(Resources.Load("Prefabs/Warning",
        //typeof(GameObject)), new Vector3(transform.position.x, transform.position.y +2f), transform.rotation) as GameObject;
        if (!already_appeared)
        {
            my_vision_effect.SetActive(true);
            my_vision_effect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            my_vision_effect.GetComponent<SpriteRenderer>().DOFade(1, 0.25f);
        }

    }

    public void back_original_color_pw_sight()
    {
        //transform.GetComponent<SpriteRenderer>().color = new Color (0,0,0,0);
        my_vision_effect.SetActive(false);
    }

    public void clear_flags_reposite()
    {
        transform.GetComponent<CircleCollider2D>().enabled = false;


        hidden = false;
        manual_trigger = false;
        corner_repositionable = false;
        repositionable = false;
        already_appeared = false;
		already_alerted = false;

        timer = 0;
        count_blink = 16;
    }

	public void ReviveRemovalLogicForClosestFloors(int floor)
	{
		if (floor == my_floor || floor+1 == my_floor || floor+2 == my_floor)
		{
			gameObject.SetActive (false);
			transform.position = new Vector3(transform.position.x - Random.Range(50, 150), transform.position.y - Random.Range(50, 150), transform.position.z);
		}
	}

}
