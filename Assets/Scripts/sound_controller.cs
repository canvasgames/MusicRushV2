using UnityEngine;
using System.Collections;
using DG.Tweening;
using FMODUnity;
//using FMOD;


public class sound_controller : MonoBehaviour
{
	#region === VArs ===
    public static sound_controller s = null;

    public AudioSource efxSource, efxSource2, efxSource3;
	public AudioSource musicSource, musicSource2, musicSource3, musicSource4, musicSource5, curFadeIn, curFadeOut;
    public AudioSource Jumptest;
	public AudioSource Explosion2;

    public AudioClip Jump, Explosion, Collect, Alert;
    public AudioClip[]  Jumps;

    public GameObject bt_sound;
	public MusicLayers[] musics;

    bool can_play_jump = true;
	public MusicStyle curMusic, curJukeboxMusic;

    int music_playing = 1;

	FMOD.Studio.EventInstance curFmodMusic, curFmodEffect;

	public bool soundMuted = false;

	#endregion

	#region ===== Init =======
    void Awake()
    {
        //muteMusic();
        //sController = this;
        if (s == null)
            s = this;
        else if (s != this)
            Destroy(gameObject);
//        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        can_play_jump = true;

	 	SoltaOSomAeDJAndreMarques (globals.s.ACTUAL_STYLE);

		// MUTE LOGIC
		int temp_sound = PlayerPrefs.GetInt("sound_state_0on_1off", 0);

		if(temp_sound == 1)
		{
			muteMusic();
			//            muteSFX();
			if(bt_sound != null)
				bt_sound.GetComponent<Animator>().Play("bt_sound_off");
		}

		if (QA.s.NO_MUSIC == true)
			muteMusic ();

		RythmController.s.OnMusicStarted ();

		Invoke ("PlayIntroMusicRush", 0.15f);

//		FMODUnity.RuntimeManager.PlayOneShot("event:Soundtrack/music_classic");
//		FMODUnity.RuntimeManager.CreateInstance("event:Soundtrack/music_classic").start();

//		curFmodMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Soundtrack/jukebox_music_selection");
//		FMODUnity.RuntimeManager.CreateInstance("event:/Soundtrack/music_classic").start();
//		FMODUnity.RuntimeManager.
//		change_music (globals.s.ACTUAL_STYLE);
//
//		if( globals.s.ACTUAL_CHAR == "rock")  change_music (MusicStyle.Rock);
//		if( globals.s.ACTUAL_CHAR == "eletronic")  change_music (MusicStyle.Eletro);
//		if( globals.s.ACTUAL_CHAR == "pop")  change_music (MusicStyle.Pop);
//		if( globals.s.ACTUAL_CHAR == "popGaga")  change_music (MusicStyle.PopGaga);
//		if( globals.s.ACTUAL_CHAR == "reggae")  change_music (MusicStyle.Reggae);
//		if( globals.s.ACTUAL_CHAR == "rap")  change_music (MusicStyle.Rap);
    }

	void PlayIntroMusicRush(){
		PlaySfxUIIntroMusicRush ();
	}

    void Update()
    {
		if (QA.s.FMOD_ON) {
//			if (music_playing != QA.s.jokeri) {
//				music_playing = QA.s.jokeri;
////				curFmodMusic.setParameterValue ("layer", music_playing);
//				curFmodMusic.setParameterValue ("style", music_playing);
//			}
//
//			if (QA.s.jokeri2 > 0) {
//				SoltaOSomAeDJAndreMarques ((MusicStyle)QA.s.jokeri2);
//				QA.s.jokeri2 = 0;
//			}

		} else {
			if (curFadeIn != null) {
				if (curFadeIn.volume < 1) {
					if (1 == 1 || music_playing > 1) {
						curFadeIn.volume += 0.7f * Time.deltaTime;
						if (curFadeOut != null)
							curFadeOut.volume -= 0.7f * Time.deltaTime;
					}/* else {
					curFadeIn.volume += 0.25f * Time.deltaTime;
					curFadeOut.volume -= 0.25f * Time.deltaTime;
				}*/
				} else { 
					curFadeIn.volume = 1;
					if (curFadeOut != null)
						curFadeOut.Stop ();
//					curFadeOut.volume = 0;
					curFadeIn = null;
					curFadeOut = null;
				}
			}
		}
    }

	#endregion

	#region ===== Music Change =====
	public void SoltaOSomAeDJAndreMarques(MusicStyle style){
		jukeboxMusicIsPlaying = false;
		if (curFmodMusic.isValid()) {
			curFmodMusic.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			curFmodMusic.release ();
		} else {
//			curFmodMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Soundtrack/music_classic");
//			curFmodMusic.start ();
		}
//		curFmodMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Soundtrack/music_"+style.ToString());
		curFmodMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Soundtrack/"+GD.s.GetMusicNameForFMOD(style));
		curFmodMusic.setParameterValue ("layer", 1);
		curFmodMusic.start ();
		//curFmodMusic = 
	}
	public void StopAndReleaseCurMusic(){
		curFmodMusic.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		curFmodMusic.release ();
	}

