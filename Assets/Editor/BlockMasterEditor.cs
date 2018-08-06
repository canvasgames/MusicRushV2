using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(BlockMaster))]

public class BlockMasterEditor : Editor {
	

	public override void OnInspectorGUI(){
	/*	if (GUILayout.Button ("UPDATE")) {
			
			obj.UpdateBlockListName ();

		}
		/*
		GUILayout.Label ("asdasd");
		//GUILayout.;

		EditorGUILayout.LabelField("Custom editor:");
		var serializedObject = new SerializedObject(target);
		var property = serializedObject.FindProperty("BlockList");
		serializedObject.Update();
		EditorGUILayout.PropertyField(property, true);
		//EditorGUILayout.prop
		serializedObject.ApplyModifiedProperties();
		*/
//		BlockMaster myScript = (BlockMaster)target;
//		var serializedObject = new SerializedObject(target);
//		List<Block> property = serializedObject.FindProperty("BlocksSuperEasy");

	
//		//new code from here
//		List<Block> property = myScript.BlocksSuperEasy;
//
//		foreach (Block b in property) {
////	
//			if (b.type == BlockType.SuperEasySpkMid) {
//				myScript.iField = EditorGUILayout.ObjectField ("TestField", myScript.iField, typeof(InputField), true) as InputField;
//			}
//		}
//
		DrawDefaultInspector ();
	}

	void OnGUI (){
		GUILayout.Label ("abc");
	}

}
