using UnityEngine;
using System.Collections;

public class ThrowStarRandomly : MonoBehaviour {
	public GameObject star;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "playerBullet") 
		{
			float randomValue=Random.Range(0.0f,100.0f);
//			print(randomValue);
			if(randomValue>65.0f)
			{

				Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
				Instantiate(star,transform.position,randomRotation);
			}
		}
	}
}
