using UnityEngine;
using System.Collections;

public class bt_sound : MonoBehaviour
{
	AudioSource musicPlayer ;
	int n=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActBT()
    {
		
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bt_sound"))
		{

            GetComponent<Animator>().Play("bt_sound_off");
			sound_controller.s.muteSFX();
            sound_controller.s.muteMusic();
            USER.s.SOUND_MUTED = 1;
            PlayerPrefs.SetInt("sound_muted", 1);
        }
		else
		{

            GetComponent<Animator>().Play("bt_sound");
            sound_controller.s.unmuteSFX();
            sound_controller.s.unmuteMusic();
            USER.s.SOUND_MUTED = 0;
            PlayerPrefs.SetInt("sound_muted", 0);
        }


		n++;
		if (n > 10 && PlayerPrefs.GetInt ("NOTAS_SECRETAS", 0) == 0) {
			PlayerPrefs.SetInt ("NOTAS_SECRETAS", 1);
			USER.s.AddNotes (30);
		}

	}

}
