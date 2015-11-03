using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Gravity))]
public class StarStats : MonoBehaviour
{
	[SerializeField] MeshRenderer starRender;
	[SerializeField] Material baseMaterial;
	[SerializeField] Transform starTransform;
//	[SerializeField] SphereCollider starCollider;

//	[SerializeField] float size = 1f;
	[SerializeField] float power = 1f;
	[SerializeField] Vector3 gravityAxis = Vector3.forward;
	[SerializeField] Color starColor = Color.white;
	[SerializeField] Color disableColor = Color.black;
	[SerializeField] float changeSpeed = 1.0f;

	public Color color
	{
		get { return starColor; }
	}
	

	string colorKey = "_EmissionColor";
	Gravity gravity;
	float minPower = 0.1f;
	
	float oldPower = 0;

	public int number { get; private set; }

	void Awake()
	{
		gravity = GetComponent<Gravity>();
		number = StarsManager.GetNumber();

		Init();
		oldPower = power;
	}


	void Init()
	{
		gravity.Init(power, gravityAxis, this);
		SetSize();
		
		starRender.material = new Material(baseMaterial);
		starRender.material.SetColor(colorKey, starColor);
	}

	public void SetDependency(float dependency)
	{
		power = Mathf.Lerp(power, dependency / 500.0f, Time.deltaTime * changeSpeed);
		
		if (oldPower != power)
		{
			SetPower();
			oldPower = power;
		}
	}


	void SetPower()
	{
		gravity.SetPower(power);
		SetSize();

		if (power < minPower)
		{
			StarsManager.Instance.DestroyStar(this);
		}
	}

	void SetSize()
	{
		starTransform.localScale = Vector3.one * power / 2.0f;
	}



}
