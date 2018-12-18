using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class BtJukebox : MonoBehaviour {

	Button myButton;
	public bool particlesAnimationIsOn;
	Image myImage;
	[SerializeField] GameObject[] myParticlesPool;
	[SerializeField] ParticleSystem myParticles;
	[SerializeField]  GameObject myParticlesGroup;
	[SerializeField] GameObject myReadyEffect;
	[SerializeField] GameObject myTextImage;
	// Use this for initialization
	void Awake () {
		myButton = GetComponent<Button> ();
		myImage = GetComponent<Image> ();
//		SetNewStyleState ();
//		SetChangeStyleState ();
	}

	public void ResetMyParticles(){
		if (myParticlesGroup.activeInHierarchy) {
			foreach (GameObject p in myParticlesPool) {
				p.transform.localPosition = new Vector3 (0, 0, 0);
			}
		}
	}

	public void BeginMyParticlesAnimation(){
		particlesAnimationIsOn = true;
		myParticlesGroup.SetActive(true);

		StartCoroutine (ParticlesAnimation ());
	}

	IEnumerator ParticlesAnimation(){
		float xLeft = -170, xRight = 170, xCur = 0, yCenter = 0;
		int i = 0;
		int xInc = 85;
		xCur = xLeft;
		while (particlesAnimationIsOn) {
			yield return new WaitForSeconds (0.15f);

			GameObject curParticle = myParticlesPool [i]; // pega a particula
			curParticle.SetActive (true);
			curParticle.transform.DOKill ();
			int rand = xInc + Random.Range (-5, 5) * 10;
			xCur = xCur + rand; // randomiza posição X
			if (xCur > xRight)
				xCur = xCur - xRight*2;
			curParticle.transform.localPosition = new Vector2 (xCur, yCenter);  //seta a posição

			float randS = Random.Range (0.7f, 1f);
			curParticle.transform.localScale = new Vector3 (randS, randS, 1f); // randomiza a scale

			int yTargetPos = Random.Range (100, 200); // Subir pra cima
			curParticle.transform.DOLocalMoveY (curParticle.transform.localPosition.y + yTargetPos, 3f);
//			curParticle.GetComponent<Image> ().DOFade (0, 2f);
//				.OnComplete (() => DeactivateThisParticle(k));

			i++;
//			if (i == myParticlesPool.Length)
			if (i == 13)
				i = 0;
		}
	}

	void DeactivateThisParticle(int a){
		myParticlesPool [a].SetActive (false);
	}


	public void SetChangeStyleState(){
//		if(myParticles) myParticles.gameObject.SetActive (false);
		if(myParticlesGroup) myParticlesGroup.SetActive (false);
		myTextImage.SetActive (false);

		Sprite img = Resources.Load<Sprite> ("Sprites/"+TransMaster.s.actualLanguage.ToString() + "/GameOver/change-style");
		Debug.Log ("IMG LOADED?  " + img + " AAA " + TransMaster.s.actualLanguage.ToString() );
		if (img)
			myImage.sprite = img;
		else
			Debug.Log ("EEEEEEERROR");
//		Sprite sprt = Resources.Load<Sprite> ("Sprites/GameOver/change-style-press");
//		SpriteState state = new SpriteState ();
//		state.pressedSprite = sprt;
//		myButton.spriteState = state;

		myParticlesGroup.SetActive (false);

		if (myReadyEffect)
			myReadyEffect.SetActive (false);

	}

	public void SetNewStyleState(bool glowAnimation = false){
		if (glowAnimation = false) {
			Sprite img = Resources.Load<Sprite> ("Sprites/GameOver/new-style");
			myImage.sprite = img;

			Sprite sprt = Resources.Load<Sprite> ("Sprites/"+TransMaster.s.actualLanguage.ToString() + "/GameOver/new-style-press");
			SpriteState state = new SpriteState ();
			state.pressedSprite = sprt;
			myButton.spriteState = state;

			//activate particles
//			myParticles.gameObject.SetActive (true);
			myTextImage.SetActive (true);
		}
		else
			StartCoroutine (ReadyAnimation());
	}

	IEnumerator ReadyAnimation(){

		if (QA.s.TRACE_PROFUNDITY > 0) Debug.Log ("BT JUKEBOX MY READY ANIMATION!!!");
		if (myReadyEffect) {
			myReadyEffect.SetActive (true);
			myReadyEffect.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			myReadyEffect.GetComponent<Animator> ().Play ("GlowBlueReadyAnim");
		}

		yield return new WaitForSeconds (0.57f);

		if(myReadyEffect) myReadyEffect.GetComponent<Image> ().DOFade (0, 0.2f);

		//change bt image
		Sprite img = Resources.Load<Sprite> ("Sprites/GameOver/new-style");
		myImage.sprite = img;

//		Sprite sprt = Resources.Load<Sprite> ("Sprites/"+TransMaster.s.actualLanguage.ToString() + "/GameOver/new-style-press");
//		SpriteState state = new SpriteState ();
//		state.pressedSprite = sprt;
//		myButton.spriteState = state;

		//activate particles
//		myParticles.gameObject.SetActive (true);
		myTextImage.SetActive (true);

		BeginMyParticlesAnimation();

	}
}
