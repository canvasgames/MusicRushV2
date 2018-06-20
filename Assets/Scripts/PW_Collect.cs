using UnityEngine;
using System.Collections;


public class PW_Collect : MonoBehaviour {

    public int pw_type = 2;
    public int my_floor = 5;
    int rand;
    // Use this for initialization
    void Start () {
        if (pw_type == 0)
        {
            pw_type = 2;  
			init_my_icon();
        }
        
        //rand = Random.Range((int)PW_Types.Invencible, (int)PW_Types.Sight + 1);

        /*if (globals.s.PW_INVENCIBLE == false && globals.s.PW_SIGHT_BEYOND_SIGHT == false && globals.s.PW_SUPER_JUMP == false)
        {
            rand = Random.Range((int)PW_Types.Invencible, (int)PW_Types.Sight + 1);
        }
        else
        {
            if(globals.s.PW_INVENCIBLE == false)
            {
                rand = (int)PW_Types.Invencible;
            }
            else if (globals.s.PW_SIGHT_BEYOND_SIGHT == false)
            {
                rand = (int)PW_Types.Sight;
            }
            else if (globals.s.PW_SUPER_JUMP == false)
            {
                rand = (int)PW_Types.Super;
            }
        }
            */
        //rand = (int)PW_Types.Sight;
        // rand = (int)PW_Types.Super;
        //pw_type = rand;
    }
	
  public void init_my_icon() {
        
        if(pw_type == 1) {
			//Debug.Log ("INIT LIFE ANIMATION");
            GetComponent<Animator>().Play("pw_collectable_life");
        }
        else if (pw_type == 2) {
			//Debug.Log ("INIT SUPER ANIMATION");
            GetComponent<Animator>().Play("pw_collectable_super");
        }
       else if (pw_type == 3) {
			//Debug.Log ("INIT sight ANIMATION");
            GetComponent<Animator>().Play("pw_collectable_sight");
        }
    }
	// Update is called once per frame
	void Update () {
        
	}

    public void collect()
    {
        USER.s.FIRST_PW_CREATED = 1;
        PlayerPrefs.SetInt("first_pw_created", 1);

       // Destroy(transform.gameObject);
    }


    public void destroy_by_floor_PW_Super(int floor_plus_n)
    {
//        if(my_floor <= floor_plus_n)
//            Destroy(transform.gameObject);
    }
}
