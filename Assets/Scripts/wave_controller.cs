using UnityEngine;
using System.Collections;

public class wave_controller : MonoBehaviour {

    public GameObject spk;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame

    public void create_new_wave(float floor_n, float floor_y)
    {
        float rand = Random.Range(globals.s.LIMIT_LEFT, globals.s.LIMIT_RIGHT);
        Debug.Log("X POS : " + rand);
        create_spike(rand, floor_y );
        Debug.Log("======== CREATING NEW WAVE! FLOOR: "+ floor_n + " | Y : " +floor_y);
        
    }

    public void create_spike(float x, float y)
    {
        GameObject obj = (GameObject)Instantiate(spk, new Vector3(x, y + 0.5f, 0), transform.rotation);
       // GameObject obj = (GameObject)Instantiate(spk, new Vector3(0, 0, 0), transform.rotation);

    }



}
