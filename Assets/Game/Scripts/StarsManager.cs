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
//	[SerializeField] float _timeScale = 0.0015f;
//
//	public float timeScale
//	{
//		get { return timeScale; }
//	}

	StarStats playableStar;

	public static System.Action onGameOver;

	//TEMP
	public void SetPlayableStar(StarStats star)
	{
		playableStar = star;
	}

	protected override void Awake ()
	{
		Application.targetFrameRate = 30;
		base.Awake ();
	}

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
