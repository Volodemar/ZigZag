using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	[Tooltip("Объект персонаж")]
	public GameObject Player;

	[Tooltip("Объект генератор мира")]
	public GameObject World;

	[Tooltip("Канвас")]
	public MenuController MenuController;

	[Tooltip("Скорост игрока")]
	public float Speed;

	[Tooltip("Сложность дорожки 1-3")]
	public int Dificult = 1;

	private int Scores = 0;

	//Направление игрового мира
	public enum LevelDirection
	{
		Forvard = 0,
		Right   = 1
	}
	public LevelDirection levelDirection; 

	public enum GameState
	{
		//Игра началась ожидание первого клика
		Start		= 0,
		//Игра в процессе
		Play		= 1,
		//Игра на паузе
		Paused		= 2,
		//Игра прогирана
		GameOver	= 3
	}
	public GameState gameState;

	private void Start()
	{
		gameState = GameState.Paused;
	}

	/// <summary>
	/// Событие выбор сложности
	/// </summary>
	/// <param name="dificult">Сложность 1-3</param>
	public void OnStartGame(int dificult)
	{
		Dificult = dificult;
		OnGameOver();
		MenuController.OnShowMenu(false);
	}

	/// <summary>
	/// Событие добавить игроку очки
	/// </summary>
	/// <param name="value"></param>
	public void OnScoresAdd(int value)
	{
		Scores = Scores + value;
		MenuController.OnScoresSet(Scores);
	}

	/// <summary>
	/// Игра закончена по ТЗ для устройства на работу, надо начинать с начала без перезапуска сцены
	/// </summary>
	public void OnGameOver()
	{
		gameState = GameState.GameOver;
		Scores = 0;
		MenuController.OnScoresSet(Scores);
		Player.GetComponent<MoveController>().SetDefPosition();
		World.GetComponent<TilesGenerator>().ResetWorld();
		gameState = GameState.Start;
	}

	/// <summary>
	/// Выйти из игры
	/// </summary>
	public void OnExit()
	{
		Application.Quit();
	}
}
