using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
		processing = true;
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
			if (starForce.stars.Count > i && starForce.forces.Count > i)
			{
				targetColor += starForce.stars[i].color * starForce.forces[i];
			}
		}

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
		return starForce.GetPart(star);
	}



}
