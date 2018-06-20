using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Stage4RythmBehaviour : RythmScenarioBehaviour {
	//GlowState my_state = GlowState.FadeOut;
	int my_state = 0;

	public GameObject[] RythmElements;
	public GameObject[] myGlowElements;

	public int lightStep = 4;

	public override void RestartMusic(){

		foreach (GameObject r in RythmElements) {
			if (r!= null) r.GetComponent<Animator> ().Play ("normal", 0, 0);
		}

	}

	public override void RestartAnimations(){
		foreach (GameObject r in RythmElements) {
			if (r!= null) r.GetComponent<Animator> ().Play ("normal", 0, 0);
		}
	}

	void asFixedUpdate () {
		if (RythmController.s.current_step == RythmController.s.step_glow_in && my_state != 0) {

			my_state = 0;

			foreach (GameObject r in RythmElements) {
				if (r!= null) r.GetComponent<Animator> ().Play ("normal", 0, 0);
			}

			//Debug.Log("(( FLOOR GLOW IN! " + Time.time);

		} else if (RythmController.s.current_step == lightStep && my_state != 1) {
			my_state = 1;
			foreach (GameObject light in myGlowElements) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.25f);
				light.GetComponent<SpriteRenderer> ().DOFade (1f, GD.s.GlowInTime);
			}
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);

		} else if (RythmController.s.current_step == lightStep + 3 && my_state != 2) {
			my_state = 2;

			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
		} 

		else if (RythmController.s.current_step == lightStep + 6 && my_state != 3) {
			foreach (GameObject light in myGlowElements) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.25f, GD.s.GlowOutTime);
			}
			my_state = 3;
			//Debug.Log("(( FLOOR GLOW OUT " + Time.time);
		}
	}
	// Use this for initialization

}
