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

	void CreateStar(Vector3 position, bool isPlayable = false)
	{
		GameObject go = GameObject.Instantiate(starPrefab, position, Quaternion.identity) as GameObject;

		if (isPlayable)
		{
			go.AddComponent(typeof(StarController));
			playableStar = go.GetComponent<StarStats>();
		}
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
