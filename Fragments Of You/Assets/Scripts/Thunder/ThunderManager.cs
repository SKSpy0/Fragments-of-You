using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThunderManager : MonoBehaviour
{
	public float frequencyLow = 3f;
	public float frequencyHigh = 7f;
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
		yield return new WaitForSeconds(Random.Range(3f, 7f));
		CastThunder();
		yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
		CastThunder();
		yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
		CastThunder();
		yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
		CastThunder();
		StartCoroutine(ThunderCycle());

	}

	void CastThunder()
    {
		float laserLength = 50f;
		Vector2 startPosition = (Vector2)transform.position + new Vector2(Random.Range(-8f, 8f), 0f);
		int layerMask = LayerMask.GetMask("Ground");
		float randDegree = 270 + Random.Range(-10f, 10f);

		RaycastHit2D hit = Physics2D.Raycast(startPosition, DegreeToVector2(randDegree), laserLength, layerMask, 0);

		//If the collider of the object hit is not NUll
		if (hit.collider != null)
		{
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
