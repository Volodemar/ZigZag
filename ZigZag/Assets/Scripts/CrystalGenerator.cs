using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGenerator : MonoBehaviour
{
	public GameObject Crystal;
	public GameObject CrysralRed;

	private System.Random rnd = new System.Random();

	public Vector3 OnCreateCrystal(Vector3 CrystalPos)
	{
		if(rnd.Next(100) < 20)
		{
			Vector3 crystalPos = new Vector3(CrystalPos.x, 1, CrystalPos.z);
			GameObject newCrystal = Instantiate(Crystal, crystalPos, Quaternion.Euler(45f,45f,45f), this.transform);
			return newCrystal.transform.position;
		}
		else
		{
			return Vector3.zero;
		}
	}

	public Vector3 OnSetCrystalRed(GameObject crystal)
	{
		Vector3 crystalPos = new Vector3(crystal.transform.position.x, 1, crystal.transform.position.z);
		GameObject newCrystalRed = Instantiate(CrysralRed, crystalPos, Quaternion.Euler(45f,45f,45f), this.transform);
		Destroy(crystal);
		return newCrystalRed.transform.position;
	}
}
