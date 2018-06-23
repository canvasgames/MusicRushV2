using UnityEngine;
using System.Collections;
using DG.Tweening;
using FMODUnity;
public class ball_hero : MonoBehaviour
{
    #region ==== Variables Declaration =====

	public Follower[] myFollowers = null;
	public bool iAmLeft;

	Skin mySkinType;
	MusicStyle myStyle;
	public GameObject superJumpEffect;
	public GameObject jetpack;
    public bool is_destroyed = false;

    public GameObject my_trail;
    float target_y = 0;
    bool target_y_reached;

    [HideInInspector]
    public int my_id;
    public bool grounded = false;
    [HideInInspector]
    public int my_floor = 0;
    public bool first_ball = false;
    //private int porro = 1111;

    public Camera camerda;

    public bool son_created = false;
    [HideInInspector]
    public GameObject my_son;
	public GameObject my_alert;
    public GameObject bola;

    public note_trail_behavior my_note_trail;
    bool sight_active = false;
    bool heart_active = false;
    bool facing_right = false;
	public GameObject myShield;

    public GameObject my_skin;

    public GameObject my_light;

    public float time_dif;
    //public GameObject

    Rigidbody2D rb;
	SpriteRenderer mySprite;
    [HideInInspector]
    public Vector2 vetor;

    public GameObject explosion, spike_explosion;

    bool hitted_wall = false;

    public float cam_fall_dist = 0;
	int my_floor_after_super_jump= 0;

    #endregion

    #region ====== Init ========

    void Awake() {
		myFollowers = null;
        rb = transform.GetComponent<Rigidbody2D>();
        my_alert.SetActive(false);
    }
	public void test() {
		Debug.Log("AAAAAAAAAA");

	}

    // START THE DANCE
    void Start() {
        my_id = globals.s.BALL_ID; 
		globals.s.BALL_ID++;
        Debug.Log(my_id+ ": BALL STARTED TIME: " + Time.time );
        time_dif = Time.time;

        //camerda = FindObjectOfType<Camera>;
        //Debug.Log(" SPEED X: " + globals.s.FLOOR_HEIGHT);

        //if(first_ball == true) grounded = true;
        son_created = false;
        //Debug.Log ("INIT NEW BALL !!! MY X SPEED: " + rb.velocity.x);

        // INITIALIZE SKIN AND MUSIC HERE!!!
		if(QA.s.CREATE_NOTE_TRAIL == true) create_note_trail();
//		changeSkinChar();
//        UpdateMySkin();
    }

	void OnEnable() {
		if (first_ball == false && my_skin.activeInHierarchy && myStyle != globals.s.ACTUAL_STYLE) {
			Debug.Log (iAmLeft + " [BALL HERO] UPDATE BALL SKIN ON ENABLE");
			UpdateMySkin ();
		}
	}


	 // FAZER CODIGO QUE PROCURA PRA VER SE "BANDN != NULL, E VAI ATRIBUINDO AS SKINS ATÉ SER NULL"
	public void UpdateMySkin() {
		Debug.Log (iAmLeft + " UPDATE MY SKIN: " + globals.s.ACTUAL_STYLE.ToString () +  " ACT SKIN " + globals.s.ACTUAL_SKIN.skinName + " id: "+globals.s.ACTUAL_SKIN.id );
//		if(globals.s.ACTUAL_STYLE != MusicStyle.Eletro)

//		if (globals.s.ACTUAL_STYLE == MusicStyle.Pop) {
//			globals.s.ACTUAL_SKIN = GD.s.skins [8];
//			globals.s.ACTUAL_STYLE = MusicStyle.Pop;
//		}
//
//		if (globals.s.ACTUAL_STYLE == MusicStyle.Rock) {
//			globals.s.ACTUAL_SKIN = GD.s.skins [5];
//			globals.s.ACTUAL_STYLE = MusicStyle.Rock;
//		}

		myStyle = globals.s.ACTUAL_STYLE;
		mySkinType = globals.s.ACTUAL_SKIN;
		Debug.Log ("searching... " + globals.s.ACTUAL_STYLE.ToString () + globals.s.ACTUAL_SKIN.styleId + "Animator");

		// RESET FOLLOWERS
		if (myFollowers != null) {
			Debug.Log ("[BALL] NO FOLLOWERS !!!!!, BUT HAD");
			for (int i = 0; i < myFollowers.Length; i++) {
				myFollowers [i].transform.position = new Vector2 (10000, 1000);
			}
		}
		myFollowers = null;

		if (globals.s.ACTUAL_SKIN.isBand == false && globals.s.ACTUAL_SKIN.isClothChanger == false) {
			if (Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + globals.s.ACTUAL_SKIN.styleId + "Animator") as RuntimeAnimatorController != null) {
				my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
					Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + globals.s.ACTUAL_SKIN.styleId + "Animator") as RuntimeAnimatorController;
			} else {
				Debug.Log ("BALL SKIN EEEEEEERROR!! ANIMATOR NOT FOUND!");
				my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
					Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + "Animator") as RuntimeAnimatorController;
			}
		} else if (globals.s.ACTUAL_SKIN.isBand == true) {
			if (Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + "Band1" + "Animator") as RuntimeAnimatorController != null) {
				my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
					Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + "Band1" + "Animator") as RuntimeAnimatorController;
			} else {
				Debug.Log ("band error!!!!!! ! ");
				my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
					Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + "Band1Animator") as RuntimeAnimatorController;
			}
				
			//UPDATE BAND! USERSS!
			myFollowers = new Follower[globals.s.ACTUAL_SKIN.bandN - 1];
			Debug.Log ("[BALL BAND] INIT MY FOLLOWERS - SIZE: " + myFollowers.Length);
