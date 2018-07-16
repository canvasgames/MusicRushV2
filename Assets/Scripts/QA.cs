using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class QA : MonoBehaviour {

	public string Phrase;

    public static QA s;

	public bool DONT_START_THE_GAME = false;
    public bool DELTADNA_ON = false;

    public bool OLD_PLAYER;

	public bool ALL_UNLOCKED;
	bool sqMode = true;
    public bool INVENCIBLE ;
	public float TIMESCALE = 1;
	public bool ALWAYS_REVIVE = false;
    public float TRACE_PROFUNDITY = 1;

    // 1 = Just Main info
    // 2 = All floor excential creation information
    // 3 = More detailed info of creation floor process and physics
    public bool NO_PWS = false;
    public bool SHOW_WAVE_TYPE = false;
	public bool CREATE_NOTE_TRAIL = true;
	public bool NO_MUSIC = false;

	public float jokerf = 0, jokerf2, jokerf3;
	public int jokeri = 0, jokeri2, jokeri3;

	public Ease ease1;


	public bool FMOD_ON;

	public GameObject SqBt, invButton;
	// Use this for initialization
	void Awake() {
		if (sqMode == false) {
			SqBt.GetComponent<Image> ().color = Color.white;
		} else {
			SqBt.GetComponent<Image> ().color = Color.green;
		}

		if (INVENCIBLE == false) {
			if(invButton != null) invButton.GetComponent<Image> ().color = Color.white;
		} else {
			if(invButton != null) invButton.GetComponent<Image> ().color = Color.green;
		}

        s = this;
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != TIMESCALE && globals.s.GAME_PAUSED == false) Time.timeScale = TIMESCALE;
    }

	public void SwitchSquaresMode(){
		if (sqMode == true) {
			sqMode = false;
			SqBt.GetComponent<Image> ().color = Color.white;
		} else {
			sqMode = true;
			SqBt.GetComponent<Image> ().color = Color.green;
		}
	}

	public void SwitchInvencible(){
		if (INVENCIBLE == true) {
			INVENCIBLE = false;
			invButton.GetComponent<Image> ().color = Color.white;
		} else {
			INVENCIBLE = true;
			invButton.GetComponent<Image> ().color = Color.green;
		}
	}
}
