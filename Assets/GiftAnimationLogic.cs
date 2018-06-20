using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GiftAnimationLogic : MonoBehaviour {

	public GameObject title, header, gift_structure;
	float header_pos, gift_structure_pos;


	public void Init (){
		gift_structure.transform.localPosition = new Vector2(gift_structure.transform.localPosition.x , gift_structure_pos);

		header.transform.localPosition = new Vector2 (header.transform.localPosition.x, header.transform.localPosition.y +
			header.transform.GetComponent<RectTransform>().rect.height);
	}

	IEnumerator TitleAnimation(){
		yield return new WaitForSeconds (0.15f);

		gift_structure.transform.DOLocalMoveY (gift_structure_pos - 500
			, 0.3f).SetEase (Ease.OutSine);

		yield return new WaitForSeconds (0.15f);

		header.transform.DOLocalMoveY (header_pos
			, 0.7f).SetEase (Ease.InOutSine);


	}

	public void EnterTitle(MusicStyle style){
		title.GetComponent<Animator> ().Play (style.ToString ());

		StartCoroutine (TitleAnimation ());

		//header.transform.DOLocalMoveY (header.transform.localPosition.y + 700


		
		//header.transform.position.
	}
	void ExitGift(){
		
	}


	// Use this for initialization
	void Awake () {
		header_pos = header.transform.localPosition.y;
		gift_structure_pos = gift_structure.transform.localPosition.y;

//		Init ();
//		EnterTitle (MusicStyle.Rock);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