//			myFollowers = BallMaster.s.GimmeMyFollowers (globals.s.ACTUAL_SKIN.bandN);

			//ADD FOLLOWERS
			for (int i = 0; i < myFollowers.Length; i++) {
				Debug.Log (iAmLeft + i.ToString () + "  zz2 UPDATE MY SKIN: " + globals.s.ACTUAL_STYLE.ToString ());

				if (iAmLeft)
					myFollowers [i] = BallMaster.s.followersBall1 [i];
				else
					myFollowers [i] = BallMaster.s.followersBall2 [i];
				Debug.Log (iAmLeft + i.ToString () + " uu3 UPDATE MY SKIN: " + globals.s.ACTUAL_STYLE.ToString ());
				Debug.Log (i.ToString ()  + " my followER name : " + myFollowers[i].name + " NULL: "+ myFollowers[i] );

				myFollowers [i].gameObject.SetActive (true);
				myFollowers [i].UpdateMySkin (globals.s.ACTUAL_SKIN, i + 2, rb.velocity);

				Debug.Log (iAmLeft + i.ToString () + "usd4 UPDATE MY SKIN: " + globals.s.ACTUAL_STYLE.ToString ());
			} 
		}
		else if (globals.s.ACTUAL_SKIN.isClothChanger == true) {
			Debug.Log ("!!!!!!!!!IS CLOTH CHANGER");
			my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
				Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString ()
					+ "Special" + BallMaster.s.clothChangerCurrent
					+ "Animator") as RuntimeAnimatorController;

//			BallMaster.s.clothChangerCurrent++;

//			my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
//				Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + "Special1Animator") as RuntimeAnimatorController;
		} 

		BallMaster.s.DeactivateUnnusedFollowers ();
//		if (Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + QA.s.Phrase + "Animator") as RuntimeAnimatorController != null) {
//			my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
//				Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + QA.s.Phrase + "Animator") as RuntimeAnimatorController;
//		} else {
//			my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
//			Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + "Animator") as RuntimeAnimatorController;
//		}
//		else
//			my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/EletronicHero") as RuntimeAnimatorController;
	}
//
//    public void changeSkinChar()
//    {
//        if (globals.s.ACTUAL_CHAR == "pop")
//        {
//            my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/PopAnimator") as RuntimeAnimatorController;
//        }
//        else if (globals.s.ACTUAL_CHAR == "rock")
//        {
//            my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/RockAnimator") as RuntimeAnimatorController;
//        }
//        else if (globals.s.ACTUAL_CHAR == "eletronic")
//        {
//            my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/EletronicHero") as RuntimeAnimatorController;
//        }
//		else if (globals.s.ACTUAL_CHAR == "popGaga")
//		{
//			my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/PopGagaAnimator") as RuntimeAnimatorController;
//		}
//		else if (globals.s.ACTUAL_CHAR == "reggae")
//		{
//			my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/ReggaeAnimator") as RuntimeAnimatorController;
//		}
//		else if (globals.s.ACTUAL_CHAR == "rap")
//		{
//			my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/RapAnimator") as RuntimeAnimatorController;
//		}
//    }
//    
	public void Init(){
		my_alert.SetActive (false);
	}

	public void Init_first_ball() {
        if (first_ball == true) {
            if (transform.position.x < 0) {
                rb.velocity = new Vector2(globals.s.BALL_SPEED_X, 0);
            }
            else {
                rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, 0);
            }

            init_my_skin();

			if(myFollowers !=null) StartCoroutine(InitMyFollowersMovement ());
        } 
    }
    
    public void init_my_skin() {
        if (transform.position.x < 0 ) {
            my_skin.transform.localScale = new Vector2(-3, my_skin.transform.localScale.y);
        }
        else if (transform.position.x > 0) {
            my_skin.transform.localScale = new Vector2(3, my_skin.transform.localScale.y);
        }
    }
    
    #endregion

    #region ======= UPDATE ==========

	IEnumerator ShowAlert() {
		yield return new WaitForSeconds (0.05f);

            Debug.Log("SHOWING ALERT!! MY FLOOR: " + my_floor);
            my_alert.SetActive(true);
            my_alert.transform.localScale = new Vector2(2.3f, 0);
            my_alert.transform.DOScaleY(2.3f, 0.12f);
            if (sound_controller.s != null) sound_controller.s.play_alert();

	}

	public void activate_pos_revive() // TBD FOLLOWERS
	{
		if (transform.position.x > 0)
			rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
		else 
			rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);

		init_my_skin ();
	}

	float nextTargetSuperJumpY=0;

    void Update()
    {
		// ================= SUPER JUMP START!!!!!!!!!! =====================
		if (globals.s.PW_SUPER_JUMP == true && transform.position.y >= nextTargetSuperJumpY)
		{
			Debug.Log ("passing through floor!!! N: " + my_floor);

			my_floor++;
			nextTargetSuperJumpY += globals.s.FLOOR_HEIGHT;
			foreach( floor andar in objects_pool_controller.s.floor_scripts){

				if (andar.my_floor == my_floor+1) {
					Debug.Log ("FLOOR FOFFFOUNDO floor!!! N: " + andar.my_floor );
					andar.colidded_super_pw ();
					//Debug.Log ("PW TRIGGER!! MY FLOOR: " + my_floor);
					game_controller.s.ball_up (my_floor);
				}
			}
		}

		// ========================  SHOW ALERT ======================
		if (globals.s.ALERT_BALL == true && son_created == false && game_controller.s.alertDebug == true) {
            globals.s.ALERT_BALL = false;
			//Debug.Log ("!!!!!!!!!!!!!!!!!!! SHOW ALERT NOW !! ");
			StartCoroutine(ShowAlert());
//			Invoke("show_alert", 0.05f);
            //show_alert();
        }

		// ===================== JUMP ============================
		if (globals.s.GAME_STARTED == true && globals.s.CURSOR_IN_PAUSE_BT == false) {
			if ((Input.GetMouseButtonDown (0) || Input.GetKey ("space")) && globals.s.GAME_STARTED == true) {
				StartCoroutine (Jump ());
                Debug.Log ("1JJJJJJJUMP! " + Input.mousePosition.y );
//				Debug.Log ("GAME ALREADY STARTED, JUST JUMP!!!");


			} 
//			else if (Input.GetMouseButtonUp (0) && hud_controller.si.HUD_BUTTON_CLICKED == false) {
//				StartCoroutine (Jump ());
////				Debug.Log ("GAME ALREADY STARTED, JUST JUMP!!!2");
//
//			}
		} else {
			if (QA.s.DONT_START_THE_GAME == false && globals.s.MENU_OPEN == false && globals.s.curGameScreen == GameScreen.MainMenu && 
				(Input.GetMouseButtonDown (0) || Input.GetKey ("space")) && Input.mousePosition.y < 430) {
//				&& Input.mousePosition.y > -7.3f && Input.mousePosition.y < 1.3f
//				Debug.Log ("GAME NOT STARTED YET! MENU: " + globals.s.GIFT_ANIMATION);
//				Debug.Log ("0JJJJJJJUMP! " + Input.mousePosition.y );
				globals.s.GAME_STARTED = true;
				hud_controller.si.start_game_coroutine ();
				Debug.Log ("START GAME: FIRST JUMP!!!");
				StartCoroutine (Jump ());
			}
		}

        symbols_PW_activate();
            
        // SET X SPEED TO MAX EVERY FRAME
        if (!globals.s.PW_SUPER_JUMP && !globals.s.REVIVING)
        {
            if (rb.velocity.x > 0)
                rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);
            else if (rb.velocity.x < 0)
                rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
        }
        //Vector3 abc = new Vector3(0, 0, 90);
        //if (rb.velocity.y != 0)
         //  my_trail.transform.rotation = new Quaternion(0, 0, 110, 0);
        //else
        //   my_trail.transform.rotation = new Quaternion(0, 0, 0, 0);

