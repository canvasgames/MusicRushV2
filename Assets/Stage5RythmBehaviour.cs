using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Stage5RythmBehaviour : RythmScenarioBehaviour {
	//GlowState my_state = GlowState.FadeOut;
	int my_state = 0;

	public GameObject[] RythmElements;
	public GameObject[] myGlowElements;
	public GameObject[] myGlowElements2;

	public int lightStep = 4, lightStep2 = 5;

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

	public override void RestartGlowFadeInAnimation(){
		foreach (GameObject light in myGlowElements) {
			light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.25f);
			light.GetComponent<SpriteRenderer> ().DOFade (1f, GD.s.GlowInTime);
		}
	}

	public override void RestartGlowFadeOutAnimation(){
		foreach (GameObject light in myGlowElements) {
			light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
			light.GetComponent<SpriteRenderer> ().DOFade (0.25f, GD.s.GlowOutTime);
		}
	}

	public override void RestartGlowFadeInAnimation2(){
		//Debug.Log ("fade in start for glow 2 !!! ");
		foreach (GameObject light in myGlowElements2) {
			light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.25f);
			light.GetComponent<SpriteRenderer> ().DOFade (1f, GD.s.GlowInTime);
		}
	}

	public override void RestartGlowFadeOutAnimation2(){
		foreach (GameObject light in myGlowElements2) {
			light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
			light.GetComponent<SpriteRenderer> ().DOFade (0.25f, GD.s.GlowOutTime);
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
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.14f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.9f, 0.05f);
			}
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);

		} else if (RythmController.s.current_step == lightStep2 && my_state != 2) {
			my_state = 2;

			foreach (GameObject light in myGlowElements2) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.14f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.9f, 0.05f);
			}
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
		} 

		else if (RythmController.s.current_step == lightStep + 6 && my_state != 3) {
			foreach (GameObject light in myGlowElements) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.9f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.14f, GD.s.GlowOutTime);
			}

			my_state = 3;
			//Debug.Log("(( FLOOR GLOW OUT " + Time.time);
		}

		else if (RythmController.s.current_step == lightStep2 + 6 && my_state != 4) {
			my_state = 4;
				foreach (GameObject light in myGlowElements2) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.9f);
					light.GetComponent<SpriteRenderer> ().DOFade (0.15f, GD.s.GlowOutTime);
				}
		}
		//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
	}

}