	void GameOverAbafarOsom(){
//		musica.
	}

	bool jukeboxMusicIsPlaying = false;

	public void change_music(MusicStyle style){ // PARAR DE INSTANCIAR 
		//		return;
		Debug.Log("chaaaaaanging music!");

		if (QA.s.FMOD_ON == false) {
			Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXHII!");
			GameObject instance = Instantiate (Resources.Load ("Prefabs/Musics/" + style.ToString (),
				typeof(GameObject)), Vector3.zero, transform.rotation) as GameObject;
			instance.transform.parent = this.transform;

			MusicLayers music = instance.GetComponent<MusicLayers> ();

			if (music.myStyle == style) {
				curMusic = style;

				musicSource.Stop ();
				musicSource2.Stop ();
				musicSource3.Stop ();
				musicSource4.Stop ();
				musicSource5.Stop ();

				musicSource = music.layer1;
				musicSource2 = music.layer2;
				musicSource3 = music.layer3;
				musicSource4 = music.layer4;
				musicSource5 = music.layer5;
				//			music.layer1.time = 5f;

				play_music ();
			}
		} else {
			SoltaOSomAeDJAndreMarques (style);
		}
	}

	public void ChangeMusicForStore(MusicStyle style){
		//		return;
		if (QA.s.FMOD_ON == false) {
			GameObject instance = Instantiate (Resources.Load ("Prefabs/Musics/" + style.ToString () + "Layer1",
				                      typeof(GameObject)), Vector3.zero, transform.rotation) as GameObject;
			instance.transform.parent = this.transform;

			MusicLayers music = instance.GetComponent<MusicLayers> ();

			if (music.myStyle == style) {
				curMusic = style;

				musicSource.Stop ();
				musicSource2.Stop ();
				musicSource3.Stop ();
				musicSource4.Stop ();
				musicSource5.Stop ();

				musicSource = music.layer1;
//			musicSource2 = music.layer2;
//			musicSource3 = music.layer3;
//			musicSource4 = music.layer4;
//			musicSource5 = music.layer5;
				//			music.layer1.time = 5f;

				play_music ();
			}
		} 
		// fmod logic
		else {

			if (jukeboxMusicIsPlaying == false) {
				Debug.Log ("[iiiiii init jukebox audio");

				curFmodMusic.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				curFmodMusic.release ();
				curFmodMusic = FMODUnity.RuntimeManager.CreateInstance ("event:/Soundtrack/jukebox_music_selection");
				curFmodMusic.start ();
				jukeboxMusicIsPlaying = true;
				curFmodMusic.setParameterValue ("style", (int)style + 1);
			}

			if (curJukeboxMusic != style) {
//				Debug.Log (" FOR REAL");
				curJukeboxMusic = style;
				curFmodMusic.setParameterValue ("style", (int)style + 1);
			}
		}
	}


	#endregion

	#region ====== Music Update ======
	public void RestartLogicForMusic() {
		if (QA.s.FMOD_ON == true) {
			if (music_playing > 1) {
				music_playing = 1;
				curFmodMusic.setParameterValue ("layer", 1);
			}
			curFmodMusic.setVolume (1f);

		} else {
			if (music_playing > 1) {
				curFadeIn = musicSource;
				curFadeIn.volume = 0;
				//TBD CUIDAR A LOJA
				Debug.Log ("SOUND CONTROLLER RESTART MUSIC2!!!!! MUSIC PLAYING: " + music_playing);
				if (music_playing == 2) {
					curFadeOut = musicSource2;
				} else if (music_playing == 3) {
					curFadeOut = musicSource3;
				} else if (music_playing == 4) {
					curFadeOut = musicSource4;
				} else if (music_playing == 5) {
					curFadeOut = musicSource5;
				}
				curFadeIn.Play ();
				curFadeIn.time = curFadeOut.time;
				music_playing = 1;
			}
		}
	}

	public void PauseAtGameplayEvent(){
		if(soundMuted== false)
		curFmodMusic.setVolume (0.2f);
	}

	public void ResumeFromPause(){
		if(soundMuted == false)
			curFmodMusic.setVolume (1f);
	}

	public void SpinDiskStart(){
		if (QA.s.FMOD_ON == true) {
			curFmodMusic.setParameterValue ("spin", 1);
		}
	}

	public void SpinDiskEnd(){
		if (QA.s.FMOD_ON == true) {
			curFmodMusic.setParameterValue ("spin", 0);
		}
	}