//
//    }
//
//    void Update()
//    {
        //Debug.Log (" MY X SPEED: " + rb.velocity.x);
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

        if (globals.s.PW_SUPER_JUMP == true && target_y_reached == false && target_y > 0) {
            // main_camera.s.PW_super_jump(transform.position.y);
            if (transform.position.y >= target_y) {
               // hud_controller.si.update_floor(my_floor+4);
                stop_go_up_PW();
            }
        }

        // falling case
        if (rb.velocity.y < -0.02f) grounded = false; //else grounded = true;

       
        #region ================ Ball Up ====================

		if (globals.s.PW_SUPER_JUMP == false && son_created == false && ((transform.position.x <= globals.s.LIMIT_LEFT + globals.s.BALL_R + 0.3f && rb.velocity.x < 0) ||
		    (transform.position.x >= globals.s.LIMIT_RIGHT - globals.s.BALL_R - 0.3f && rb.velocity.x > 0))) {
			// my_light.SetActive(false);
			// Destroy(my_light);
			if (QA.s.TRACE_PROFUNDITY >=1) Debug.Log ("BALL UP: END REACHED!!!!!!! MY POS: " + transform.position.x + " LEFT: " + globals.s.LIMIT_LEFT + " RIGHT: " + globals.s.LIMIT_RIGHT);
			son_created = true;
			float x_new_pos = 0f;

			// define the relative new pos
			if (transform.position.x < 0) {
				x_new_pos = globals.s.LIMIT_LEFT - Mathf.Abs (globals.s.LIMIT_LEFT - transform.position.x);
				//x_new_pos = 0;
			} else {
				x_new_pos = globals.s.LIMIT_RIGHT + Mathf.Abs (globals.s.LIMIT_RIGHT - transform.position.x);
			}

			//Debug.Log("BALL DESTROYED TIME: " + Time.time + " .. TIME DIF: " + (Time.time - time_dif));

			my_son = BallMaster.s.ReturnInactiveBall ();
			my_son.transform.position = new Vector2 (x_new_pos, transform.position.y + globals.s.FLOOR_HEIGHT);

			PW_controller.s.add_ball (my_son.GetComponent<ball_hero> ());
//			BallMaster.s.AddNewBall(my_son.GetComponent<ball_hero>());

			my_son.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-rb.velocity.x, rb.velocity.y);
			//Debug.Log("MMMMMM MY SON VY " + my_son.GetComponent<Rigidbody2D>().velocity.y + " | MY VY: " + rb.velocity.y);
			my_son.GetComponent<ball_hero> ().my_floor = my_floor + 1;
			my_son.GetComponent<ball_hero> ().grounded = grounded;


			globals.s.BALL_Y = my_son.transform.position.y;
			globals.s.BALL_X = my_son.transform.position.x;
			globals.s.CUR_BALL_SPEED = my_son.GetComponent<Rigidbody2D> ().velocity.x;

			my_son.GetComponent<ball_hero> ().init_my_skin ();
			if (grounded == false) { 
				//my_son.GetComponent<ball_hero>().my_skin.GetComponent<Animator>().Play("Jumping", 0,
				//my_skin.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
				my_skin.GetComponent<Animator> ().SetBool ("Jumping", true);
				my_son.GetComponent<ball_hero> ().my_skin.GetComponent<Animator> ().SetBool ("Jumping", true);
				//Debug.Log("aaaaaaanimator: " + my_skin.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
			}

			my_son.GetComponent<ball_hero>().InitMyFollowers();

			if(globals.s.ACTUAL_SKIN.isClothChanger) my_son.GetComponent<ball_hero>().UpdateMySkinAndMakeMeFabolous();


			// CALL GAME CONTROLLER
			game_controller.s.ball_up (my_floor);
			// CALL MAIN CAMERA
			float pos = ((globals.s.BASE_Y + (my_floor + 1) * globals.s.FLOOR_HEIGHT)) + 1f;
			//Debug.Log("================ CALCULATED POS: " + pos + " MYSON POS: " + globals.s.BALL_Y + "my pos: " + transform.position.y);
			if (my_floor >= 1)
				main_camera.s.on_ball_up (pos);

			if (hitted_wall)
				main_camera.s.hitted_on_wall = false;

			// MAKE WALLS POSITION THEMSELVES
			wall[] paredez = GameObject.FindObjectsOfType (typeof(wall)) as wall[]; // TBD
			foreach (wall p in paredez) {
				p.place_me_at_the_other_corner (-my_son.transform.position.x, my_floor + 2);
			}

            // MAKE SAWS POSITION THEMSELVES
            saw[] saws = GameObject.FindObjectsOfType(typeof(saw)) as saw[]; // TBD
            foreach (saw p in saws)
            {
                p.place_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 2);
            }

            if (USER.s.BEST_SCORE <= 5) {
//				floor[] floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
//				foreach(floor p in floors){
//					p.reposite_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 2);
//				}

				hole_behaviour[] holes = GameObject.FindObjectsOfType (typeof(hole_behaviour)) as hole_behaviour[];
				foreach (hole_behaviour p in holes) {
					p.reposite_me_at_the_other_corner (-my_son.transform.position.x, my_floor + 3);
				}

				spike[] spks = GameObject.FindObjectsOfType (typeof(spike)) as spike[];
				foreach (spike p in spks) {
					p.reposite_me_for_FTU (-my_son.transform.position.x, my_floor + 3);
				}
			}

			/*
			hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
			foreach(hole_behaviour p in holes){
				p.place_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 2);
			}*/

			//my_son = (GameObject)Instantiate (ball_hero, new Vector3 (0, 0, 0), transform.rotation);
			//Debug.Log("------------ NEW BALL CREATED! MY ID: " +my_id +" time: " + Time.time + " | pos y: " +my_son.transform.position.y + " CAMERA Y: " + main_camera.s.transform.position.y);

		}
        #endregion

		// ===== DEACTIVATE MYSELF !!!!! ===========
        else if (son_created == true && (transform.position.x < globals.s.LIMIT_LEFT - globals.s.BALL_D ||
		               transform.position.x > globals.s.LIMIT_RIGHT + globals.s.BALL_D)) {
//			Debug.Log ("Destroy me !!!! my pos:" + transform.position.x);

			son_created = false;

			if(myFollowers != null) DeactivateMyFollowers ();
			my_alert.SetActive(false);
			gameObject.SetActive (false);
			//my_light.SetActive(false);
		}
