using UnityEngine;
using System.Collections;
using DG.Tweening;

public class main_camera : MonoBehaviour {

    private Rigidbody2D rb;
    bool initiated= false;
    public bool moving = false;
    public bool falling = false;
    public bool pw_super_jump = false;
    public static main_camera s;
    public bool hitted_on_wall = false;
	public float  yStart;
	public Vector2 targetCenterPos;

    void Awake (){ s = this; }
  
     // Use this for initialization
    void Start()
    {
		yStart = transform.position.y;
        rb = transform.GetComponent<Rigidbody2D>();
		Debug.Log ("[cam] YSTART!! " + yStart);

        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 9.6f / 16.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }

		transform.position = new Vector3(0, -1.24f, -10);
    }


    public void OnBallFalling() {
        if (!falling) {
            falling = true;
            float target_dif = 0;
            float dif = (transform.position.y - globals.s.BALL_Y);
            if (dif > 1)
                target_dif = globals.s.FLOOR_HEIGHT - 0.5f;
            else { 
                target_dif = globals.s.FLOOR_HEIGHT - 0.5f;
                target_dif = target_dif / 2 + target_dif/4 + (((dif*target_dif)/100) * (target_dif/2)) ;

            }

            Debug.Log("vvvvvvvvvvvvvvvvvvvvv [CAMERA] ON BALL FALLING !! TDIF: " + target_dif + " [] YD: " + (transform.position.y - globals.s.BALL_Y) + " MY POSITION: " + transform.position.y + "  MY TARGET Y " + (transform.position.y - globals.s.FLOOR_HEIGHT - 0.5f));

            transform.DOMoveY(transform.position.y - target_dif, 0.4f).SetEase(Ease.InOutQuad).OnComplete(() => falling = false);
        }
    }

    public void OnBallTooHigh() {
        if (!falling) {
            Debug.Log("[CAMERA] ON BALL TOO HIGH !! BALL Y: " + globals.s.BALL_Y + " | LIMIT: " + (transform.position.y + globals.s.FLOOR_HEIGHT + 1.5f));
            falling = true;
            transform.DOMoveY(transform.position.y + globals.s.FLOOR_HEIGHT + 0.5f, 0.4f).SetEase(Ease.InOutQuad).OnComplete(() => falling = false);
        }
        else Debug.Log("[CAMERA] IT IS FALLING! NOT BALL TO HIGH =/");
    }

    public void init_PW_super_jump(float pos_y, float time)
    {
        transform.DOMoveY(pos_y, time).SetEase(Ease.InOutSine);
        pw_super_jump = true;
    }
   
	public void PW_super_jump(float pos_y) {
        transform.position = new Vector3(transform.position.x, pos_y, transform.position.z);
    }

    public void pw_super_jump_end()
    {
        //Debug.Log("[CAMERA] SUPER JUMP END !! ");
//        pw_super_jump = false;
		Invoke("pw_super_jump_end_for_real", 0.3f);
    }

	void pw_super_jump_end_for_real(){
		pw_super_jump = false;
	}

    public void on_ball_up(float ball_y) {
        if (!moving && ball_y > transform.position.y - globals.s.FLOOR_HEIGHT / 4)//Debug.Log("MY Y POS: " + transform.position.y);  if (globals.s.BALL_Y > transform.position.y)
        {
            rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
            moving = true;
        }
    }

	void Update() {

		//transform.position = new Vector3 (0, 0,0);
		if (pw_super_jump == false && globals.s.REVIVING == false && globals.s.GAME_STARTED == true)
		{
			if (globals.s.GAME_OVER == 0 && !falling && !hitted_on_wall) {
				if (initiated == false)
				{
					Debug.Log ("[aacam] INIT Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf) +" REAL Y: " + globals.s.BALL_Y);
					Debug.Log ("[aacam2] INIT Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf2) + " / DIF X : " + (globals.s.BALL_X - QA.s.jokerf3));

					//if ball is in a superior position than the camera
					if (globals.s.BALL_CUR_FLOOR_Y > transform.position.y + QA.s.jokerf || 
							(globals.s.BALL_CUR_FLOOR_Y> transform.position.y + QA.s.jokerf2 &&
							((globals.s.BALL_X > QA.s.jokerf3 && globals.s.BALL_SPEED_X > 0) ||
							(globals.s.BALL_X > QA.s.jokerf3 && globals.s.BALL_SPEED_X < 0)))) {
							Debug.Log ("[cam] FOR REEEEAL DIF Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf3) + " / DIF X : " + (globals.s.BALL_X - QA.s.jokerf3));
						rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
						initiated = true;
						moving = true;
					}
				}
				else
				{
					
					//if ball is in a superior position than the camera
					if (globals.s.BALL_CUR_FLOOR_Y> transform.position.y + QA.s.jokerf || 
							(globals.s.BALL_CUR_FLOOR_Y> transform.position.y + QA.s.jokerf2 &&
							((globals.s.BALL_X > QA.s.jokerf3 && globals.s.BALL_SPEED_X > 0) ||
							(globals.s.BALL_X > QA.s.jokerf3 && globals.s.BALL_SPEED_X < 0)))) {
//						QA.s.TIMESCALE = 1;
//						Debug.Log ("[cam] moooooove for real " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf));
						Debug.Log ("[cam] KEEEP MOVING BALL Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf));
						Debug.Log ("[cam2] KEEEP MOVING DIF Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf2) + " / DIF X : " + (globals.s.BALL_X - QA.s.jokerf3));

						if (moving == false) {
							rb.velocity = new Vector2 (0, globals.s.CAMERA_SPEED);
							moving = true;
							Debug.Log ("sssssssssssssssssssssssssssss");
							Debug.Log ("[LLcam] KEEEP MOVING BALL Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf));
							Debug.Log ("[LLcam2] KEEEP MOVING DIF Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf2) + " / DIF X : " + (globals.s.BALL_X - QA.s.jokerf3));
						}
					} else if (globals.s.BALL_CUR_FLOOR_Y< transform.position.y + QA.s.jokerf3) {
						Debug.Log ("[cam stop] PAAAAAAUSE MOV DIF Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf));
						Debug.Log ("[cam stop] PAAAAAAUSE MOV DIF Y: " + (globals.s.BALL_CUR_FLOOR_Y - transform.position.y - QA.s.jokerf2) + " / DIF X : " + (globals.s.BALL_X - QA.s.jokerf3));
//						QA.s.TIMESCALE = 0.1f;
						if (moving == true) {
							QA.s.TIMESCALE = 0;
							moving = false;
							Debug.Log ("XXXXXXXXXXXXXXXXX");
							Debug.Log ("[XXXXXcam stop] PAAAAAAUSE MOV DIF Y: " + (globals.s.BALL_CUR_FLOOR_Y- transform.position.y - QA.s.jokerf));
							Debug.Log ("[XXXXcam stop] PAAAAAAUSE MOV DIF Y: " + (globals.s.BALL_CUR_FLOOR_Y - transform.position.y - QA.s.jokerf2) + " / DIF X : " + (globals.s.BALL_X - QA.s.jokerf3));
							rb.velocity = new Vector2 (0, 0);
						}
					}

//					// ball is to high
//					if (globals.s.GAME_STARTED && globals.s.BALL_FLOOR > 1 && globals.s.BALL_GROUNDED && globals.s.PW_SUPER_JUMP == false &&
//						globals.s.BALL_Y > transform.position.y + globals.s.FLOOR_HEIGHT + 1.5f) { 
//
//						OnBallTooHigh();
//					}
//
//					// stop camera if ball is too low
//					else if (moving && globals.s.BALL_Y < transform.position.y - globals.s.FLOOR_HEIGHT && globals.s.BALL_GROUNDED == true) {
//						Debug.Log ("[CAM] WOWWWWW!!!! BALL IS TOO LOW!! " + (globals.s.BALL_Y - transform.position.y));
//						if(globals.s.BALL_Y < transform.position.y - globals.s.FLOOR_HEIGHT - 2)
//							rb.velocity = new Vector2(0, - globals.s.CAMERA_SPEED);
//						else
//							rb.velocity = new Vector2(0, 0);
//						moving = false;
//					}
//
//					//make camera move normally
//					else if (globals.s.BALL_Y > transform.position.y - globals.s.FLOOR_HEIGHT / 4 && globals.s.BALL_GROUNDED == true)//Debug.Log("MY Y POS: " + transform.position.y);  if (globals.s.BALL_Y > transform.position.y)
//					{
//						rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
//						moving = true;
//					}
				}
			}
			else
			{
				if(globals.s.GAME_OVER == 1)
					rb.velocity = new Vector2(0, 0);
				else
					rb.velocity = new Vector2(0, -globals.s.CAMERA_SPEED);
				moving = false;
			}
		}
	}

    void Update2() {
        
        //transform.position = new Vector3 (0, 0,0);
        if (pw_super_jump == false && globals.s.REVIVING == false && globals.s.GAME_STARTED == true)
        {
            if (globals.s.GAME_OVER == 0 && !falling && !hitted_on_wall) {
                if (initiated == false)
                {
                    if (globals.s.BALL_Y > transform.position.y) //if ball is in a superior position than the camera
                    {
                        rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
                        initiated = true;
                        moving = true;
                    }
                }
                else
                {
                    // ball is to high
					if (globals.s.GAME_STARTED && globals.s.BALL_FLOOR > 1 && globals.s.BALL_GROUNDED && globals.s.PW_SUPER_JUMP == false &&
						globals.s.BALL_Y > transform.position.y + globals.s.FLOOR_HEIGHT + 1.5f) { 
                       
						OnBallTooHigh();
                    }

                    // stop camera
                    else if (moving && globals.s.BALL_Y < transform.position.y - globals.s.FLOOR_HEIGHT && globals.s.BALL_GROUNDED == true) {
                        if(globals.s.BALL_Y < transform.position.y - globals.s.FLOOR_HEIGHT - 2)
                            rb.velocity = new Vector2(0, - globals.s.CAMERA_SPEED);
                        else
                            rb.velocity = new Vector2(0, 0);
                        moving = false;
                    }

					//camera is moving normally
                    else if (globals.s.BALL_Y > transform.position.y - globals.s.FLOOR_HEIGHT / 4 && globals.s.BALL_GROUNDED == true)//Debug.Log("MY Y POS: " + transform.position.y);  if (globals.s.BALL_Y > transform.position.y)
                    {
                        rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
                        moving = true;
                    }
                }
            }
            else
            {
               if(globals.s.GAME_OVER == 1)
                    rb.velocity = new Vector2(0, 0);
               else
                    rb.velocity = new Vector2(0, -globals.s.CAMERA_SPEED);
                moving = false;
            }
        }
    }
}
