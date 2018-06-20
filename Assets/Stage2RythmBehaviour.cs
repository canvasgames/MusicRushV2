using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Stage2RythmBehaviour : RythmScenarioBehaviour {
	//GlowState my_state = GlowState.FadeOut;
	int my_state = 0;

	public GameObject myGlass;
	public GameObject myBg;
	public GameObject myNotes;
	public GameObject[] RythmElements;
	public GameObject myRecording;
	public int lightStep = 4;

	public override void RestartMusic(){
		foreach (GameObject r in RythmElements) {
			if (r!= null) r.GetComponent<Animator> ().Play ("normal", 0, 0);
		}

		if(myBg != null) myBg.GetComponent<Animator> ().Play ("normal", 0, 0);
	}
		
	public override void RestartAnimations(){
		foreach (GameObject r in RythmElements) {
			if (r!= null) r.GetComponent<Animator> ().Play ("normal", 0, 0);
		}
		if(myBg != null) myBg.GetComponent<Animator> ().Play ("normal", 0, 0);
	}

	void asFixedUpdate () {
		if (RythmController.s.current_step == RythmController.s.step_glow_in && my_state != 0) {
			//			foreach (GameObject light in myLights) {
			//				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.25f);
			//				light.GetComponent<SpriteRenderer> ().DOFade (1f, (float)lightStep * RythmController.s.current_step_time);
			//			}
			my_state = 0;
			foreach (GameObject r in RythmElements) {
				r.GetComponent<Animator> ().Play ("normal", 0, 0);
			}
			myBg.GetComponent<Animator> ().Play ("normal", 0, 0);
			//Debug.Log("(( FLOOR GLOW IN! " + Time.time);

		} else if (RythmController.s.current_step == lightStep && my_state != 1) {
			my_state = 1;
			//myRecording.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.10f);
			//myRecording.GetComponent<SpriteRenderer> ().DOFade (1f, 0.005f);
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);

		} else if (RythmController.s.current_step == lightStep + 4 && my_state != 2) {
			my_state = 2;
			//myRecording.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
		} 

		else if (RythmController.s.current_step == lightStep + 8 && my_state != 3) {
			
			//myRecording.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
			//myRecording.GetComponent<SpriteRenderer> ().DOFade (0.10f, GD.s.GlowOutTime);

			my_state = 3;
			//Debug.Log("(( FLOOR GLOW OUT " + Time.time);
		}
	}
	// Use this for initialization

}