//		else {
//			Debug.Log (my_id + ": NEVER REACH HERE... SON CREATED: " + son_created);
//		}

		if (my_id == BallMaster.s.currentBall) {
            globals.s.BALL_Y = transform.position.y;
            globals.s.BALL_X = transform.position.x;
            globals.s.CUR_BALL_SPEED = rb.velocity.x;
            globals.s.BALL_GROUNDED = grounded;
            //Debug.Log("XY UPDATED | MY ID: " + my_id + " time: " + Time.time + " MY FLOOR " + my_floor + " CUR BALL SPEED: " + globals.s.CUR_BALL_SPEED);
            //globals.s.BALL_FLOOR = my_floor;
        }
    }

	void create_note_trail() {
		//note_trail_behavior obj = (note_trail_behavior)Instantiate(my_note_trail, 
		//new Vector3(transform.position.x, transform.position.y + Random.Range(-0.2f, 0.2f)), transform.rotation);

		objects_pool_controller.s.reposite_note_trail (transform.position.x, transform.position.y + Random.Range (-0.2f, 0.2f));


		if(!is_destroyed) Invoke("create_note_trail",0.07f);
	}

	int clothChangerSkin = 1;
	void UpdateMySkinAndMakeMeFabolous(){
		Invoke ("UpdateSkinGagaForReal", 0.01f);

	}

	void UpdateSkinGagaForReal(){
		BallMaster.s.clothChangerCurrent++;
		if (BallMaster.s.clothChangerCurrent > 3)
			BallMaster.s.clothChangerCurrent = 1;
		Debug.Log (iAmLeft + " !!!!!!!!!ccccccccccccc IS CLOTH CHANGER! cur: " + BallMaster.s.clothChangerCurrent);

		my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
			Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString ()
				+ "Special" + BallMaster.s.clothChangerCurrent
				+ "Animator") as RuntimeAnimatorController;

