using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakPlatManager : MonoBehaviour {

	public static breakPlatManager Instance = null;

	[SerializeField]
	GameObject platformPrefab;

	void Awake()
	{
		if (Instance == null) 
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);
	}

	IEnumerator SpawnPlatform(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (2f);
		Instantiate (platformPrefab, spawnPosition, platformPrefab.transform.rotation);
	}

}
