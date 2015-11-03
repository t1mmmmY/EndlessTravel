using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ParticlesManager 
{
	private static int currentNumber = 0;

	private static List<SpaceParticle> allParticles;

	static ParticlesManager()
	{
		allParticles = new List<SpaceParticle>();
	}

	public static bool AddParticle(SpaceParticle particle)
	{
		if (!allParticles.Contains(particle))
		{
			allParticles.Add(particle);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static bool RemoveParticle(SpaceParticle particle)
	{
		if (allParticles.Contains(particle))
		{
			allParticles.Remove(particle);
			return true;
		}
		else
		{
			return false;
		}
	}

//	public static List<SpaceParticle> GetAllParticlesInRadius(Vector3 position, float radius)
//	{
//		return (from particle in allParticles where 
//		        Vector3.Distance(position, particle.transform.position) <= radius select particle) as List<SpaceParticle>;
//	}


	public static List<ParticleInfo> GetAllParticlesInRadius(Vector3 position, float radius)
	{
//		IEnumerable<ParticleInfo> particlesInRadius = (from particle in allParticles where 
//		                                               Vector3.Distance(position, particle.transform.position) <= radius select new ParticleInfo(particle));

		var part = (from particle in allParticles where Vector3.Distance(position, particle.transform.position) <= radius select new ParticleInfo(particle));

		List<ParticleInfo> particlesInRadius = new List<ParticleInfo>();

		foreach (ParticleInfo sp in part)
		{
			particlesInRadius.Add(sp);
		}

//		foreach (SpaceParticle sp in allParticles)
//		{
//			if (Vector3.Distance(position, sp.transform.position) <= radius)
//			{
//				particlesInRadius.Add(new ParticleInfo(sp));
//			}
//		}

		return particlesInRadius;
	}

//	public static IEnumerable<ParticleInfo> GetAllParticlesInRadius(Vector3 position, float radius)
//	{
//		IEnumerable<ParticleInfo> particlesInRadius = (from particle in allParticles where 
//		         Vector3.Distance(position, particle.transform.position) <= radius select new ParticleInfo(particle));
//
//		return particlesInRadius;
//	}

	public static int GetNumber()
	{
		currentNumber++;
		return currentNumber;
	}
}
