using UnityEngine;
using System.Collections;


public class collider_tutorial_right : MonoBehaviour {
    public GameObject my_objs;
    int cur_best;
    // Use this for initialization
    void Start () {
        cur_best = PlayerPrefs.GetInt("best", 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Rigidbody2D>().velocity.x > 0 && cur_best <= 3)
        {
            Debug.Log("Ativaaaa");
            my_objs.SetActive(true);
        }
    }
}
