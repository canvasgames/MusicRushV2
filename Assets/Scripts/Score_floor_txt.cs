using UnityEngine;
using System.Collections;

public class Score_floor_txt : MonoBehaviour {

    //1 - Best  ------- 2 - Day ------- 3 - Last
    public int my_type;
    public int my_floor;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void try_destroy_me(int floor, int type, bool destroy_now = false)
    {
        if(floor == my_floor && type == my_type || destroy_now == true)
        {
            //Destroy(transform.gameObject);
            transform.position = new Vector3 (5000, 50, 0);
        }
    }

    public void destroy_same_floor(int floor)
    {
        if (floor == my_floor)
        {
            //Destroy(transform.gameObject);
            transform.position = new Vector3(50, 50, 0);
        }
    }
}
