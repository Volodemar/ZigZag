using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[Tooltip("Панель меню")]
	public GameObject Menu;

	[Tooltip("Очки текст")]
	public GameObject Scores;

	private GameController GameController;

	private void Awake()
	{
		GameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void Update()
    {
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if(GameController.gameState != GameController.GameState.Paused)
			{
				OnShowMenu(true);
			}
			else 
			{
				OnShowMenu(false);
				GameController.gameState = GameController.GameState.Play;
			}
		}
    }

	/// <summary>
	/// Скрыть/показать меню
	/// </summary>
	public void OnShowMenu(bool isShow)
	{
		if(isShow)
		{
			GameController.gameState = GameController.GameState.Paused;
			Menu.SetActive(true);
		}
		else
		{
			Menu.SetActive(false);
			GameController.gameState = GameController.GameState.Start;
		}		
	}

	/// <summary>
	/// Отобразить новое значение счета игрока
	/// </summary>
	/// <param name="value"></param>
	public void OnScoresSet(int value)
	{
		Scores.GetComponent<Text>().text = "Scores: " + value.ToString();
	}
}
