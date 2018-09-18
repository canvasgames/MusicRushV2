using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GlowBehaviour : MonoBehaviour {

	GlowState my_state = GlowState.FadeOut;
	public float my_fade_out_value = 0.25f;
	SpriteRenderer mySprite;
	// Use this for initialization
	void Start () {
		//glow_animation_wait ();
		mySprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void aaUpdate () {
		if (RythmController.s.current_step == RythmController.s.step_glow_in && my_state != GlowState.FadeIn) {
			mySprite.color = new Color (1, 1, 1, my_fade_out_value);
			mySprite.DOFade (1f, GD.s.GlowInTime);
			my_state = GlowState.FadeIn;
			//Debug.Log("(( FLOOR GLOW IN! " + Time.time);

		} 
		else if (RythmController.s.current_step == RythmController.s.step_glow_static && my_state != GlowState.Static) {
			my_state = GlowState.Static;

			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);

		}
		else if (RythmController.s.current_step == RythmController.s.step_glow_out && my_state != GlowState.FadeOut) {
			mySprite.color = new Color (1, 1, 1, 1f);
			mySprite.DOFade(my_fade_out_value, GD.s.GlowOutTime);
			my_state = GlowState.FadeOut;
			//Debug.Log("(( FLOOR GLOW OUT " + Time.time);
		}
	}

//	public void glow_animation_start() {
//		GetComponent<SpriteRenderer>().DOFade(1f, GD.s.GlowInTime).OnComplete(glow_animation_wait);
//	}
//	public void glow_animation_wait(){
//		Invoke("glow_animation_end", GD.s.GlowStaticTime);
//	}
//	public void glow_animation_end() {
//		GetComponent<SpriteRenderer>().DOFade(0, GD.s.GlowOutTime).OnComplete(glow_animation_start);
//	}
//
}
