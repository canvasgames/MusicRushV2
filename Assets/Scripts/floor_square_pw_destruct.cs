using UnityEngine;
using System.Collections;
using DG.Tweening;
public class floor_square_pw_destruct : MonoBehaviour {

    bool can_disappear = false;

    float initial_y, initial_x;
	Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
        initial_y = transform.localPosition.y;
        initial_x = transform.localPosition.x;
		myRigidBody = GetComponent<Rigidbody2D> ();
    }
	
	// Update is called once per frame
	void aFixedUpdate () {
        if (can_disappear == false)
        {
			if (myRigidBody.velocity != new Vector2(0, 0))
            {
                can_disappear = true;
            }
        }
    }

	public void DisappearSoon(){
		Invoke ("DisappearNow", 0.5f);
	}

	public void DisappearNow(){
		if (myRigidBody.velocity != Vector2.zero) {
			can_disappear = true;
			scale_down_to_dessapear ();
		}
		else{
			Invoke ("DisappearNow", 0.4f);
		}

	}

    public void scale_down_to_dessapear()
    {
     //if (can_disappear == true)
        //transform.DOScale(0, 0.3f) ;
    }

    public void reposite_myself_back()
    {
		//transform.localScale = Vector3.one;
        transform.localPosition = new Vector3(initial_x, initial_y, 0);
       // transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

}
