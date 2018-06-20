using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class MenusEditor
{

    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }
}
