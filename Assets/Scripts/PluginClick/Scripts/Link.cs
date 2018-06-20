using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{


	public void OpenLink()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=mominis.Generic_Android.Bomblast");
    }

	public void OpenLinkJS()
	{
		//Application.ExternalEval("window.open('""https://play.google.com/store/apps/details?id=mominis.Generic_Android.Bomblast""');");
	}

	public void OpenLinkJSPlugin()
	{
#if !UNITY_EDITOR
		openWindow("https://play.google.com/store/apps/details?id=mominis.Generic_Android.Bomblast");
#endif
    }

    [DllImport("__Internal")]
	private static extern void openWindow(string url);

}