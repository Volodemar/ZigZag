using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
	private GameController GameController;
		
	private void Awake()
	{
		GameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void Update()
    {
        if(this.transform.position.y < -5)
			GameController.OnGameOver();
    }
}
