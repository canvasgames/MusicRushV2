using UnityEngine;
using System.Collections;
using System;

[Serializable]
public enum ObstacleType {spk, tripleSpk, hiddenSpk, hiddenTripleSpk, saw, hole, hiddenHole, wallCorner, wallCenter, }
[Serializable]
public class Obstacle{
	[HideInInspector]public string name;
	public ObstacleType myType;
	public float xInit, xEnd;
	bool mirrored;

	public Obstacle(string name){
		this.name = name;
	}
	public void UpdateName(bool holeCase = false){
		if (holeCase == false) {
			if (xEnd == 0)
				name = myType.ToString () + "_" + xInit.ToString ();
			else
				name = myType.ToString () + "_[" + xInit.ToString () + ", " + xEnd.ToString () + "]";
		} else {
			name = "HOLE_JUST_INIT_X";
		}
		//Debug.Log ("name: " + name);
	}
}
[Serializable]
public class Block  {
	[HideInInspector] public string name;
	public int chance;
	public BlockType type;
	public DenyCondition[] denyConditions;
	public Obstacle[] obstacles; 
	public Obstacle[] obstaclesFloor2; 

	public Block(string name){
		this.name = name;
	}
	public void UpdateName(){
		name = type.ToString () + "_" + chance.ToString ();
		//Debug.Log ("name: " + name);
	}
}
