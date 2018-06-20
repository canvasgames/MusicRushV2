using UnityEngine;
using System.Collections;

public class floor_pw_collider : MonoBehaviour {
    public GameObject daddy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void unactive_sprite_daddy()
    {
        if(daddy!= null)
        {
            transform.GetComponent<Rigidbody2D>().isKinematic = true;
            if (daddy.GetComponent<floor>() != null)
            {
                daddy.GetComponent<floor>().colidded_super_pw();
            }
            else if (daddy.GetComponentInParent<hole_behaviour>() != null)
                {
                    daddy.GetComponentInParent<hole_behaviour>().colidded_super_pw();
                }
        }
    }
}
