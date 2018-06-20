using UnityEngine;
using System.Collections;

public class NewHighscoreLights : MonoBehaviour {
	[SerializeField] GameObject[] myLights;
	// Use this for initialization
	void OnEnable () {
		StartCoroutine (LightsEnteringAnimation ());
	}

	IEnumerator LightsEnteringAnimation(){
		int curLine = 0;
		int max = 0;
		for (int i = 0; i < myLights.Length; i++) {
			myLights [i].SetActive (false);

		}

		for (int i = 0; i < myLights.Length ;i++) {
//				myLights [i].SetActive (true);
			myLights [i].SetActive (true);
			//				lightsTopLine [i].SetActive (true);
			//				lightsBottomtLine [i].SetActive (true);
			yield return new WaitForSeconds (0.02f);

			//				lightsTopLine [i].SetActive (false);
			//				lightsBottomtLine [i].SetActive (false);

		}
		StartCoroutine (LightAnimations ());
	}


	IEnumerator LightAnimations(){
		int curLine = 0;
		int max = 0;
		for (int i = 0; ;i++) {
			if (1==1 || globals.s.curGameScreen == GameScreen.Store) {
				myLights [i].SetActive (false);
				//				lightsTopLine [i].SetActive (true);
				//				lightsBottomtLine [i].SetActive (true);
				yield return new WaitForSeconds (0.05f);

				myLights [i].SetActive (true);
				//				lightsTopLine [i].SetActive (false);
				//				lightsBottomtLine [i].SetActive (false);


				if (i == myLights.Length-1) {
					i = -1;
					yield return new WaitForSeconds (0.23f);
				}

			} else
				break;

			max++;
			if (max > 10000)
				break;
		}
	}
}
