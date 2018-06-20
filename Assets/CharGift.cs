using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class CharGift : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitAnimation(MusicStyle style){
		
		if (style == MusicStyle.Eletro)
		{
			GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/UIAnimations/ElectroUIAnimator") as RuntimeAnimatorController;
		}
		else if (style == MusicStyle.Rock)
		{
			GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/UIAnimations/RockUIAnimator") as RuntimeAnimatorController;
		}
		else if (style == MusicStyle.Pop)
		{
			GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/UIAnimations/PopUIAnimator") as RuntimeAnimatorController;
		}
		else if (style == MusicStyle.PopGaga)
		{
			GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/UIAnimations/PopGagaUIAnimator") as RuntimeAnimatorController;
		}
		else if (style == MusicStyle.Reggae)
		{
			GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/UIAnimations/ReggaeUIAnimator") as RuntimeAnimatorController;
		}
		RectTransform rect = GetComponent<RectTransform> ();

		rect.localScale = new Vector2 (0.5f, 0.5f);
		rect.localPosition = new Vector2 (0, 0);

		rect.DOLocalMoveY (rect.transform.localPosition.y + 290, 1f).SetEase(Ease.OutQuad);
		rect.transform.DOScale (new Vector3 (1.6f, 1.6f, 1f), 1f).SetEase(Ease.OutQuad).OnComplete(() => onPosition());
	}

	void onPosition(){
		hud_controller.si.giftAnimationEnded = true;
	}
}
