using UnityEngine;
//using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class MenuItems
{
    static List<Transform> listaA ;
    static List<Transform> listaB ;
   // [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

  
}