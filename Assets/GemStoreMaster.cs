using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemStoreMaster : MonoBehaviour {

	public Text PriceS, PriceM, PriceL, PriceXL, PriceXXL;
	public string curCountry = "Philippines";
	// Use this for initialization
	void OnEnable(){

		if (curCountry == "Philippines") {
			if (PriceS != null)
				PriceS.text = "$ 79";
			if (PriceM != null)
				PriceM.text = "$ 159";
			if (PriceL != null)
				PriceL.text = "$ 319";
			if (PriceXL != null)
				PriceXL.text = "$ 905"; // 365
			if (PriceXXL != null)
				PriceXXL.text = "$ 164";
		}

		else if (curCountry == "Brazil") {
			if (PriceS != null)
				PriceS.text = "R$ 2.99";
			if (PriceM != null)
				PriceM.text = "R$ 5.99";
			if (PriceL != null)
				PriceL.text = "R$ 11.99";
			if (PriceXL != null)
				PriceXL.text = "R$ 32.99";
			if (PriceXXL != null)
				PriceXXL.text = "$ 164";
		}

		else  {
			if (PriceS != null)
				PriceS.text = "$ 1.49";
			if (PriceM != null)
				PriceM.text = "$ 2.99";
			if (PriceL != null)
				PriceL.text = "$ 5.99";
			if (PriceXL != null)
				PriceXL.text = "$ 16.99";
			if (PriceXXL != null)
				PriceXXL.text = "$ 164";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
