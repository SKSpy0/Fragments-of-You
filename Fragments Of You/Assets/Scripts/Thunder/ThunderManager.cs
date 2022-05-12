using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThunderManager : MonoBehaviour
{
	public float frequencyLow = 3f;
	public float frequencyHigh = 7f;
	public float clusterFrequencyLow = 0.1f;
	public float clusterFrequencyHigh = 0.3f;
	[Range(0f, 8f)]
	public float searchRange = 8f;
	[Range(0f, 20f)]
	public float searchDegreeOffset = 5f;
	public int maxClusterSize = 4;
	public GameObject[] thunderObjects;
	private GameObject thunderObject;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(ThunderCycle());
	}

    // Update is called once per frame
    void Update()
    {
       
    }

	void FixedUpdate()
	{
		//CastThunder();
	}

	IEnumerator ThunderCycle()
    {
		Debug.Log("New Thunder Cycle");
		yield return new WaitForSeconds(Random.Range(frequencyLow, frequencyHigh));
		int clusterSize = Random.Range(1, maxClusterSize);
		for (int i = 0; i < clusterSize; i++)
        {
			Debug.Log("Casting Thunder Strike " + i);
			CastThunder();
			yield return new WaitForSeconds(Random.Range(clusterFrequencyLow, clusterFrequencyHigh));
		}
		StartCoroutine(ThunderCycle());
	}

	void CastThunder()
    {
		float laserLength = 50f;
		Vector2 startPosition = (Vector2)transform.position + new Vector2(Random.Range(-searchRange, searchRange), 0f);
		int layerMask = LayerMask.GetMask("Ground");
		float randDegree = 270 + Random.Range(-searchDegreeOffset, searchDegreeOffset);

		RaycastHit2D hit = Physics2D.Raycast(startPosition, DegreeToVector2(randDegree), laserLength, layerMask, 0);

		//If the collider of the object hit is not NUll
		if (hit.collider != null)
		{
			Debug.Log("Valid Strike ");
			Debug.Log("Hitting " + hit.collider.tag + "– x:" + hit.point.x + "; y: " + hit.point.y);
			thunderObject = GetThunderObject();
			Instantiate(thunderObject, new Vector2(hit.point.x, hit.point.y + 24), Quaternion.identity);
		}

		//Method to draw the ray in scene for debug purpose
		Debug.DrawRay(startPosition, DegreeToVector2(randDegree) * laserLength, Color.red);
	}

	private GameObject GetThunderObject()
    {
		int arraySize = thunderObjects.Length;
		return thunderObjects[Random.Range(0, arraySize - 1)];
    }

	public static Vector2 RadianToVector2(float radian)
	{
		return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}


	public static Vector2 DegreeToVector2(float degree)
	{
		return RadianToVector2(degree * Mathf.Deg2Rad);
	}


}
