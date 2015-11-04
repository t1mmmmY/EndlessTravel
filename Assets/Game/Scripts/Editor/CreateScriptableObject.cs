using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateScriptableObject : MonoBehaviour {

	[MenuItem("Assets/Create/StarVariations")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<StarVariations>();
	}
}
