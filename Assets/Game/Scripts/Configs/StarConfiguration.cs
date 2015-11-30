using UnityEngine;
using System.Collections;

[System.Serializable]
public class StarConfiguration
{
	[SerializeField] float _power = 1f;
	[SerializeField] Vector3 _gravityAxis = Vector3.forward;
	[SerializeField] Color _starColor = Color.white;
	[SerializeField] Color _disableColor = Color.black;
	[SerializeField] float _changeSpeed = 1.0f;
	
	public float power 
	{ 
		get { return _power; }
		set { _power = value; }
	}
	
	public Vector3 gravityAxis 
	{ 
		get { return _gravityAxis; }
	}
	
	public Color starColor 
	{ 
		get { return _starColor; }
	}
	
	public Color disableColor 
	{ 
		get { return _disableColor; }
	}
	
	public float changeSpeed 
	{ 
		get { return _changeSpeed; }
	}


	public void CopyConfig(StarConfiguration config)
	{
		_power = config.power;
		_gravityAxis = config.gravityAxis;
		_starColor = config.starColor;
		_disableColor = config.disableColor;
		_changeSpeed = config.changeSpeed;
	}

}