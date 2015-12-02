using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarAI : BaseStarMovement 
{
	float distanceOfView = 50.0f;
	List<StarStats> allStars;
	List<StarStats> smallerStars;
	List<StarStats> biggerStars;

	Transform target;

	void Start()
	{
		StartCoroutine("BrainWork");
	}

	void OnDestroy()
	{
		StopCoroutine("BrainWork");
	}

	IEnumerator BrainWork()
	{
		do
		{
			FindNearestStars();
			SplitStarsBySize();

			smallerStars = SortStarsByDistance(smallerStars);

			target = FindTarget();
			if (target == null)
			{
				//better run

			}

			yield return new WaitForSeconds(CONST.AI_TIMESTAMP);
		} while (true);
	}

	void Update()
	{
		if (target != null)
		{
			Move(GetDirection());
		}
	}

	Vector2 GetDirection()
	{
		Vector3 direction3d = target.position - stats.position;
		return (Vector2)direction3d.normalized;
	}

	void FindNearestStars()
	{
		allStars = new List<StarStats>();
		allStars = StarsManager.Instance.StarsInRadius(stats, distanceOfView);
	}

	void SplitStarsBySize()
	{
		smallerStars = new List<StarStats>();
		biggerStars = new List<StarStats>();

		foreach (StarStats star in allStars)
		{
			if (star.power <= stats.power)
			{
				smallerStars.Add(star);
			}
			else
			{
				biggerStars.Add(star);
			}
		}

	}

	List<StarStats> SortStarsByDistance(List<StarStats> stars)
	{
		stars.Sort(delegate(StarStats a, StarStats b) 
		{
			if (Vector3.Distance(stats.position, a.position) < Vector3.Distance(stats.position, b.position))
			{
				return -1;
			}
			else
			{
				return 1;
			}
		});

		return stars;
	}

	Transform FindTarget()
	{
		foreach (StarStats star in smallerStars)
		{
			if (IsAchievable(star))
			{
				return star.transform;
			}
		}

		if (smallerStars.Count > 0)
		{
			return smallerStars[0].transform;
		}
		else
		{
			return null;
		}
	}

	bool IsAchievable(StarStats star)
	{
		//TODO
		return true;
	}

}
