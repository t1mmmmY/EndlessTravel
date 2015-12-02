using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Gravity))]
public class StarStats : MonoBehaviour
{
	[SerializeField] MeshRenderer starRender;
	[SerializeField] Material baseMaterial;
	[SerializeField] Transform starTransform;

	[SerializeField] StarConfiguration starConfig;


	public Color color
	{
		get { return starConfig.starColor; }
	}

	public float power
	{
		get { return starConfig.power; }
	}

	public Vector3 position 
	{ 
		get { return transform.position; } 
	}

	public float radius
	{
		get { return power; }
	}
	

	string colorKey = "_EmissionColor";
	Gravity gravity;
	float minPower = 0.1f;
	
	float oldPower = 0;

	public bool isActive { get; private set; }

	public int number { get; private set; }



	void Awake()
	{
		gravity = GetComponent<Gravity>();
		number = StarsManager.GetNumber();

		Init();
		oldPower = starConfig.power;
	}


	void Init()
	{
		gravity.Init(starConfig.power, starConfig.gravityAxis, this);
		SetSize();
		
		starRender.material = new Material(baseMaterial);
		starRender.material.SetColor(colorKey, starConfig.starColor);
	}

	public void SetDependency(float dependency)
	{
		starConfig.power = Mathf.Lerp(starConfig.power, dependency / CONST.PARTICLES_COEFFICIENT, Time.deltaTime * starConfig.changeSpeed);

		
		if (oldPower != starConfig.power)
		{
			SetPower();
			oldPower = starConfig.power;
		}
	}

	public void SetConfiguration(StarConfiguration config)
	{
		starConfig.CopyConfig(config);
		Init();
	}

	public void SetActiveStatus(bool isActive)
	{
		this.isActive = isActive;
		if (isActive)
		{
			gravity.EnableGravity();
		}
		else
		{
			gravity.DisableGravity();
		}
	}


	void SetPower()
	{
		gravity.SetPower(starConfig.power);
		SetSize();

		if (starConfig.power < minPower)
		{
			StarsManager.Instance.DestroyStar(this);
		}
	}

	void SetSize()
	{
		starTransform.localScale = Vector3.one * starConfig.power / 2.0f;
	}



}
