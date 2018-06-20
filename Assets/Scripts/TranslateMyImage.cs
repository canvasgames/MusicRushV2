using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateMyImage : MonoBehaviour {
    public Text myTexttxt;
    public TextMesh myTextMesh3d;
    public string myTextID;
    Language actualFontLanguage;
    public FontTypes myFontType;
    public bool isTitle;
    // Use this for initialization
    void OnEnable () {
        if(myTextID != "")
        {
            Invoke("TranslateMe", 0.01f);
        }

        if (actualFontLanguage != TranslationController.instance.actualLanguage)
        {
            if (actualFontLanguage == null)
                actualFontLanguage = TranslationController.instance.actualLanguage;
        }
    }
	
    
	// Update is called once per frame
	void Update () {
		
	}

    public void TranslateMe()
    {
        if (myTexttxt != null)
            myTexttxt.text = TranslationController.instance.GetMyStringText(myTextID, "");
        if (myTextMesh3d != null)
            myTextMesh3d.text = TranslationController.instance.GetMyStringText(myTextID, "");
    }

  
}
