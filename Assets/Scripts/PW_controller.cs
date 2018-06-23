using UnityEngine;
using System.Collections;
using DG.Tweening;
public class PW_controller : MonoBehaviour
{

    public static PW_controller s;

    public ball_hero[] balls;
    int balls_i = 0;

    float PW_symbol_ending_duration = 5f;

    float temp_alpha;
    float previous_time;
    // Use this for initialization
    void Start()
    {
        s = this;

        balls = new ball_hero[2];

        globals.s.PW_INVENCIBLE = false;
        globals.s.PW_SIGHT_BEYOND_SIGHT = false;
        globals.s.PW_SUPER_JUMP = false;
        globals.s.CAN_REVIVE = false;
        globals.s.PW_ENDING = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (globals.s.PW_ENDING == true)
        {
            //change_alpha_signs();
            new_change_balls_symbol_mask_alpha();
        }
    }

    //Called by invoke
    public void pw_ending()
    {
        temp_alpha = 0;
        previous_time = Time.time;
        globals.s.PW_ENDING = true;
        /* PW_symbol_startTime = Time.time;

         PW_symbol_active = true;
         PW_symbol_active_time = 0.6f;
         PW_symbol_active_actual_time = Time.time + PW_symbol_active_time;*/
    }


    #region INVENCIBLE HEART
    public void invencible_start()
    {
        globals.s.PW_INVENCIBLE = true;
        Invoke("invencible_end", GD.s.GD_PW_HEARTH_TIME);
        Invoke("pw_ending", GD.s.GD_PW_HEARTH_TIME - PW_symbol_ending_duration);
    }

    public void invencible_end()
    {
        CancelInvoke("invencible_end");
        CancelInvoke("pw_ending");
        globals.s.PW_INVENCIBLE = false;
        globals.s.PW_ENDING = false;

        //change_balls_symbol_mask_active(true);

    }
    #endregion

    #region SIGHT
    public void PW_sight_start()
    {
        globals.s.PW_SIGHT_BEYOND_SIGHT = true;
        change_color_sight_pw();

        Invoke("sight_end", GD.s.GD_PW_SIGHT_TIME);
        Invoke("pw_ending", GD.s.GD_PW_SIGHT_TIME - PW_symbol_ending_duration);

		game_controller.s.visionMask.SetActive (true);
		game_controller.s.visionMask.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
		game_controller.s.visionMask.GetComponent<SpriteRenderer>().DOFade(0.38f,0.25f);

    }


    public void sight_end()
    {
        CancelInvoke("pw_ending");
        CancelInvoke("sight_end");
        globals.s.PW_ENDING = false;
        globals.s.PW_SIGHT_BEYOND_SIGHT = false;
        back_color_sight_pw();

		game_controller.s.visionMask.SetActive (false);


        //change_balls_symbol_mask_active(true);

    }
    #endregion

    #region SIGHT CHANGE COLOR
    void change_color_sight_pw()
    {
        int i;

        wall[] walls = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
        for (i = 0; i < walls.Length; i++)
        {
            walls[i].show_me_pw_sight();
        }

        spike[] spikes = GameObject.FindObjectsOfType(typeof(spike)) as spike[];
        for (i = 0; i < spikes.Length; i++)
        {
            spikes[i].show_me_pw_sight();
        }


        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
        for (i = 0; i < holes.Length; i++)
        {
            holes[i].show_me_pw_sight();
        }

        saw[] saws = FindObjectsOfType(typeof(saw)) as saw[];
        for (i = 0; i < saws.Length; i++)
        {
            saws[i].show_me_pw_sight();
        }
    }

    void back_color_sight_pw()
    {
        int i;

        wall[] walls = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
        for (i = 0; i < walls.Length; i++)
        {
            walls[i].back_original_color_pw_sight();
        }

        spike[] spikes = GameObject.FindObjectsOfType(typeof(spike)) as spike[];
        for (i = 0; i < spikes.Length; i++)
        {
            spikes[i].back_original_color_pw_sight();
        }


        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
        for (i = 0; i < holes.Length; i++)
        {
            holes[i].back_original_color_pw_sight();
        }
    }
    #endregion

    public void add_ball(ball_hero new_ball)
    {
        balls[balls_i] = new_ball;

        if (balls_i == 0)
        {
            balls_i++;
        }
        else
        {
            balls_i = 0;
        }
    }

    void new_change_balls_symbol_mask_alpha()
    {
        if(previous_time + 0.01f < Time.time)
        {
            previous_time = Time.time;
            temp_alpha = temp_alpha + 0.0025f;
        }
			
    }


}