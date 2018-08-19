using UnityEngine;
using System.Collections;

public enum GlowState{
	FadeIn,
	Static,
	FadeOut,
}

public class RythmController : MonoBehaviour {
	public static RythmController s;
	public int total_steps = 24, current_step = 0;
	public float current_step_time;
	public int step_glow_in = 0, step_glow_static = 3, step_glow_out = 6;
	public float step_time = 0.96f;
	[HideInInspector] public bool music_started = false;
	private bool already_started = false;
	private float next_step_time = 0;
	int my_state = 0, lightStep = 3, lightStep2 = 15;

	public GameObject[] glowAnimationsObjs;
	Animator[] glowAnimators;
	public GameObject[] sliderHorizontalObjs;
	
	private RythmScenarioBehaviour[] stages;

	void Awake(){
		s = this;
	}
	// Use this for initialization
	void Start () {
		 stages = GameObject.FindObjectsOfType(typeof(RythmScenarioBehaviour)) as RythmScenarioBehaviour[];
		foreach(RythmScenarioBehaviour s in stages){
			s.RestartAnimations();
		}

//		glowAnimators = new Animator[glowAnimationsObjs.Length];
//		int i = 0;
//		foreach (GameObject anims in glowAnimationsObjs) {
//			glowAnimators [i] = anims.GetComponent<Animator> ();
//			Debug.Log ("HEY LISTEN LINK, YOUR RAP DOESN'T STINK " + i + " LENGT: " + glowAnimators [i]);
//			i++;
//		}
	}

	public void OnMusicStarted(){
		already_started = false;
		music_started = true;
		current_step = 0;
		next_step_time = 0;
		current_step_time = step_time / total_steps;

		RythmScenarioBehaviour[] stages = GameObject.FindObjectsOfType(typeof(RythmScenarioBehaviour)) as RythmScenarioBehaviour[];
		for (int i = 0; i < stages.Length; i++)
		{
			stages[i].RestartMusic();
		}

//		foreach (Animator anims in glowAnimators) {
//			Debug.Log ("zzzHEY LISTEN LINK, YOUR RAP DOESN'T STINK LENGT: " + anims);
//			anims.Play ("normal");
//
//		}

		int a = 0;
		foreach (GameObject anims in glowAnimationsObjs) {
			if (a % 2 == 0) {
				anims.GetComponent<Animator> ().Play ("normal");
//				Debug.Log ("REST 0000000000");
			} else {
				anims.GetComponent<Animator> ().Play ("normal", 1, 0.7f);
//				Debug.Log ("REST NO 0");
			}

			//Debug.Log ("HEY LISTEN LINK, YOUR RAP DOESN'T STINK " + a + " LENGT: " + glowAnimators [a]);
			a++;
		}

		a = 0;
		foreach (GameObject anims in sliderHorizontalObjs) {
//			if(a % 2 == 0)
			anims.GetComponent<Animator> ().Play("normal");
			//Debug.Log ("HEY LISTEN LINK, YOUR RAP DOESN'T STINK " + a + " LENGT: " + glowAnimators [a]);
			a++;
		}

	}
		
	// Update is called once per frame
	void FixedUpdate () {
		if (music_started == true) {
			if (already_started == false) {
				already_started = true;
				current_step = 0;
				next_step_time = Time.time + current_step_time;
			}

			if (Time.time > next_step_time) {
				next_step_time = next_step_time + current_step_time;
				current_step++;
				if (current_step == total_steps)
					current_step = 0;
			}
		}

		if (current_step == step_glow_in && my_state != 0) {
			my_state = 0;
			foreach (GameObject anims in glowAnimationsObjs) {
				anims.GetComponent<Animator> ().Play ("normal",0,0);
				//Debug.Log (" PLAY THE ANIMATION!!");
			}

			foreach (RythmScenarioBehaviour s in stages) {
				if (s != null)
					s.RestartAnimations ();
			}
		} else if (current_step == lightStep && my_state != 1) {
			my_state = 1;
			foreach (RythmScenarioBehaviour s in stages) {
				if (s != null)
					s.RestartGlowFadeInAnimation ();
			}
		

		} else if (current_step == lightStep + 6 && my_state != 2) {
			my_state = 2;
			foreach (RythmScenarioBehaviour s in stages) {
				if (s != null)
					s.RestartGlowFadeOutAnimation ();
			}
		
		} else if (current_step == lightStep2 && my_state != 3) {
			my_state = 3;
			foreach (RythmScenarioBehaviour s in stages) {
				if (s != null)
					s.RestartGlowFadeInAnimation2 ();
			}


		} else if (current_step == lightStep2 + 6 && my_state != 3) {
			my_state = 3;
			foreach (RythmScenarioBehaviour s in stages) {
				if (s != null)
					s.RestartGlowFadeOutAnimation2 ();
			}
		}


//		Debug.Log("STEP: " + current_step);
//		if( current_step == step_glow_in)
//			Debug.Log("(( STEP GLOW IN! " + step_glow_in + " ./.  Time: " + Time.time);
//		if( current_step == step_glow_static)
//			Debug.Log("__ STEP GLOW STATIC! " + step_glow_static + " ./.  Time: " + Time.time);
//		if( current_step == step_glow_out)
//			Debug.Log("(( STEP GLOW OUT " + step_glow_out + " ./.  Time: " + Time.time);
//		Debug.Log ("time: " + Time.time);
	}



}
