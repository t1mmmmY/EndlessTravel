using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StarForce
{
	public List<StarStats> stars;
	public List<float> forces;

	public StarForce()
	{
		forces = new List<float>();
		stars = new List<StarStats>();
	}

	public void AddForce(StarStats star, float force)
	{
		int starNumber = GetStarNumber(star);

		//New star
		if (starNumber == -1)
		{
			stars.Add(star);
			forces.Add(force);
			starNumber = stars.Count - 1;
		}
		else
		{
			forces[starNumber] += force;
		}

		RedistributionForce();
	}

	public float GetPart(StarStats star)
	{
		int starNumber = GetStarNumber(star);

		if (starNumber == -1)
		{
			return 0;
		}
		else
		{
			return forces[starNumber];
		}
	}

	void RedistributionForce()
	{
		float forceSum = 0f;

		foreach (float f in forces)
		{
			forceSum += f;
		}

		if (forceSum > 1.0f)
		{
			for (int i = 0; i < forces.Count; i++)
			{
				forces[i] *= 1.0f / forceSum;
			}
		}
	}


	int GetStarNumber(StarStats star)
	{
		for (int i = 0; i < stars.Count; i++)
		{
			if (stars[i].number == star.number)
			{
				return i;
			}
		}
		return -1;
	}
}


[RequireComponent(typeof(MySexyRigidbody))]
public class SpaceParticle : MonoBehaviour 
{

	new SpriteRenderer renderer;
	MySexyRigidbody sexyRigidbody;

	Vector3 force = Vector3.zero;
	float multiplicator = 1.0f;
	Color currentColor = Color.white;
	Color targetColor = Color.white;

	bool processing = true;

//	int particleNumber = 0;
	private float timeStamp = 0.1f;

	StarForce starForce;

	void Awake()
	{
		starForce = new StarForce();
		renderer = GetComponent<SpriteRenderer>();
		sexyRigidbody = GetComponent<MySexyRigidbody>();
	}

	void OnEnable()
	{
		currentColor = renderer.color;
		StartCoroutine("CustomUpdate");
		ParticlesManager.AddParticle(this);
	}

	void OnDisable()
	{
		ParticlesManager.RemoveParticle(this);
		StopCoroutine("CustomUpdate");
		processing = false;
	}

	IEnumerator CustomUpdate()
	{
		do
		{
			yield return new WaitForSeconds(timeStamp);

//			string log = string.Empty;
//			for (int i = 0; i < starForce.forces.Count; i++)
//			{
//				log += starForce.stars[i].name + " " + starForce.forces[i].ToString() + '\n';
//			}
//			Debug.Log(log);

			if (force != Vector3.zero)
			{
				sexyRigidbody.AddForce(force);
				force = Vector3.zero;
			}

			if (multiplicator != 1.0f)
			{
				sexyRigidbody.velocity *= multiplicator;
				multiplicator = 1.0f;
			}

			if (targetColor != renderer.color)
			{
				renderer.color = targetColor;
				currentColor = renderer.color;
			}

		} while (processing);
	}


	public void SetColor(StarStats starStats, float intensity)
	{
		starForce.AddForce(starStats, intensity);

		targetColor = Color.black;

		for (int i = 0; i < starForce.forces.Count; i++)
		{
			targetColor += starForce.stars[i].color * starForce.forces[i];
		}

//		targetColor = Color.Lerp(currentColor, starStats.GetStarColor(), intensity);
	}


	public void AddForce(Vector3 newForce)
	{
		force += newForce;
	}


	public void SlowDown(float multiplicator)
	{
		this.multiplicator *= multiplicator;
	}

	public float Dependency(StarStats star)
	{
//		float dependency = 1.0f - ((Mathf.Abs(starColor.r - currentColor.r) + Mathf.Abs(starColor.g - currentColor.g) + Mathf.Abs(starColor.b - currentColor.b)) / 3.0f);
		return starForce.GetPart(star);
	}



}
