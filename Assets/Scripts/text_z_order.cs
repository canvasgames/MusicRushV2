using UnityEngine;
using System.Collections;

public class text_z_order : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
        //this.GetComponent<Renderer>().sortingOrder = 100000;
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Renderer>().sortingLayerID = this.transform.parent.GetComponent<Renderer>().sortingLayerID;
        this.GetComponent<Renderer>().sortingOrder = this.transform.parent.GetComponent<Renderer>().sortingOrder;
    }
}
