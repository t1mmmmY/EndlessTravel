using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StarsManager : BaseSingleton<StarsManager>
{
	static int currentNumber = 0;

	public static int GetNumber()
	{
		int num = currentNumber;
		currentNumber++;
		return num;
	}

	[SerializeField] GameObject starPrefab;
	[SerializeField] float timeStamp = 0.2f;
	[SerializeField] float baseActiveDistance = 4;

	StarStats playableStar;
	List<StarStats> allStar;

	public static System.Action onGameOver;
	public static System.Action<StarStats> onSetPlayableStar;

	protected override void Awake ()
	{
		Application.targetFrameRate = 30;
		allStar = new List<StarStats>();
		base.Awake ();
	}

	protected override void OnDestroy ()
	{
		StopCoroutine("StarStateCoroutine");
		base.OnDestroy ();
	}

	void Start()
	{
		StartCoroutine("StarStateCoroutine");
	}

	IEnumerator StarStateCoroutine()
	{
		do
		{
			foreach (StarStats star in allStar)
			{
				bool isActive = true;
//				bool isActive = Vector3.Distance(playableStar.position, star.position) < baseActiveDistance * playableStar.radius;
				if (isActive != star.isActive)
				{
					star.SetActiveStatus(isActive);
				}
			}

			yield return new WaitForSeconds(timeStamp);
		} while (true);
	}



	public StarStats CreateStar(Vector3 position, StarConfiguration config, bool isPlayable = false)
	{
		GameObject go = GameObject.Instantiate(starPrefab, position, Quaternion.identity) as GameObject;
		StarStats starStats = go.GetComponent<StarStats>();

		if (starStats == null)
		{
			Debug.LogError("StarStats required!");
			return null;
		}

		starStats.SetConfiguration(config);

		ParticlesPool.Instance.GetParticles((int)(CONST.PARTICLES_COEFFICIENT * starStats.power), go.transform.position, starStats.power);
		
		if (isPlayable)
		{
			go.AddComponent(typeof(StarController));
			playableStar = starStats;
			playableStar.SetActiveStatus(true);

			if (onSetPlayableStar != null)
			{
				onSetPlayableStar(starStats);
			}
		}
		else
		{
			go.AddComponent(typeof(StarAI));
			allStar.Add(starStats);
		}

		return starStats;
	}

	public List<StarStats> StarsInRadius(StarStats self, float radius)
	{
		List<StarStats> starsInRadius = new List<StarStats>();

		foreach (StarStats star in allStar)
		{
			if (star.number != self.number)
			{
				float distance = Vector3.Distance(self.position, star.position);
				if (distance <= radius)
				{
					starsInRadius.Add(star);
				}
			}
		}

		return starsInRadius;
	}


	public void DestroyStar(StarStats star)
	{
		//Is it playable star?
		if (star.number == playableStar.number)
		{
			if (onGameOver != null)
			{
				onGameOver();
			}
		}
		else
		{
			allStar.Remove(star);
			Destroy(star.gameObject);
		}
	}

}
