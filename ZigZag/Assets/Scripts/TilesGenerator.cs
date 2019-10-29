using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
	public GameController GameController;

	[Tooltip("Префаб платформа1")]
	public GameObject mainTile1;
	[Tooltip("Префаб платформа2")]
	public GameObject mainTile2;
	[Tooltip("Префаб платформа3")]
	public GameObject mainTile3;

	private GameObject mainTile;
	private CrystalGenerator CrystalGenerator;

	private Vector3 NextA;
	private Vector3 NextB;
	private Vector3 LastPos;
	private float Scaler;

	System.Random rnd = new System.Random();

	private void Awake()
	{
		GameController = GameObject.Find("GameController").GetComponent<GameController>();
		CrystalGenerator = this.GetComponent<CrystalGenerator>();
	}

	void Start()
    {
		ResetWorld();
    }

	private void Update()
	{
		if(GameController.gameState == GameController.GameState.Play)
		{
			if(Vector3.Distance(LastPos, GameController.Player.transform.position) < 5f)
			{
				CreateNext();
			}
		}
	}

	/// <summary>
	/// Пересоздание мира
	/// </summary>
	public void ResetWorld()
	{
		//Выбор тайлов от сложности
		switch (GameController.Dificult)
		{
			case 1:
				mainTile = mainTile3;
				Scaler = 3;
				break;
			case 2:
				mainTile = mainTile2;
				Scaler = 2;
				break;
			case 3:
				mainTile = mainTile1;
				Scaler = 1;
				break;
			default:
				mainTile = mainTile1;
				Scaler = 1;
				break;
		}


		//Удаляем дочерние
		for(int i=0; i<this.transform.childCount; i++)
		{
			Destroy(this.transform.GetChild(i).gameObject);
		}

		//Генерируем площадку
		for(int x=-1;x<2;x++)
		{
			for(int z=-1;z<2;z++)
			{
				CreateTile(this.transform.position, new Vector3(x*Scaler,	0,	z*Scaler), false);
			}
		}

		switch (GameController.levelDirection)
		{
			case GameController.LevelDirection.Forvard:
				LastPos = this.transform.position + new Vector3(-1*Scaler, 0, -1*Scaler);
				NextA	= new Vector3(0,0,-1*Scaler);
				NextB	= new Vector3(-1*Scaler,0,0);
				break;
			case GameController.LevelDirection.Right:
				LastPos = this.transform.position + new Vector3(-1*Scaler, 0, 1*Scaler);
				NextA	= new Vector3(-1*Scaler,0,0);
				NextB	= new Vector3(0,0,1*Scaler);
				break;
			default:
				LastPos = this.transform.position + new Vector3(-1*Scaler, 0 -1*Scaler);
				NextA	= new Vector3(0,0,-1*Scaler);
				NextB	= new Vector3(-1*Scaler,0,0);
				break;
		}

		//Генерируем путь
		for(int i=0; i<5; i++)
		{
			CreateNext();
		}
	}

	/// <summary>
	/// Создание следующего тайла согласно пути
	/// </summary>
	private void CreateNext()
	{
		int AB = rnd.Next(2);
		if(AB == 0)
		{
			LastPos = CreateTile(LastPos, NextA, true);
		}
			
		if(AB == 1)
		{
			LastPos = CreateTile(LastPos, NextB, true);
		}
	}

	/// <summary>
	/// Создание тайтла пути
	/// </summary>
	/// <param name="MainPos">Координаты предыдущего тайтла</param>
	/// <param name="AddPos">Направление</param>
	private Vector3 CreateTile(Vector3 MainPos, Vector3 Direction, bool isCreatedCrystal)
	{
		GameObject newCube = Instantiate(mainTile, MainPos+Direction, Quaternion.identity, this.transform);

		//Вызов события создание кристала
		if(isCreatedCrystal)
			CrystalGenerator.OnCreateCrystal(newCube.transform.position);

		return newCube.transform.position;
	}
}