//		my_skin.GetComponent<Animator> ().SetBool ("jumping", false);
//		my_skin.GetComponent<Animator> ().Play ("Running");
	}

    #endregion

	#region ======= Jump ==========

	public IEnumerator Jump()
    {
        //Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

		if (grounded == false) { // wait a short delay if the player miss press jump in the air
			yield return new WaitForSeconds(0.05f);
		}

		if (grounded == false) { // wait a short delay if the player miss press jump in the air
			yield return new WaitForSeconds(0.05f);
		}

        //GetComponent<EdgeCollider2D>().enabled = false;
		if (grounded == true && gameObject.activeInHierarchy)
        {
            // my_trail.transform.localRotation = new Quaternion(0, 0, 110, 0);
			if (my_trail != null) my_trail.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.01f, RotateMode.Fast);
			sound_controller.s.PlayJump();
            grounded = false;
            //rb.AddForce (new Vector2 (0, y_jump));
            rb.velocity = new Vector2(rb.velocity.x, globals.s.BALL_SPEED_Y);

           // my_skin.GetComponent<Animator>().Play("Jumping");
            my_skin.GetComponent<Animator>().SetBool("Jumping", true);

//			if(myFollowers != null) StartCoroutine (JumpMyFollowers ());
			if(myFollowers != null) BallMaster.s.IEnumeratorJumpMyFollowers(iAmLeft);
        }
        //else Debug.Log("ÇÇÇÇÇÇÇÇÇÇÇÇÇÇÇ CANT JUMP! I AM NOT GROUNDED");
    }

    void Land() {
//		Debug.Log ("[BALL " + (iAmLeft) + "] LANDING!!!!");

		if (my_trail != null) my_trail.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.01f, RotateMode.Fast);

		if (myFollowers!= null) {
			LandMyFollowers ();
		}
    }

	#endregion

	#region === FOLLOWERS ===


	public void InitMyFollowers(){
		if(myFollowers != null) BallMaster.s.IEnumeratorInitFollowersMovement (iAmLeft, transform.position, rb.velocity);
//		if(myFollowers != null) StartCoroutine(InitMyFollowersMovement());
	}

	public IEnumerator InitMyFollowersMovement(){
//		Debug.Log ("IIIIINIT FL. TIME: " + Time.time + " MY POS: " + transform.position);
		if (myFollowers != null && myFollowers.Length > 0) {
			Vector2 myPos = transform.position;
//		foreach (Follower f in myFollowers) {
			for (int i = 1; i <= myFollowers.Length; i++) {
				yield return new WaitForSeconds (GD.s.FOLLOWER_DELAY);
				if (myFollowers != null) {
					Follower f = myFollowers [i - 1];
//			Debug.Log (i+" UPDATING POS FOR FOLLOWER " + myPos + " FOLLOWER LENGHT: " + myFollowers.Length + " TTTIME: "+  (GD.s.FOLLOWER_DELAY_BASE + GD.s.FOLLOWER_DELAY * i));
//			Debug.Log ("REAL TIME: " +Time.time);
					f.gameObject.SetActive (true);
					f.transform.position = myPos;
					f.InitMovement (rb.velocity);
				}
			}
		}
	}

	IEnumerator JumpMyFollowers(){
		int i = 0;
		foreach (Follower f in myFollowers) {
			yield return new WaitForSeconds (GD.s.FOLLOWER_DELAY );
			if (f != null) {
//			f.rb.velocity = new Vector2(rb.velocity.x, globals.s.BALL_SPEED_Y);
				f.JumpOn ();
				i++;
			}
		}
	}

	void LandMyFollowers(){
		int i = 1;
		foreach (Follower f in myFollowers) {
//			StartCoroutine (f.LandOn (GD.s.FOLLOWER_DELAY * i, transform.position.y));
			f.IenumeratorLandOn (GD.s.FOLLOWER_DELAY * i);
			i++;
		}
	}

	void WallCollisionForMyFollowers (){
		int i = 1;
		foreach (Follower f in myFollowers) {
			f.IenumeratorWallCollision (GD.s.FOLLOWER_DELAY * i);
			i++;
		}
	}


	void DeactivateMyFollowers(){
		int i = 1;
		foreach (Follower f in myFollowers) {
			f.DeactivateMe(GD.s.FOLLOWER_DELAY * i);
			i++;
		}
	}

	void KillMyFollowersWithPainfullDeathsAndSendTheirSoulsToOurMightySatan() {
		int i = 1;
		BallMaster.s.IEnumeratorforDeath (iAmLeft, transform.position);

//		foreach (Follower f in myFollowers) {
////			StartCoroutine (f.LetMeSacrificeMyselfForTheGreaterGood(GD.s.FOLLOWER_DELAY * i));
//			f.KillMe(GD.s.FOLLOWER_DELAY * i);
//			i++;
//		}
	}


		
	#endregion

	#region ============ COLLISIONS ================


	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log ("kkkkkkkkkkkkkkkkkCOLLISION IS HAPPENING!! ");
		if (coll.gameObject.CompareTag ("PW")) {
			
			PW_Collect temp = coll.gameObject.GetComponent<PW_Collect> ();
			coll.transform.position = new Vector2 (-9909,-9999);
			pw_do_something (temp);
			if(sound_controller.s != null) sound_controller.s.play_collect_pw ();
		} 
		else if (coll.gameObject.CompareTag ("Spike")) {
			if (globals.s.PW_SUPER_JUMP == false && !QA.s.INVENCIBLE) {
				if (globals.s.PW_INVENCIBLE == false) {
					destroy_me (coll.gameObject.GetComponent<spike> ().wave_name);
					//coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
				} else {
					// Destroy(coll.gameObject);
					Instantiate (spike_explosion, new Vector3 (coll.transform.position.x, coll.transform.position.y, coll.transform.position.z), transform.rotation);
					PW_controller.s.invencible_end ();
					coll.gameObject.transform.position = new Vector3 (coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
				}
			}
		}
        else if (coll.gameObject.CompareTag("Saw"))
        {
            if (globals.s.PW_SUPER_JUMP == false && !QA.s.INVENCIBLE)
            {
                if (globals.s.PW_INVENCIBLE == false)
                {
                    destroy_me(coll.gameObject.GetComponent<saw>().wave_name);
                    //coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
                }
                else {
                    // Destroy(coll.gameObject);
                    Instantiate(spike_explosion, new Vector3(coll.transform.position.x, coll.transform.position.y, coll.transform.position.z), transform.rotation);
                    PW_controller.s.invencible_end();
                    coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
                }
            }
        }
        else if (coll.gameObject.CompareTag("Wall"))
		{
			rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
			my_skin.transform.localScale = new Vector2(-my_skin.transform.localScale.x, my_skin.transform.localScale.y);

			if (myFollowers != null)
				WallCollisionForMyFollowers ();

			if (transform.position.y < main_camera.s.transform.position.y - 10f) {
				hitted_wall = true;
				main_camera.s.hitted_on_wall = true;
			}   
		}

		else if (coll.gameObject.CompareTag("Note")) {
			USER.s.NOTES += 1;
			USER.s.TOTAL_NOTES += 1;
			globals.s.NOTES_COLLECTED += 1;
			globals.s.NOTES_COLLECTED_JUKEBOX += 1;
			hud_controller.si.display_notes(USER.s.NOTES);
			coll.transform.position = new Vector2 (-9909,-9999);
			coll.GetComponent <note_behaviour> ().active = false;
			coll.GetComponent <note_behaviour> ().state = 0;
			//Destroy(coll.gameObject);
			//Debug.Log("COLLECTING NOTE !!!!!!! ");
			if(sound_controller.s != null) sound_controller.s.special_event();
		}

		else if (coll.gameObject.CompareTag("HoleFalling")) {
			//if (QA.s.TRACE_PROFUNDITY >= 3) Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
			if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
			//coll.transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			//coll.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			//grounded = false;
			//coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100f);
			if (transform.position.y < main_camera.s.transform.position.y + cam_fall_dist) { 
				main_camera.s.OnBallFalling();
				//coll.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

			}

		}
	}

	void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("xxxxxxxxxxxxxxxxxxxxx COLLIDING WITH SOMETHING!");


        if (coll.gameObject.CompareTag("Floor")) {
            
			if (coll.transform.position.y + coll.transform.GetComponent<floor>().my_skin.GetComponent<SpriteRenderer>().bounds.size.y / 2 <= transform.position.y - globals.s.BALL_R + 1f) {
                //rb.AddForce (new Vector2 (0, 0));
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //my_skin.GetComponent<Animator>().Play("Running");
                my_skin.GetComponent<Animator>().SetBool("Jumping", false);
                //transform.position = new Vector2(transform.position.x, coll.transform.position.y + coll.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2 + globals.s.BALL_R);
                my_floor = coll.gameObject.GetComponent<floor>().my_floor;
                //Debug.Log(my_id + " KKKKKKKKKKKKKKKKKK KOLLISION! MY NEW FLOOR: " + my_floor + " I AM GROUNDED ");

                if (globals.s.PW_SUPER_JUMP) {
                    pw_super_end_for_real();
                }

                grounded = true;
                Land();

//                coll.gameObject.GetComponent<floor>().try_to_display_best_score();
            }
            else { Debug.Log("" + my_id + " ***************ERROR! THIS SHOULD NEVER HAPPEN ***************\n\n"); }
        }

