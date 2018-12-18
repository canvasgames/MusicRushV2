using UnityEngine;
using System.Collections;

//enum Language {
//	Portuguese,
//	English
//}

public class TransMaster : MonoBehaviour {
	public static TransMaster s;
	public Language actualLanguage = Language.eng;
	// Use this for initialization
	void Awake () {
		s = this;

//		if (PlayerPrefs.GetInt("language") == 0)
//		{
//			if (Application.internetReachability == NetworkReachability.NotReachable)
//				SetActualLanguage(Language.usa);
//			else
//			{
//				if (Application.systemLanguage == SystemLanguage.Portuguese)
//					SetActualLanguage(Language.ptbr);
//				else if (Application.systemLanguage == SystemLanguage.Russian)
//					SetActualLanguage(Language.ru);
//				else if (Application.systemLanguage == SystemLanguage.Chinese || Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.ChineseTraditional)
//					SetActualLanguage(Language.cn);
//				else if (Application.systemLanguage == SystemLanguage.Arabic)
//					SetActualLanguage(Language.ar);
//				else if (Application.systemLanguage == SystemLanguage.Japanese)
//					SetActualLanguage(Language.jp);
//				else if (Application.systemLanguage == SystemLanguage.Spanish)
//					SetActualLanguage(Language.es);
//				else if (Application.systemLanguage == SystemLanguage.Korean)
//					SetActualLanguage(Language.sk);
//				else
//					SetActualLanguage(Language.usa);
//			}
//		}
//		else
//		{
//			int lang = PlayerPrefs.GetInt("language");
//			SetActualLanguage((Language)lang);
//		}

	}


//	public string GetGameOverStoreButtonSpritePath(){
//		if (actualLanguage = Language.usa) {
//			return 
//		}
//	}

	public void ChangeLanguage()
	{
		if ((int)actualLanguage == 8)
			actualLanguage = Language.eng;
		else
			actualLanguage++;
		SetActualLanguage(actualLanguage);
	}

	public void SetActualLanguage(Language lang)
	{
		actualLanguage = lang;
		PlayerPrefs.SetInt("language", (int)lang);
//		languageStrings.Clear();
		if (actualLanguage == Language.jp || actualLanguage == Language.sk || actualLanguage == Language.ru || actualLanguage == Language.cn)
			LoadTranslationTxts();
//			LoadFont();
		else
		{
			LoadTranslationTxts();
			TranslateActiveTexts();
		}

	}

	void LoadTranslationTxts()
	{
//		if (actualLanguage == Language.ptbr)
//			fgCSVReader.LoadFromFile("ptbr", new fgCSVReader.ReadLineDelegate(ReadLineTest));
//		else if (actualLanguage == Language.ru)
//			fgCSVReader.LoadFromFile("ru", new fgCSVReader.ReadLineDelegate(ReadLineTest));
//		else if (actualLanguage == Language.jp)
//			fgCSVReader.LoadFromFile("jp", new fgCSVReader.ReadLineDelegate(ReadLineTest));
//		else if (actualLanguage == Language.ar)
//			fgCSVReader.LoadFromFile("ar", new fgCSVReader.ReadLineDelegate(ReadLineTest));
//		else if (actualLanguage == Language.es)
//			fgCSVReader.LoadFromFile("es", new fgCSVReader.ReadLineDelegate(ReadLineTest));
//		else if (actualLanguage == Language.sk)
//			fgCSVReader.LoadFromFile("sk", new fgCSVReader.ReadLineDelegate(ReadLineTest));
//		else if (actualLanguage == Language.cn)
//			fgCSVReader.LoadFromFile("cn", new fgCSVReader.ReadLineDelegate(ReadLineTest));
//		else
//			fgCSVReader.LoadFromFile("usa", new fgCSVReader.ReadLineDelegate(ReadLineTest));
	}

	void TranslateActiveTexts()
	{
		//        TranslateMyText[] texts = FindObjectsOfType<TranslateMyText>();
		//
		//        foreach (TranslateMyText script in texts)
		//        {
		//            script.ChangeFont(actualLanguage);
		//            script.TranslateMe();
		//        }
	}
}

public enum Language
{
	eng = 1,
	ptbr = 2,
	cn = 3,
	ar = 4,
	ru = 5,
	es = 6,
	sk = 7,
	jp = 8
}
