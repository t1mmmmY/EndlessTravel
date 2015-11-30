using UnityEngine;
using System.Collections;

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

	StarStats playableStar;

	public static System.Action onGameOver;
	public static System.Action<StarStats> onSetPlayableStar;

	protected override void Awake ()
	{
		Application.targetFrameRate = 30;
		base.Awake ();
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

			if (onSetPlayableStar != null)
			{
				onSetPlayableStar(starStats);
			}
		}

		return starStats;
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
			Destroy(star.gameObject);
		}
	}

}
