using UnityEngine;
using System.Collections;
using System;

[Serializable]

public class Block  {
	public string name;
	public int chance;
	public BlockType type;
	public DenyCondition[] denyConditions;

	public Block(string name){
		this.name = name;
	}
	public void UpdateName(){
		name = type.ToString () + "_" + chance.ToString ();
		//Debug.Log ("name: " + name);
	}
}
