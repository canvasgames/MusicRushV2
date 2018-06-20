using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TranslationController : MonoBehaviour
{
    public Dictionary<string, string> languageStrings;
    public Language actualLanguage;

    [Space(10)]


    public Font ToonishText;
    public Font DimboText;

    [Space(10)]

    public Font arabicFontText;

    public Font multiFont;
    [Space(10)]
    public Material arabicFontTitleMat;
    public Material multiFontTitleMat;
    public Material toonishFontTitleMat;

    public static TranslationController instance;
    [Space(10)]
    public GameObject menuCanvas;
    GameObject menu;
    public bool isIntro = false;
    public WWW www;
    // Use this for initialization
    void Awake()
    {

        instance = this;
        languageStrings = new Dictionary<string, string>();

        if (PlayerPrefs.GetInt("language") == 0)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                SetActualLanguage(Language.eng);
            else
            {
                if (Application.systemLanguage == SystemLanguage.Portuguese)
                    SetActualLanguage(Language.ptbr);
                else if (Application.systemLanguage == SystemLanguage.Russian)
                    SetActualLanguage(Language.ru);
                else if (Application.systemLanguage == SystemLanguage.Chinese || Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.ChineseTraditional)
                    SetActualLanguage(Language.cn);
                else if (Application.systemLanguage == SystemLanguage.Arabic)
                    SetActualLanguage(Language.ar);
                else if (Application.systemLanguage == SystemLanguage.Japanese)
                    SetActualLanguage(Language.jp);
                else if (Application.systemLanguage == SystemLanguage.Spanish)
                    SetActualLanguage(Language.es);
                else if (Application.systemLanguage == SystemLanguage.Korean)
                    SetActualLanguage(Language.sk);
                else
                    SetActualLanguage(Language.eng);
            }
        }
        else
        {
            int lang = PlayerPrefs.GetInt("language");
            SetActualLanguage((Language)lang);
        }
    }

    public void SetActualLanguage(Language lang)
    {
        actualLanguage = lang;
        PlayerPrefs.SetInt("language", (int)lang);
        languageStrings.Clear();
        if (actualLanguage == Language.jp || actualLanguage == Language.sk || actualLanguage == Language.ru || actualLanguage == Language.cn)
            LoadFont();
        else
        {
            LoadTranslationTxts();
            TranslateActiveTexts();
        }

    }

    void LoadFont()
    {
        StartCoroutine(GetFontFromServerOrCache());
    }

    IEnumerator GetFontFromServerOrCache()
    {
        //Already loaded the font
        if(multiFont != null)
        {
            LoadTranslationTxts();
            TranslateActiveTexts();
            yield return null;
        }
        else
        {
            // Wait for the Caching system to be ready
            while (!Caching.ready)
                yield return null;

            string url;
            if (Application.platform == RuntimePlatform.Android)
                url = "http://www.eletronicabeckmann.com.br/index/multilanguagefont";
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                url = "http://www.eletronicabeckmann.com.br/index/multilanguagefontios";
            else
                url = "http://www.eletronicabeckmann.com.br/index/multilanguagefont";

            bool loadMenu = false;
            if (Caching.IsVersionCached(url, 1) == false)
            {
                menu = Instantiate(Resources.Load("Menus/DownloadingFont", typeof(GameObject))) as GameObject;
                menu.transform.SetParent(menuCanvas.transform);
                menu.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                menu.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                if (isIntro)
                    menu.transform.localScale = new Vector3(2f, 2f, 2f);
                else
                    menu.transform.localScale = new Vector3(1f, 1f, 1f);
                loadMenu = true;
            }

            using (www = WWW.LoadFromCacheOrDownload(url, 1))
            {
                yield return www;
                if (www.error != null)
                {
                    ErrorInLoadLanguage();
                    yield return 0;
                }
                AssetBundle bundle = www.assetBundle;
                languageStrings.Clear();
                var japaneseFonts = bundle.LoadAllAssets();
                multiFont = bundle.LoadAsset<Font>("MultiLanguageFont.otf");
                LoadTranslationTxts();
                TranslateActiveTexts();

                if (loadMenu)
                    Destroy(menu);
                // Unload the AssetBundles compressed contents to conserve memory
                bundle.Unload(false);

            } // memory is freed from the web stream (www.Dispose() gets called implicitly)
        }
    }


    void ErrorInLoadLanguage()
    {
        SetActualLanguage(Language.eng);
    }


    public void ChangeLanguage()
    {
        if ((int)actualLanguage == 8)
            actualLanguage = Language.eng;
        else
            actualLanguage++;
        SetActualLanguage(actualLanguage);
    }

    void LoadTranslationTxts()
    {
        if (actualLanguage == Language.ptbr)
            fgCSVReader.LoadFromFile("ptbr", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        else if (actualLanguage == Language.ru)
            fgCSVReader.LoadFromFile("ru", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        else if (actualLanguage == Language.jp)
            fgCSVReader.LoadFromFile("jp", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        else if (actualLanguage == Language.ar)
            fgCSVReader.LoadFromFile("ar", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        else if (actualLanguage == Language.es)
            fgCSVReader.LoadFromFile("es", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        else if (actualLanguage == Language.sk)
            fgCSVReader.LoadFromFile("sk", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        else if (actualLanguage == Language.cn)
            fgCSVReader.LoadFromFile("cn", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        else
            fgCSVReader.LoadFromFile("usa", new fgCSVReader.ReadLineDelegate(ReadLineTest));
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

    void ReadLineTest(int line_index, List<string> line)
    {
        languageStrings.Add(line[0], line[1]);
    }

    public string GetMyStringText(string name, string originalString = "")
    {
        if (languageStrings.ContainsKey(name))
        {
            string myText = languageStrings[name].Replace("\\n", "\n");
            return myText;
        }
        else
            return originalString;
    }

}

public enum FontTypes
{
    Toonish,
    Dimbo,
    Liberation
}


