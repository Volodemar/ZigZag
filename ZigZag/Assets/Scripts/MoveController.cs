using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	private float Gravity = 5f;
	private float Speed   = 1f;
	
	private GameController GameController;
	private CharacterController controller;
	private Vector3 direction;

	private bool isLeftMove	= false;

	private void Awake()
	{
		GameController = GameController = GameObject.Find("GameController").GetComponent<GameController>();
		Speed = GameController.Speed;

		// На объекте персонаж, должен быть компонент CharacterController
		controller	= this.GetComponent<CharacterController>();
	}

	void Update()
    {
		if (Input.GetMouseButtonDown(0) && (GameController.gameState == GameController.GameState.Start || GameController.gameState == GameController.GameState.Play))
		{
			controller.transform.position = this.transform.position;

			if(isLeftMove)
			{
				if(GameController.levelDirection == GameController.LevelDirection.Forvard)
				{
					direction = new Vector3(0 * Speed * Time.fixedDeltaTime, 0, -1 * Speed * Time.fixedDeltaTime);
				}
				else
				{
					direction = new Vector3(-1 * Speed * Time.fixedDeltaTime, 0, 0 * Speed * Time.fixedDeltaTime);
				}
			}
			else
			{
				if(GameController.levelDirection == GameController.LevelDirection.Forvard)
				{
					direction = new Vector3(-1 * Speed * Time.fixedDeltaTime, 0, 0 * Speed * Time.fixedDeltaTime);
				}
				else
				{
					direction = new Vector3(0 * Speed * Time.fixedDeltaTime, 0, 1 * Speed * Time.fixedDeltaTime);
				}
			}
			isLeftMove = !isLeftMove;

			if(GameController.gameState == GameController.GameState.Start)
				GameController.gameState = GameController.GameState.Play;
		}

		if(GameController.gameState == GameController.GameState.Play)
		{
			// Плавное перемещение персонажа в новую позицию
			controller.Move(direction);

			if (!controller.isGrounded)
			{
				controller.Move(Vector3.down * Gravity * Time.fixedDeltaTime);
			}
		}

    }

	/// <summary>
	/// Устанавливает игрока в начальную позицию
	/// </summary>
	public void SetDefPosition()
	{
		this.transform.position = new Vector3(0f,0.8f,0f);
	}
}
