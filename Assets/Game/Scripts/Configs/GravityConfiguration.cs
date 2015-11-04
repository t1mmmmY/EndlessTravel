using UnityEngine;
using System.Collections;

[System.Serializable]
public class GravityConfiguration
{
	[SerializeField] float baseGravityRadius = 6;
	[SerializeField] float baseOutRadius = 1.5f;
	[SerializeField] float baseInRadius = 1.25f;
	[SerializeField] float baseOutPower = 50;
	[SerializeField] float baseInPower = 5;
	[SerializeField] float baseAngularPower = 22.5f;
	[SerializeField] float baseColorPower = 1.0f;
	
	
	public float gravityRadius { get; private set; }
	public float outRadius { get; private set; }
	public float inRadius { get; private set; }
	public float outPower { get; private set; }
	public float inPower { get; private set; }
	public float angularPower { get; private set; }
	public float colorPower { get; private set; }
	
	public Vector3 axis { get; private set; }
	
	
	public float power { get; private set; }
	
	public void SetPower(float power)
	{
		this.power = power;
		
		gravityRadius = baseGravityRadius * power;
		outRadius = baseOutRadius * power;
		inRadius = baseInRadius * power;
		outPower = baseOutPower * power;
		inPower = baseInPower * power;
		angularPower = baseAngularPower * power;
		
		colorPower = baseColorPower * power;
	}
	
	public void SetAxis(Vector3 axis)
	{
		this.axis = axis;
	}
}