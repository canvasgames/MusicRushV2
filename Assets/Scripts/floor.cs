using UnityEngine;
using System.Collections;
using DG.Tweening;


public class floor : scenario_objects {

	#region === VARS ===
   // public int my_floor;
	public hole_behaviour my_hole;
    public GameObject QA_wave_name;
    public GameObject colliderPW;
    GameObject my_text;
    public bool pw_super_collided = false;
    public GameObject my_skin;
    public GameObject my_skin_bg;
    public floor_note [] my_notes_fg;
    float my_txt_y_dif = 0.32f;
    public GameObject my_glow, my_lines_glow;
	public bool repositionable = false;
	public bool already_placed = false;
	public GameObject pauta;
	public GameObject myHighscoreAnimation = null;
	bool canUpdate;
	#endregion

    // Use this for initialization
    void Start () {
        i_am_floor = true;
        //glow_animation_wait();

		//		Invoke ("clear_flags_reposite", 2f);
		Invoke ("CanUpdate", 2f);
    }

	public void ResetCanUpdate(){
		canUpdate = false;
		Invoke ("CanUpdate", 1f);
	}

	void CanUpdate(){
		canUpdate = true;
	}

	void Update(){
		if( isActiveAndEnabled && canUpdate && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f){
//			Debug.Log ("sTARTTTTTT FLOOR");
			foreach (floor_note note in my_notes_fg) {
				if (note.already_appeared == false &&
				  ((globals.s.CUR_BALL_SPEED > 0 && globals.s.BALL_X > note.transform.position.x) ||
				  ((globals.s.CUR_BALL_SPEED < 0 && globals.s.BALL_X < note.transform.position.x)))) {
					note.already_appeared = true;

					note.mySR.enabled = true;
					note.mySR.color = new Color (1, 1, 1, 0);
					note.mySimpleAnimation.Rewind ();
//					note.myAnimation.Rewind();
//					note.myAnimation.Rewind. Play ();
//					note.

					//            mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0);
					//            mySR.DOFade(1, 0.3f);
					//			GetComponent<Animator>().Play("NoteFadeInAnimation");
//					GetComponent<Animator> ().enabled = true;
//					GetComponent<Animator> ().SetTrigger ("FadeIn");
					//Debug.Log("LET ME APPEAR! MY: " + transform.position.x + " BALL X: " + globals.s.BALL_X + "ball speed: " + globals.s.CUR_BALL_SPEED);
				}
			}
		}
	}


    public void glow_animation_start() {
		my_glow.GetComponent<SpriteRenderer>().DOFade(1f, GD.s.GlowInTime).OnComplete(glow_animation_wait);
		my_lines_glow.GetComponent<SpriteRenderer> ().DOFade (0.4f, GD.s.GlowInTime);

		//if (my_hole != null)
			//my_hole.glow_animation_start ();
    }
	void glow_animation_wait(){
		Invoke ("glow_animation_end", GD.s.GlowStaticTime);
		//glow_animation_end ();
	}

    public void glow_animation_end() {
		my_glow.GetComponent<SpriteRenderer>().DOFade(0, GD.s.GlowOutTime).OnComplete(glow_animation_start);
		my_lines_glow.GetComponent<SpriteRenderer>().DOFade(0,GD.s.GlowOutTime);
    }

