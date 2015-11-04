using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Gravity : MonoBehaviour 
{
	[SerializeField] GravityConfiguration gravityConfiguration;

	[SerializeField] int deltaTime = 50;

	bool isAlive = true;
	bool calculate = false;

	Vector3 starPosition = Vector3.zero;
	List<ParticleInfo> usedParticles;

	System.Action onCalculate;

	StarStats stats;

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
						particle.spaceParticle.AddForce(particle.force * CONST.GRAVITY_TIME_SCALE);
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
			particle.spaceParticle.SetColor(stats, Mathf.Abs(gravityConfiguration.colorPower) * CONST.GRAVITY_TIME_SCALE);
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
