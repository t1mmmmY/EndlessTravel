using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ParticleInfo
{
	public SpaceParticle spaceParticle;
	public Vector3 position;
	public Vector3 force = Vector3.zero;
	public float angularPower = 0;
	public float slowDown = 1;

	public ParticleInfo(SpaceParticle spaceParticle)
	{
		this.spaceParticle = spaceParticle;
		this.position = spaceParticle.transform.position;
	}
}

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

public class Gravity : MonoBehaviour 
{
	[SerializeField] GravityConfiguration gravityConfiguration;

	[SerializeField] int deltaTime = 50;

	Material material;
	bool isAlive = true;
	bool calculate = false;

	Vector3 starPosition = Vector3.zero;
	List<ParticleInfo> usedParticles;
	System.Action onCalculate;
//	[SerializeField] float elapsedTime = 0.0015f;
	float timeScale = 0.0015f;
	Color starColor = Color.white;

	StarStats stats;

	int particleCount = 0;
	float dependency = 0f;


	public void Init(float power, Vector3 gravityAxis, StarStats stats)
	{
		SetPower(power);
		gravityConfiguration.SetAxis(gravityAxis);
		this.stats = stats;
	}

	public void SetPower(float power)
	{
		gravityConfiguration.SetPower(power);
	}

	void OnEnable()
	{
		isAlive = true;

		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		if (meshRenderer != null)
		{
			material = meshRenderer.sharedMaterial;
		}

		if (material != null)
		{
			starColor = material.GetColor("_EmissionColor");
		}

		Loom.RunAsync(CalculateGravity);
	}

	void OnDisable()
	{
		isAlive = false;
	}

	void OnDestroy()
	{
		isAlive = false;
	}

	void GetParticles() 
	{
		starPosition = transform.position;

		usedParticles = ParticlesManager.GetAllParticlesInRadius(starPosition, gravityConfiguration.gravityRadius);

		calculate = true;
	}

//	void Update()
//	{
////		elapsedTime += Time.deltaTime;
//	}

	void CalculateGravity()
	{
		do
		{
			calculate = false;
			Loom.QueueOnMainThread(GetParticles);

			do 
			{
				System.Threading.Thread.Sleep(5);
			} while (!calculate);

			if (usedParticles != null)
			{
				foreach (ParticleInfo particle in usedParticles)
				{
					float distance = Vector3.Distance(starPosition, particle.position);
					float distanceSlowdown = distance < 1 ? 1 : Mathf.Pow(distance, 2);
					Vector3 direction = starPosition - particle.position;
					float power = 0;
					particle.force = Vector3.zero;
					
					if (distance <= gravityConfiguration.inRadius)
					{
						power = -gravityConfiguration.inPower;
					}
					else
					{
						power = gravityConfiguration.outPower;
					}

					dependency += particle.spaceParticle.Dependency(stats);

					particle.force += (Vector3.Cross(direction, gravityConfiguration.axis).normalized * gravityConfiguration.angularPower / distanceSlowdown);
					
					if (power != 0)
					{
						float finalPower = power / distanceSlowdown;
						particle.force += direction * finalPower;
					}

					if (particle.force != Vector3.zero)
					{
						particle.spaceParticle.AddForce(particle.force * timeScale);
					}
					if (particle.slowDown != 1)
					{
						particle.spaceParticle.SlowDown(particle.slowDown);
					}
//					particle.spaceParticle.SetColor(stats, Mathf.Abs(gravityConfiguration.colorPower) * elapsedTime);
				}
			}
			
			Loom.QueueOnMainThread(OnCalculate);
			
			System.Threading.Thread.Sleep(deltaTime);
			
		} while (isAlive);
	}

	/// <summary>
	/// Assign force. Find new ones
	/// </summary>
	void OnCalculate()
	{
		foreach (ParticleInfo particle in usedParticles)
		{
			particle.spaceParticle.SetColor(stats, Mathf.Abs(gravityConfiguration.colorPower) * timeScale);
		}

//		elapsedTime = 0;
		if (stats != null)
		{
			stats.SetDependency(dependency);
		}
		dependency = 0.0f;
	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, gravityConfiguration.gravityRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, gravityConfiguration.outRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, gravityConfiguration.inRadius);
	}

}
