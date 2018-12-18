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

	public Animator[] glowAnimationsObjs;
	Animator[] glowAnimators;
	public GameObject[] sliderHorizontalObjs;

	private GlowBehaviour[] glows;
	
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

		glows = GameObject.FindObjectsOfType(typeof(GlowBehaviour)) as GlowBehaviour[];
//		foreach(GlowBehaviour s in glows){
//			glows.RestartAnimations();
//		}

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
		foreach (Animator anims in glowAnimationsObjs) {
			if (anims.gameObject.activeInHierarchy && anims != null) {
				if (a % 2 == 0) {
					anims.SetTrigger ("Play");
//				Debug.Log ("REST 0000000000");
				} else {
					anims.Play ("normal", 1, 0.7f);
//					anims.SetTrigger ("Play");
//				Debug.Log ("REST NO 0");
				}
			}

			//Debug.Log ("HEY LISTEN LINK, YOUR RAP DOESN'T STINK " + a + " LENGT: " + glowAnimators [a]);
			a++;
		}

		a = 0;
		foreach (GameObject anims in sliderHorizontalObjs) {
			if (anims.activeInHierarchy && anims.GetComponent<Animator> () != null) {

//			if(a % 2 == 0)
				anims.GetComponent<Animator> ().Play ("normal");
				//Debug.Log ("HEY LISTEN LINK, YOUR RAP DOESN'T STINK " + a + " LENGT: " + glowAnimators [a]);
				a++;
			}
		}

	}
	int i=0;	
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
//			foreach (GameObject anims in glowAnimationsObjs) {
			for (i = 0; i < glowAnimationsObjs.Length; i++) {
				if (glowAnimationsObjs [i] != null && glowAnimationsObjs [i].gameObject.activeInHierarchy)
					glowAnimationsObjs [i].SetTrigger ("Play");
				//Debug.Log (" PLAY THE ANIMATION!!");
			}

			// FFFFFFFFF FADE IN GLOWS
//			for (i = 0; i < glows.Length; i++) {
			GlowBehaviour[] glowsz = GlowBehaviour.All.ToArray(); 
			for (i = 0; i < glowsz.Length; i++) {
				if (glowsz [i] != null && glowsz [i].gameObject.activeInHierarchy)
					glowsz [i].FadeIn ();
			}

			// Stages
			for (i = 0; i < stages.Length; i++) {
				if (stages [i] != null && stages [i].gameObject.activeInHierarchy)
					stages [i].RestartAnimations ();
			}

		} else if (current_step == lightStep && my_state != 1) {
			my_state = 1;
//			foreach (RythmScenarioBehaviour s in stages) {
			for (i = 0; i < stages.Length; i++) {
				if (stages [i] != null && stages [i].gameObject.activeInHierarchy)
					stages [i].RestartGlowFadeInAnimation ();
			}
		

		} else if (current_step == lightStep + 6 && my_state != 2) {
			my_state = 2;
			for (i = 0; i < stages.Length; i++) {
				if (stages [i] != null && stages [i].gameObject.activeInHierarchy)
					stages [i].RestartGlowFadeOutAnimation ();
			}
		
		} else if (current_step == lightStep2 && my_state != 3) {
			my_state = 3;
			for (i = 0; i < stages.Length; i++) {
				if (stages [i] != null && stages [i].gameObject.activeInHierarchy)
					stages [i].RestartGlowFadeInAnimation2 ();
			}
		} else if (current_step == lightStep2 + 6 && my_state != 3) {
			my_state = 3;
			for (i = 0; i < stages.Length; i++) {
				if (stages [i] != null && stages [i].gameObject.activeInHierarchy)
					stages [i].RestartGlowFadeOutAnimation2 ();
			}

		}
		// FFFFFFFFF FADE OUT GLOWS
		else if (current_step == step_glow_out) {
			GlowBehaviour[] glowsz = GlowBehaviour.All.ToArray();
			for (i = 0; i < glowsz.Length; i++) {
				if (glowsz [i] != null && glowsz [i].gameObject.activeInHierarchy)
					glowsz [i].FadeOut ();
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
