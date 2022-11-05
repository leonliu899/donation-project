using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForever : MonoBehaviour
{
	public float rotateSpeed;
	void Update()
	{
		if(CityRise.Instance.toggleMiniRaise)
		{
			Vector3 position = transform.GetComponent<Renderer>().bounds.center;

			transform.RotateAround(position, Vector3.up, rotateSpeed * Time.deltaTime);
		}
			
			
	}
}
