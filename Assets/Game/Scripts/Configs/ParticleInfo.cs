using UnityEngine;
using System.Collections;

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