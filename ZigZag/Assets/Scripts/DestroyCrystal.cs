using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrystal : MonoBehaviour
{
	private GameObject Player;
	private GameController GameController;
	private CrystalGenerator CrystalGenerator;
	private bool isDestroyed = false;
	private bool isMassed = false;
	private float Scaler;

	private void Awake()
	{
		GameController = GameObject.Find("GameController").GetComponent<GameController>();
		CrystalGenerator = GameObject.Find("World").GetComponent<CrystalGenerator>();
		Player = GameController.Player;

		switch (GameController.Dificult)
		{
			case 1:
				Scaler = 3;
				break;
			case 2:
				Scaler = 2;
				break;
			case 3:
				Scaler = 1;
				break;
			default:
				Scaler = 1;
				break;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			//Добавляем очки игроку
			GameController.OnScoresAdd(1);
			isDestroyed = true;
		}
	}

	private void Update()
	{
		if(isDestroyed)
		{
			Destroy(this.gameObject);
		}

		//Вот захотелось добавить пометку упущенных кристаллов
		if(!isMassed)
		{
			if(Vector3.Distance(Player.transform.position, this.transform.position) < Scaler)
			{
				isMassed = true;
			}
		}
		else
		{
			if(Vector3.Distance(Player.transform.position, this.transform.position) > Scaler)
			{
				CrystalGenerator.OnSetCrystalRed(this.gameObject);
			}
		}

		//Простая защита от остатков кубов в памяти
		if(Vector3.Distance(Player.transform.position, this.transform.position) > 10f)
		{
			Destroy(this.gameObject);
		}
	}
}
