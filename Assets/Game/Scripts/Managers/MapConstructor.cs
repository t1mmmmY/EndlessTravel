using UnityEngine;
using System.Collections;

public class MapConstructor : BaseSingleton<MapConstructor> 
{
	[SerializeField] float density = 0.2f;
	[SerializeField] StarVariations playableStarVariations;
	[SerializeField] StarVariations enemyStarVariations;
	StarsManager starManager;

	StarStats playableStar;

	protected override void Awake ()
	{
		StarsManager.onSetPlayableStar += OnSetPlayableStar;
		base.Awake ();
	}

	protected override void OnDestroy ()
	{
		StarsManager.onSetPlayableStar -= OnSetPlayableStar;
		base.OnDestroy ();
	}

	void Start()
	{
		starManager = StarsManager.Instance;

		InitGame();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CreateEnemyStar();
		}
	}

	private void OnSetPlayableStar(StarStats playableStar)
	{
		this.playableStar = playableStar;
	}


	private void InitGame()
	{
		CreatePlayableStar();
		for (int i = 0; i < CONST.INITIAL_STARS_COUNT; i++)
		{
			CreateEnemyStar();
		}
	}

	private void CreatePlayableStar()
	{
		starManager.CreateStar(Vector3.zero, GetRandomStarConfig(playableStarVariations), true);
	}

	private void CreateEnemyStar()
	{
		starManager.CreateStar(GetRandomPosition(), GetRandomStarConfig(enemyStarVariations), false);
	}

	private Vector3 GetRandomPosition()
	{
		float minDistance = playableStar.power * 4;
		float distance = 0;
		Vector3 position = Vector3.zero;
		do 
		{
			position = playableStar.transform.position + new Vector3(Random.Range(-playableStar.power * 30, playableStar.power * 30),
			                                                         Random.Range(-playableStar.power * 30, playableStar.power * 30),
			                                                         0);
			distance = Vector3.Distance(position, playableStar.transform.position);
		} while (distance < minDistance);

		return position;
//		return playableStar.transform.position + new Vector3((Random.Range(0, 2) * 2 - 1) * 8,
//		                                                     (Random.Range(0, 2) * 2 - 1) * 8,
//		                                                     0);
	}

	private StarConfiguration GetRandomStarConfig(StarVariations variations)
	{
		return variations.configurations[Random.Range(0, variations.configurations.Length)];
	}

}
