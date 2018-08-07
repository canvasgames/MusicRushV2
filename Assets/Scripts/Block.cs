using UnityEngine;
using System.Collections;
using System;

[Serializable]
public enum ObstacleType {spk, tripleSpk, hiddenSpk, hiddenTripleSpk, saw, hole, hiddenHole, wallCorner, wallCenter, }
[Serializable]
public class Obstacle{
	public ObstacleType myType;
	public float xInit, xEnd;
}
[Serializable]
public class Block  {
	public string name;
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
