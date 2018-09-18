using UnityEngine;
using System.Collections;
using DG.Tweening;

public class floor_note : MonoBehaviour {
    public bool already_appeared = false;
	public SpriteRenderer mySR ;
	public Animation myAnimation;
	public SimpleAnimation mySimpleAnimation;
	public float startTime = 0;
	// Use this for initialization
	void Awake () {
		mySR = GetComponent<SpriteRenderer>();
//		myAnimation = GetComponent<Animation>();
//		mySimpleAnimation = GetComponent<SimpleAnimation>();
	}
	
	// Update is called once per frame
//	void Update () {
//
//        if(!already_appeared && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f && 
//            ((globals.s.CUR_BALL_SPEED > 0 && globals.s.BALL_X > transform.position.x) ||
//            ((globals.s.CUR_BALL_SPEED < 0 && globals.s.BALL_X < transform.position.x)))
//            ) {
//            already_appeared = true;
//          
//            mySR.enabled = true;
////            mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0);
////            mySR.DOFade(1, 0.3f);
////			GetComponent<Animator>().Play("NoteFadeInAnimation");
//			GetComponent<Animator> ().enabled = true;
//			GetComponent<Animator> ().SetTrigger ("FadeIn");
//            //Debug.Log("LET ME APPEAR! MY: " + transform.position.x + " BALL X: " + globals.s.BALL_X + "ball speed: " + globals.s.CUR_BALL_SPEED);
//        }
//	
//	}
}
