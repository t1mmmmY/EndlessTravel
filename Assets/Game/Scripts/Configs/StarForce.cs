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