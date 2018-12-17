using UnityEngine;
using System.Collections;

public class hole_skin_behaviour : scenario_objects {

	public GameObject myGlowOn, myGlowOff;

	public override void TurnTheLightsOnForThePartyHard(){
		myGlowOn.SetActive (true);
//		myGlowOff.SetActive (false);
	}
}