	#endregion

	#region ====== SFX ======
	public void play_alert() {
		//        PlaySingle(Alert);
	}

	public void PlaySfxReward(){
		if(soundMuted == false)
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_ui_new_highscore");
	}

	public void special_event() {
		//PlaySingle(Collect);
//		PlaySingle(Jumps[Random.Range(0, 7)], 1.5f);

	}

	public void PlaySfxQuemQuerDinheiroooo() { // coin collected
		if(soundMuted == false)
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_collect_coin");
	}

	public void PlaySfxCollectPw() {
//		PlaySingle(Collect);
		//PlaySingle(Jumps[Random.Range(0, 7)]);
		if(soundMuted == false)
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_collect_power_up");
	}

	public void PlayJump()
	{
		if (soundMuted == false) {
			if (QA.s.FMOD_ON)
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_jump");
			else {
				if (efxSource.volume > 0 && can_play_jump == true)
				{
					can_play_jump = false;
					//			Debug.Log ("PLAY JUMP");
					PlaySingle(Jump);
					//PlaySingle(Jumps[Random.Range(0,7)]);
					Invoke("can_play_jump_again", 0.3f);
				}
			}
		}
	}

	void can_play_jump_again() 	{
		can_play_jump = true;
	}

	public void PlaySfxCharacterExplosion()	{
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_explosion");
		}
	}

	public void PlaySfxShieldBreak()	{
		if (soundMuted == false) {
			//FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_shield_break");
		}
	}

	public void PlaySfxCharacterSuperJumpEffect()	{
		if (soundMuted == false) {
			//FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_super_jump");
		}
	}

	public void PlaySfxCharacterVisionEndingAlert()	{ 
		if (soundMuted == false) {
//			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_vision_ending_alert");
		}
	}

	public void PlaySfxTrapHiddenHoleFall()	{ 
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_trap_hidden_hole_fall");
		}
	}

	public void PlaySfxTrapHiddenSawAppear() {
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/Traps/sfx_trap_hidden_saw_appear");
		}
	}

	public void PlaySfxTrapHiddenSpikeAppear()	{ 
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/Traps/sfx_trap_hidden_spike_appear");
		}
	}

	public void PlaySfxTrapHiddenWallAppear()	{
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/Traps/sfx_trap_hidden_wall_appear");
		}
	}

	public void PlaySfxCharacterWallCollided()	{ 
		if (soundMuted == false) {
			//FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_character_wall_collided");
		}
	}

	public void PlayUIButtonPressed()	{ // tbd
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/UI/sfx_ui_button_pressed");
		}
	}

	public void PlaySfxUIIntroMusicRush()	{
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/sfx_ui_intro_music_rush");
		}
	}

	public void PlaySfxUIJukeboxCoinFalling()	{ 
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/UI/sfx_ui_jukebox_coin_falling");
		}
	}

	bool restoreMusicVolume = false;
	public void PlaySfxUIJukeboxSortingCharacter()	{ 
		if (soundMuted == false) {
			curFmodEffect = FMODUnity.RuntimeManager.CreateInstance ("event:/Sfx/UI/sfx_ui_jukebox_sorting_character");
			curFmodEffect.start ();
			curFmodMusic.setVolume (0f);
			restoreMusicVolume = true;
//			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/UI/sfx_ui_jukebox_sorting_character");
		}
	}

	public void PlaySfxUINewHighscore()	{
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/UI/sfx_ui_new_highscore");
		}
	}

	public void PlaySfxUIReviveCountdown()	{ // ?tbd?
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/UI/sfx_ui_revive_countdown");
		}
	}

	public void PlaySfxUIGameOverScoreRaisingWhichIsBasicallyTheSoundOfALoser()	{ // tbd
		if (soundMuted == false) {
			curFmodEffect = FMODUnity.RuntimeManager.CreateInstance ("event:/Sfx/UI/sfx_ui_game_over_score_raising");
			curFmodEffect.start ();
		}
	}

	public void StopSfxEffect(FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.IMMEDIATE){
		curFmodEffect.stop (stopMode);
		curFmodEffect.release ();
		if(restoreMusicVolume == true){
			restoreMusicVolume = false;
			curFmodMusic.setVolume (1f);
		}
	}

	public void PlaySfxUISpinDiskReward()	{ // tbd
		if (soundMuted == false) {
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Sfx/UI/sfx_ui_spin_disk_reward");
		}
	}

	#endregion
  
	#region ==== Technical Stuff =====
    //#####################################################
    void PlaySingle(AudioClip clip, float volume = 3f)
	{
        if (efxSource.isPlaying == false)
        {
//			Debug.Log ("WOW1 " + clip.name);
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;
			efxSource.volume = volume;
            //Play the clip.
            efxSource.PlayOneShot(clip);

        }
        else if (efxSource2.isPlaying == false)
        {
//			Debug.Log ("WOW2");

            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource2.clip = clip;
			efxSource.volume = volume;
            //Play the clip.
            efxSource2.PlayOneShot(clip);
        }
        else 
        {
//			Debug.Log ("WOW3");

            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource3.clip = clip;
			efxSource.volume = volume;
            //Play the clip.
            efxSource3.PlayOneShot(clip);
        }
    }

    public void muteSFX()
    {
        PlayerPrefs.SetInt("sound_state_0on_1off", 1);
        efxSource.volume = 0;
        efxSource2.volume = 0;
        efxSource3.volume = 0;
    }

    public void unmuteSFX()
    {
        PlayerPrefs.SetInt("sound_state_0on_1off", 0);
        efxSource.volume = 1;
        efxSource2.volume = 1;
        efxSource3.volume = 1;
    }

    public void muteMusic()
    {
		if(QA.s.FMOD_ON)
			curFmodMusic.setVolume (0f);

		else
			musicSource.volume = 0;
    }

    public void unmuteMusic()
    {
		if(QA.s.FMOD_ON)
			curFmodMusic.setVolume (1f);
		else
			musicSource.volume = 1;
    }

	#endregion

	#region == zOld == 
	public void play_music()
	{	
		Debug.Log ("[SOUND CTRL] PLAY MUSIC");
		if (QA.s.FMOD_ON == false) {
			musicSource.Play ();
			//		musicSource2.Play();
			//		musicSource3.Play();
			//		musicSource4.Play();
			//        musicSource5.Play();
			musicSource.volume = 1;
		} else {
			//			SoltaOSomAeDJAndreMarques (globals.s.ACTUAL_STYLE);
		}
		music_playing = 1;

		RythmController.s.OnMusicStarted ();
	}

	public void stop_music()
	{
		musicSource.Stop();
	}


	public void update_music() {
		if (USER.s.SOUND_MUTED == 0) {
			if (QA.s.FMOD_ON == false)
				update_music2 ();
			else {
				music_playing++;
				curFmodMusic.setParameterValue ("layer", music_playing);
			}
		}
		/* music_playing++;
        if (music_playing == 2) {
            //musicSource.Stop();
            //musicSource2.volume = 1;
            curFadeIn = musicSource2;
            curFadeOut = musicSource;
        }
        else if (music_playing == 3) {
            musicSource2.Stop();
            musicSource3.volume = 1;
        }

        else if (music_playing == 4) {
            musicSource3.Stop();
            musicSource4.volume = 1;
        }
        else if (music_playing == 5) {
            musicSource4.Stop();
            musicSource5.volume = 1;
        }
        */
	}

	public void update_music2() {
		Debug.Log ("UPDATE MUSIC2!!!!! MUSIC PLAYING: " + music_playing);
		music_playing++;
		if (music_playing == 2) {
			//musicSource.Stop();
			//musicSource2.volume = 1;
			curFadeIn = musicSource2;
			curFadeOut = musicSource;

			curFadeIn.Play();
			curFadeIn.time = curFadeOut.time;
		}
		else if (music_playing == 3) {
			curFadeIn = musicSource3;
			curFadeOut = musicSource2;
			curFadeIn.Play();
			curFadeIn.time = curFadeOut.time;
		}

		else if (music_playing == 4) {
			if (curMusic != MusicStyle.Rock) {
				curFadeIn = musicSource4;
				curFadeOut = musicSource3;
				curFadeIn.Play();
				curFadeIn.time = curFadeOut.time;
			} else {
				curFadeIn = musicSource4;
				curFadeIn.Play();
			}
		}
		else if (music_playing == 5) {

			if (curMusic != MusicStyle.Rock) {
				curFadeIn = musicSource5;
				curFadeOut = musicSource4;
				curFadeIn.Play();
				curFadeIn.time = curFadeOut.time;
			} else {
				curFadeIn = musicSource5;
				curFadeOut = musicSource3;
				curFadeIn.Play();
				curFadeIn.time = curFadeOut.time;
			}
		}
	}




	public void change_music2(MusicStyle style){
		foreach (MusicLayers mus in musics) {
			if (mus.myStyle == style) {
				curMusic = style;

				musicSource.Stop ();
				musicSource2.Stop ();
				musicSource3.Stop ();
				musicSource4.Stop ();
				musicSource5.Stop ();

				musicSource = mus.layer1;
				musicSource2 = mus.layer2;
				musicSource3 = mus.layer3;
				musicSource4 = mus.layer4;
				musicSource5 = mus.layer5;

				play_music ();
			}
		}
	}

	#endregion
}