	#region === NEW HIGHSCORE === 
    public void check_if_have_score()
    {
        if(my_floor > 1)
        {
            if (USER.s.BEST_SCORE == my_floor)
            {
				myHighscoreAnimation = objects_pool_controller.s.RepositeFloorBestScore ();
				myHighscoreAnimation.transform.parent = transform;
				myHighscoreAnimation.transform.localPosition = new Vector2 (0f, 0.08f);
				myHighscoreAnimation.transform.parent = null;

//                GameObject obj = objects_pool_controller.s.reposite_score(0, transform.position.y - my_txt_y_dif);
//                obj.GetComponentInChildren<TextMesh>().text = "YOUR BEST";
//                obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
//                obj.GetComponentInChildren<Score_floor_txt>().my_type = 1;
            }
            else if (USER.s.DAY_SCORE == my_floor)
            {
				myHighscoreAnimation = objects_pool_controller.s.RepositeFloorDailyBest ();
				myHighscoreAnimation.transform.parent = transform;
				myHighscoreAnimation.transform.localPosition = new Vector2 (0f, 0.08f);
				myHighscoreAnimation.transform.parent = null;

//                GameObject obj = objects_pool_controller.s.reposite_score(0, transform.position.y - my_txt_y_dif);
//                obj.GetComponentInChildren<TextMesh>().text = "DAILY BEST";
//                obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
//                obj.GetComponentInChildren<Score_floor_txt>().my_type = 2;
            }
            else if (USER.s.LAST_SCORE == my_floor)
            {
				myHighscoreAnimation = objects_pool_controller.s.RepositeFloorLastScore ();
				myHighscoreAnimation.transform.parent = transform;
				myHighscoreAnimation.transform.localPosition = new Vector2 (0f, 0.08f);
				myHighscoreAnimation.transform.parent = null;

//                GameObject obj = objects_pool_controller.s.reposite_score(0, transform.position.y - my_txt_y_dif);
//                obj.GetComponentInChildren<TextMesh>().text = "LAST GAME";
//                obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
//                obj.GetComponentInChildren<Score_floor_txt>().my_type = 3;
            }
        }

    }
	
	// Update is called once per frame
//	void Update () {
//		if(transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT*4){
//			//Destroy(gameObject);
//		}
//    }

    public void try_to_display_best_score() // TBD FAZER UMA POOL DE SCENARIO OBJECTS
    {
       // Debug.Log("user day score: " + USER.s.DAY_SCORE);
        if (already_blinked == false && USER.s.DAY_SCORE >= 4 )

        {
            //USER.s.LAST_SCORE + 1 == my_floor
            if (USER.s.DAY_SCORE + 1 == my_floor  || USER.s.BEST_SCORE + 1 == my_floor)
            {

                int i;
//                scenario_objects[] allScenario = GameObject.FindObjectsOfType(typeof(scenario_objects)) as scenario_objects[];
				count_blink = 35;
//                for (i = 0; i < allScenario.Length; i++)
//                {
//                    if (allScenario[i].i_am_floor == false)
//                        allScenario[i].try_blink(my_floor);
//                    else
//                       blink_color_mine();
//                }
//				blink_color_mine();


                if (USER.s.BEST_SCORE + 1 == my_floor)
                {
					Debug.Log ("!!!!NEW HIGHSCORE");
					myHighscoreAnimation = objects_pool_controller.s.RepositeFloorNewHighscore ();
					myHighscoreAnimation.transform.parent = transform;
					myHighscoreAnimation.transform.localPosition = new Vector2 (0.131f, 0.07f);
//                    destroy_previous_score_create_new(1);
                }
                else if (USER.s.DAY_SCORE + 1 == my_floor)
                {
                    destroy_previous_score_create_new(2); 
                }
                else if (USER.s.LAST_SCORE + 1 == my_floor)
                {
                    destroy_previous_score_create_new(3);  
                }
            }
        }
    }

    void blink_color_mine() {
		my_skin.GetComponent<SpriteRenderer>().color = Color.yellow;
		if (my_hole != null) {
			if (my_hole.my_skin !=null) my_hole.my_skin_sprite.color = Color.yellow;
		}
        Invoke("blink_back_mine", 0.15f);
    }


    void blink_back_mine() {
        count_blink -= 1;
        my_skin.GetComponent<SpriteRenderer>().color = Color.white;
		if (my_hole != null) {
			if (my_hole.my_skin !=null)  my_hole.my_skin_sprite.color = Color.white;
		}

		if (count_blink > 0)
			Invoke ("blink_color_mine", 0.15f);
		else
			my_skin.GetComponent<SpriteRenderer> ().color = Color.black;
    }

    //Under floor score destroy
    void destroy_previous_score_create_new(int score_type)
    {
		print (";;;;;;;;;;; DESTROY AND CREATE NEW");

        Score_floor_txt[] Score_txt = GameObject.FindObjectsOfType(typeof(Score_floor_txt)) as Score_floor_txt[];
        int i;

        for (i = 0; i < Score_txt.Length; i++)
        {
            Score_txt[i].try_destroy_me(my_floor - 1, score_type);
            Score_txt[i].try_destroy_me(my_floor, score_type);
        }

        create_score_text(score_type);

    }
		
  

	#endregion

	#region === SUPER JUMP ===

