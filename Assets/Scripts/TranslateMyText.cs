//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class TranslateMyText : MonoBehaviour {
//    public Text myTexttxt;
//    public TextMesh myTextMesh3d;
//    public string myTextID;
//    Language actualFontLanguage;
//    public FontTypes myFontType;
//    public bool isTitle;
//    // Use this for initialization
//    void OnEnable () {
//        if(myTextID != "")
//        {
//            Invoke("TranslateMe", 0.01f);
//        }
//
//        if (actualFontLanguage != TranslationController.instance.actualLanguage)
//        {
//            if (actualFontLanguage == null)
//                actualFontLanguage = TranslationController.instance.actualLanguage;
//            ChangeFont(TranslationController.instance.actualLanguage);
//
//        }
//    }
//	
//    
//	// Update is called once per frame
//	void Update () {
//		
//	}
//
//    public void TranslateMe()
//    {
//        if (myTextMesh != null)
//            myTextMesh.text = TranslationController.instance.GetMyStringText(myTextID, "");
//        if (myTexttxt != null)
//            myTexttxt.text = TranslationController.instance.GetMyStringText(myTextID, "");
//        if (myTextMesh3d != null)
//            myTextMesh3d.text = TranslationController.instance.GetMyStringText(myTextID, "");
//    }
//
//    public void ChangeFont(Language language)
//    {
//        if(language == Language.ru || language == Language.cn || language == Language.jp || language == Language.sk)
//        {
//            if(myTextMesh != null)
//            {
//                myTextMesh.font = TranslationController.instance.multiFontTextMesh;
//                if (isTitle)
//                {
//                    myTextMesh.fontSharedMaterial = TranslationController.instance.multiFontTitleMat;
//                }
//            }
//            if (myTexttxt != null)
//                myTexttxt.font = TranslationController.instance.multiFont;
//            if (myTextMesh3d != null)
//            {
//                myTextMesh3d.font = TranslationController.instance.multiFont;
//                myTextMesh3d.GetComponent<MeshRenderer>().material = TranslationController.instance.multiFont.material;
//            }
//        }
//        else if (language == Language.ar)
//        {
//            if (myTextMesh != null)
//            {
//                myTextMesh.font = TranslationController.instance.arabicFontTextMesh;
//                if (isTitle)
//                    myTextMesh.fontSharedMaterial = TranslationController.instance.arabicFontTitleMat;
//            }
//                
//            if (myTexttxt != null)
//                myTexttxt.font = TranslationController.instance.arabicFontText;
//            if (myTextMesh3d != null)
//            {
//                myTextMesh3d.font = TranslationController.instance.arabicFontText;
//                myTextMesh3d.GetComponent<MeshRenderer>().material = TranslationController.instance.arabicFontText.material;
//            }
//        }
//        else
//        {
//            if (myTextMesh != null)
//            {
//                if (myFontType == FontTypes.Toonish)
//                {
//                    myTextMesh.font = TranslationController.instance.ToonishTextMesh;
//                    if (isTitle)
//                        myTextMesh.fontSharedMaterial = TranslationController.instance.toonishFontTitleMat;
//                }
//                else if (myFontType == FontTypes.Dimbo)
//                    myTextMesh.font = TranslationController.instance.DimboTextMesh;
//                else if(myFontType == FontTypes.Liberation)
//                    myTextMesh.font = TranslationController.instance.LiberationTextMesh;
//            }
//            if (myTexttxt != null)
//            {
//                if (myFontType == FontTypes.Toonish)
//                    myTexttxt.font = TranslationController.instance.ToonishText;
//                else if(myFontType == FontTypes.Dimbo)
//                    myTexttxt.font = TranslationController.instance.DimboText;
//            }
//            if (myTextMesh3d != null)
//            {
//                myTextMesh3d.font = TranslationController.instance.ToonishText;
//                myTextMesh3d.GetComponent<MeshRenderer>().material = TranslationController.instance.ToonishText.material;
//            }
//        }
//
//        actualFontLanguage = language;
//    }
//}