//        else if (coll.gameObject.CompareTag("Spike")) {
//            if (globals.s.PW_SUPER_JUMP == false && !QA.s.INVENCIBLE)
//            {
//                if (globals.s.PW_INVENCIBLE == false)
//                {
//                    destroy_me(coll.gameObject.GetComponent<spike>().wave_name);
//                    //coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
//                }
//                else
//                {
//                    // Destroy(coll.gameObject);
//                    Instantiate(spike_explosion, new Vector3(coll.transform.position.x, coll.transform.position.y, coll.transform.position.z), transform.rotation);
//                    PW_controller.s.invencible_end();
//                    coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
//                }
//            }
//            else
//            {
//                Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
//                GetComponent<SpriteRenderer>().color = Color.blue;
//                Invoke("back_to_normal_color", 0.2f);
//
//            }
//        }
//

        /*else if (coll.gameObject.CompareTag("Hole")) {
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
            my_skin.GetComponent<Animator>().Play("Jumping");
        }*/

        
//        else if (coll.gameObject.CompareTag("Wall"))
//        {
//            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
//            my_skin.transform.localScale = new Vector2(-my_skin.transform.localScale.x, my_skin.transform.localScale.y);
//
//            if (transform.position.y < main_camera.s.transform.position.y - 10f) {
//                hitted_wall = true;
//                main_camera.s.hitted_on_wall = true;
//            }   
//        }
		
       /* else if (coll.gameObject.CompareTag("PW"))
        {
            PW_Collect temp = coll.gameObject.GetComponent<PW_Collect>();
            pw_do_something(temp);
            sound_controller.s.play_collect_pw();
			Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }*/
        else if (coll.gameObject.CompareTag("Revive"))
        {
            globals.s.CAN_REVIVE = true;
            Destroy(coll.gameObject);
        }
//        if (globals.s.PW_SUPER_JUMP == true)
//        {
//			if (coll.gameObject.CompareTag("PW_Trigger") && my_floor < my_floor_after_super_jump)
//            {
//				if (coll.gameObject.GetComponent<floor_pw_collider> () != null) {
//					coll.gameObject.GetComponent<floor_pw_collider> ().unactive_sprite_daddy ();
//					//Debug.Log ("PW TRIGGER!! MY FLOOR: " + my_floor);
//					game_controller.s.ball_up (my_floor);
//					my_floor++;
//				}
//            }
//        }
    }


	void destroy_me(string killer_wave_name) { //TBD PEGAR A OUTRA BOLA DO BALL MASTER, PEGAR OS FLOOR DA POOL

		if (myFollowers != null) {
			KillMyFollowersWithPainfullDeathsAndSendTheirSoulsToOurMightySatan ();
		}

		ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];

		foreach (ball_hero b in bolas) {
			//Destroy(b.gameObject);
			b.gameObject.SetActive(false);
			b.is_destroyed = true;

		}
		bool with_high_score = false;

