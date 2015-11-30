using UnityEngine;
using System.Collections;

public class ParticlesPool : BaseSingleton<ParticlesPool> 
{
	[SerializeField] SpaceParticle particlePrefab;

	SpaceParticle[] allParticles;

	int pointer = 0;

	protected override void Awake ()
	{
		CreateParticles(CONST.MAX_PARTICLES_COUNT);
		base.Awake ();
	}

	void CreateParticles(int count)
	{
		allParticles = new SpaceParticle[count];

		for (int i = 0; i < count; i++)
		{
			allParticles[i] = InstantiateParticle(false);
		}
	}

	SpaceParticle InstantiateParticle(bool isActive)
	{
		GameObject go = GameObject.Instantiate<GameObject>(particlePrefab.gameObject);
		go.transform.parent = this.transform;
		go.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
		go.SetActive(isActive);

		return go.GetComponent<SpaceParticle>();
	}

	public void GetParticles(int count, Vector3 starPosition, float radius)
	{
		int start = pointer;
		int end = pointer + count;

		if (end > allParticles.Length)
		{
			//TODO
			//need to combine particles
			return;
		}

		for (int i = start; i < end; i++)
		{
			allParticles[i].transform.position = starPosition + new Vector3(Random.Range(-radius * 2, radius * 2), 
			                                                                Random.Range(-radius * 2, radius * 2), 
			                                                                Random.Range(-radius * 2, radius * 2));
			allParticles[i].gameObject.SetActive(true);
		}

		pointer += count;
	}

}
