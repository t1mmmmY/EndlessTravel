using UnityEngine;
using System.Collections;

public class StarVariations : ScriptableObject 
{
	[SerializeField] StarConfiguration[] configs;

	public StarConfiguration[] configurations
	{
		get { return configs; }
	}

}