    public void activate_colider_super_pw(int floor_actual)
    {
		//if (gameObject.
//        colliderPW.SetActive(true);
//        colliderPW.GetComponent<Rigidbody2D>().isKinematic = false;

        if(my_floor > floor_actual && my_floor < floor_actual+6 && transform.position.x>=0)
        {
           objects_pool_controller.s.reposite_squares_floor_particle(0, transform.position.y);
        }
    }

    public void unactivate_colider_super_pw()
    {
//        colliderPW.SetActive(false);
    }

    public void colidded_super_pw()
    {
        pw_super_collided = true;
       // transform.GetComponent<SpriteRenderer>().enabled = false;
        my_skin.transform.GetComponent<SpriteRenderer>().enabled = false;
    }

	public void reaper_post_PW_super(int floor)
	{
		if(my_floor > floor)
		{
			pw_super_collided = false;
			transform.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	#endregion

	#region === POOL LOGIC ====

    public void clear_flags_reposite()
    {
//        if(my_text != null)
//        {
//            Destroy(my_text);   
//        }
//		Invoke ("clear_flags_reposite", 2f);
//		if(!pauta.activeInHierarchy) pauta.SetActive (true);

		my_hole = null;
        count_blink = 16;
		repositionable = false;
        pw_super_collided = false;
        my_skin.transform.GetComponent<SpriteRenderer>().enabled = true;
        my_skin.GetComponent<SpriteRenderer>().color = Color.black;

		if (myHighscoreAnimation != null) {
			myHighscoreAnimation.transform.parent = null;
		}
        
		if (isActiveAndEnabled) {
			foreach (floor_note a in my_notes_fg) {
				a.mySR.enabled = false;
				a.already_appeared = false;
			}
		}
        
    }

	public void reposite_me_at_the_other_corner(float ball_position, int floor)
	{
		if (repositionable && floor == my_floor)
		{
			Debug.Log (" FLOOR REPOSITIONING.. ");
			if (ball_position < 0)
				transform.position = new Vector2(0 - Mathf.Abs(0 - transform.position.x), transform.position.y);
			else 
				transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
		}
		//if (my_hole != null)
			//my_hole.transform.position = transform.position;
	}

	#endregion

   /* void destroy_me_baby()
    {
        Destroy(gameObject);
    }*/
	#region == Zold ===
	public void create_score_game_over (int floor, int type) // TBD NAO USAR O FIND OBJECTS
	{
		print (";;;;;;;;;;; GAME OVER NEW HIGHSCORE");
		if(floor == my_floor)
		{
			//destroy previous scores
			Score_floor_txt[] Score_txt = GameObject.FindObjectsOfType(typeof(Score_floor_txt)) as Score_floor_txt[];
			int i;

			for (i = 0; i < Score_txt.Length; i++)
			{
				Score_txt[i].try_destroy_me(0, 0, true);
			}

			//create new
			create_score_text(type);
			blink_color_mine();

			//            scenario_objects[] allScenario = GameObject.FindObjectsOfType(typeof(scenario_objects)) as scenario_objects[];
			//
			//            for (i = 0; i < allScenario.Length; i++)
			//            {
			//                if (allScenario[i].i_am_floor == false)
			//                    allScenario[i].try_blink(my_floor);
			//                else
			//                    blink_color_mine();
			//            }
		}


	}
	public void create_score_text(int score_type) // TBD BAD PERFORMANCE... PEGAR DA POOL
	{
		print (";;;;;;;;;;; CREATE SCORE TEXT");

		Score_floor_txt[] Score_txt = GameObject.FindObjectsOfType(typeof(Score_floor_txt)) as Score_floor_txt[];
		int i;

		for (i = 0; i < Score_txt.Length; i++)
		{
			Score_txt[i].destroy_same_floor(my_floor);
		}

		// my_text = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
		my_text = objects_pool_controller.s.reposite_score(0, transform.position.y - my_txt_y_dif);
		if (score_type == 1)
		{
			my_text.GetComponentInChildren<TextMesh>().text = "NEW RECORD";
		}
		else if (score_type == 2)
		{
			my_text.GetComponentInChildren<TextMesh>().text = "NEW DAILY RECORD";
		}
		else if (score_type == 3)
		{
			my_text.GetComponentInChildren<TextMesh>().text = "LAST GAME";
		}
	}
	#endregion
}