//		if(my_floor > USER.s.BEST_SCORE)
//		{
//			with_high_score = true;
//			floor[] chaozis = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
//			int i;
//			for (i = 0; i < chaozis.Length; i++)
//			{
//				chaozis[i].create_score_game_over(my_floor, 1);
//			}
//		}
//		else if(my_floor > USER.s.DAY_SCORE)
//		{
//			with_high_score = true;
//			floor[] chaozis = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
//			int i;
//			for (i = 0; i < chaozis.Length; i++)
//			{
//				chaozis[i].create_score_game_over(my_floor,2);
//			}
//		}

		if(sound_controller.s != null) sound_controller.s.PlayExplosion();
		Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

		game_controller.s.game_over(killer_wave_name, bolas, with_high_score);

	}


    #endregion

    public void send_actual_balls() {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];

        game_controller.s.store_unactive_balls(bolas);

        foreach (ball_hero b in bolas) {
            //Destroy(b.gameObject);
            b.gameObject.SetActive(false);
        }
    }

    void pw_do_something(PW_Collect temp)
    {
		//USER.s.FIRST_PW_CREATED = 1;
		//PlayerPrefs.SetInt("first_pw_created", 1);

       // temp.collect();
        if(globals.s.PW_INVENCIBLE == true)
        {
            PW_controller.s.invencible_end();
        }
        else if(globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            PW_controller.s.sight_end();
        }

      //  Debug.Log("Tipodo PW q peguei " + temp.pw_type);
        if (temp.pw_type == (int) PW_Types.Invencible)
        {
            PW_controller.s.invencible_start();
			globals.s.pwShieldCollected++;
        }
        else if (temp.pw_type == (int)PW_Types.Super)
        {
            go_up_pw_start();
			globals.s.pwSuperJumpCollected++;
        }
        else if((temp.pw_type == (int)PW_Types.Sight))
        {
            PW_controller.s.PW_sight_start();
			globals.s.pwVisionCollected++;
        }
    }

    #region ==== POWER UP -> GO UP ====
    void go_up_pw_start()
    {
		if(QA.s.TRACE_PROFUNDITY >=2) Debug.Log ("PW GO UP START!! MY FLOOR: " + my_floor);
		my_floor_after_super_jump = my_floor + 5;
		jetpack.SetActive(true);
        my_alert.SetActive(false);
        rb.velocity = new Vector2(0, 0);
       // my_skin.GetComponent<Animator>().Play("Jumping");
        my_skin.GetComponent<Animator>().SetBool("Jumping", true);

        rb.gravityScale = 0;
        rb.isKinematic = true;

		nextTargetSuperJumpY = ((globals.s.BASE_Y + ((my_floor+2) * globals.s.FLOOR_HEIGHT) - 0.7f));
		float pos = ((globals.s.BASE_Y + ((my_floor+1) * globals.s.FLOOR_HEIGHT) +  (5* globals.s.FLOOR_HEIGHT) + globals.s.FLOOR_HEIGHT / 2 ));
		main_camera.s.init_PW_super_jump( pos,  (pos-transform.position.y)/20  + 0.5f);
		target_y = (globals.s.BASE_Y + ((my_floor) * globals.s.FLOOR_HEIGHT) +  (5* globals.s.FLOOR_HEIGHT) + globals.s.FLOOR_HEIGHT / 2 - 0.6f );

        //construct floors
        int i;
        int temp = my_floor;
        globals.s.PW_SUPER_JUMP = true;

//        for (i = my_floor + 1; i <= temp + 5; i++) {
//           game_controller.s.create_new_wave();
//        }

        //activate squares of collisions
        activate_particles_floor();
        //main_camera.s.init_PW_super_jump(transform.position.y + 5 * globals.s.FLOOR_HEIGHT + 2f, 5 * (globals.s.FLOOR_HEIGHT / 20) + 0.5f);
        
		if(QA.s.TRACE_PROFUNDITY >=2) Debug.Log ("PW GO UP start end, MY FLOOR:  " + my_floor);

        Invoke("go_up_PW", 0.2f);
    }

    void go_up_PW() {
		if(QA.s.TRACE_PROFUNDITY >=2) Debug.Log ("PW GO UP START STEP 2!! MY FLOOR START:  " + my_floor);
        //globals.s.PW_SUPER_JUMP = true;
        desactivate_pws_super();
		superJumpEffect.SetActive (true);

        int ball_speed = 20;
        target_y_reached = false;

        rb.velocity = new Vector2(0, ball_speed);
        float dist = (target_y - transform.position.y);
      //  Debug.Log("[GO UP PW] Dist: " + dist + " OLD Dist: " + (5*globals.s.FLOOR_HEIGHT) + " dist/speed: " + (dist / ball_speed) + " | OLD dist/speed: " + ((5 *globals.s.FLOOR_HEIGHT) / ball_speed));
      //  Debug.Log("[GO UP PW] MY POS: " + transform.position.y + " BASE Y+FLOOR "+ (globals.s.BASE_Y + my_floor * globals.s.FLOOR_HEIGHT) + " BASE Y: " + globals.s.BASE_Y + " MY FLOOR: " + my_floor);
        //Invoke("stop_go_up_PW", ( dist / ball_speed));
        //Invoke("stop_go_up_PW", ((globals.s.BASE_Y + (my_floor * globals.s.FLOOR_HEIGHT) + 5* globals.s.FLOOR_HEIGHT + (globals.s.FLOOR_HEIGHT/2) ) - transform.position.y) / ball_speed);

        //transform.DOMoveY(target_y, 1.1f).SetEase(Ease.Linear).OnComplete(()=> stop_go_up_PW());
		if(QA.s.TRACE_PROFUNDITY >=2) Debug.Log ("PW GO UP START STEP 2!! MY FLOOR end:  " + my_floor);

    }

    void stop_go_up_PW()
    {
		if(QA.s.TRACE_PROFUNDITY >=2)  Debug.Log("[GOUPPW] FINISHED GOING UP ! MY Y: " + transform.position.y);
        rb.velocity = new Vector2(0.3f, globals.s.BALL_SPEED_Y/2);
        rb.gravityScale = 0.5f;
        rb.isKinematic = false;
        target_y = 0;

        target_y_reached = true; // bla

        unactivate_particles_floor();
        
        Invoke("create_floor", 0.2f);
    }
	

    void create_floor()
    {
		jetpack.SetActive(false);
        //my_floor += 5;
        appear_floors();
		if(QA.s.TRACE_PROFUNDITY >=2) Debug.Log ("TIME TO CREATE FLOOR FOR LANDING! !! ");
		GameObject floor = game_controller.s.create_floor(12, my_floor+1, false, true);
        destroy_spikes();
		floor.GetComponent<floor> ().pauta.SetActive (false);
        floor.transform.DOMoveX(0, 0.3f);//.OnComplete(pw_super_end);

		superJumpEffect.SetActive (false);

    }

   void pw_super_end_for_real() {
        globals.s.PW_SUPER_JUMP = false;
        squares_desappear();
        rb.gravityScale = 1;
		main_camera.s.pw_super_jump_end();

        if (transform.position.x > 0)
            rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
        else
            rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);

		init_my_skin ();

        Invoke("unactivate_squares", 0.3f);
    }

    void activate_particles_floor() {  
        int i=0;
    	floor[] floors = null;
       	//floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
		floors = objects_pool_controller.s.floor_scripts;
        for (i = 0; i < floors.Length; i++)
          {
			if (floors[i] != null && floors[i].isActiveAndEnabled)
				floors[i].activate_colider_super_pw(my_floor);
				//Debug.Log ("game object is active: " + floors [i].isActiveAndEnabled);
          }

		floor_square_pw_destruct[] squares = null;
		//floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
		squares = objects_pool_controller.s.squares_floor_scripts;
		for (i = 0; i < squares.Length; i++)
		{
			if (squares [i] != null && squares [i].isActiveAndEnabled)
				squares [i].DisappearSoon ();
			//Debug.Log ("game object is active: " + floors [i].isActiveAndEnabled);
		}

		hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
       // hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];

        for (i = 0; i < holes.Length; i++)
        {
            holes[i].activate_squares();
        }

        wall[] walls = GameObject.FindObjectsOfType(typeof(wall)) as wall[];

        for (i = 0; i < walls.Length; i++)
        {
            walls[i].destroy_me_PW_super();
        }

    }

    void appear_floors()
    {
		floor[] floors = null;
        int i;
		floors = objects_pool_controller.s.floor_scripts;
		for (i = 0; i < floors.Length; i++) {
			if (floors[i] != null && floors [i].isActiveAndEnabled) {
				floors [i].reaper_post_PW_super (my_floor);
				//floors [i].activate_colider_super_pw (my_floor);
			}
		}
    }

    void unactivate_particles_floor()
    {
         //destroy_spikes();
         int i;
		floor[] floors = objects_pool_controller.s.floor_scripts;
          for (i = 0; i < floors.Length; i++)
          {
			if (floors[i] != null && floors[i].isActiveAndEnabled)
				floors[i].unactivate_colider_super_pw();
             }

          hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];

          for (i = 0; i < holes.Length; i++)
          {
              holes[i].destroy_pw_super_under_floors(transform.position.y);
          }

    }

    //Called by invoke
    void unactivate_squares()
    {
        squares_desappear();
    }
    void squares_desappear()
    {
        int i;

		floor_square_pw_destruct[] squares = objects_pool_controller.s.squares_floor_scripts;
       // floor_square_pw_destruct[] squares = GameObject.FindObjectsOfType(typeof(floor_square_pw_destruct)) as floor_square_pw_destruct[];
        for (i = 0; i < squares.Length; i++)
        {
            squares[i].scale_down_to_dessapear();
        }

       objects_pool_controller.s.clear_squares_floor_particle();
    }


    void destroy_spikes()
    {
        int i;
		spike[] spikes = objects_pool_controller.s.spikes_scripts;
        for (i = 0; i < spikes.Length; i++)
        {
			if(spikes[i] !=null)  spikes[i].destroy_throwed_spikes(transform.position.y);
        }
    }


    void desactivate_pws_super()
    {
        int i;
		PW_Collect[] pws = objects_pool_controller.s.pw_scripts;
        for (i = 0; i < pws.Length; i++)
        {
			if(pws[i] !=null) pws[i].destroy_by_floor_PW_Super(my_floor + 6);
        }
    }
    #endregion

    #region POWER UP -> SYMBOLS PW
    void symbols_PW_activate()
    {
        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true && sight_active == false)
        {
           // sight_start();
        }
        else if ((globals.s.PW_SIGHT_BEYOND_SIGHT == false && sight_active == true))
        {
           // sight_end();
        }

        if (globals.s.PW_INVENCIBLE == true && heart_active == false) {
            //heart_start();
			heart_active = true;
			myShield.SetActive (true);
        }
        else if (globals.s.PW_INVENCIBLE == false && heart_active == true) {
            //heart_end();
			myShield.SetActive (false);
			heart_active = false;
        }
    }
    #endregion


    #region =========== DEBUG ================
    void back_to_normal_color() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    #endregion

}
