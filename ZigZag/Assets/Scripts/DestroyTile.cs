//Уничтожаем блокк после соприкосновения если игрок удалился

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
	private GameObject Player;
	public float SpeedDestroy = 100f;

	private bool isDestroyed = false;

	private void Awake()
	{
		Player = GameObject.Find("Player");
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			isDestroyed = true;
		}
	}

	private void Update()
	{
		if(isDestroyed)
		{
			float speedDestroy = SpeedDestroy * Time.fixedDeltaTime;
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - speedDestroy, this.transform.position.z);
			if(this.transform.position.y < -3)
				Destroy(this.gameObject);
		}

		//Простая защита от остатков кубов в памяти
		if(Vector3.Distance(Player.transform.position, this.transform.position) > 20f)
		{
			Destroy(this.gameObject);
		}
	}
}
