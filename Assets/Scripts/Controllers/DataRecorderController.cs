using UnityEngine;
using System.Collections;

public class DataRecorderController : MonoBehaviour {
	public int userSessionGames = 0;
	public int userSessionHighscore = 0;
	public int userSessionHighse = 0;
	public int notesCollectedThisSession = 0;


	public static DataRecorderController s;
	// Use this for initialization
	void Awake() {
		s = this;
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
