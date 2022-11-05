using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine;

public class CityCameraManager : MonoBehaviour
{
	public CinemachineVirtualCamera trackerCam;
	public CinemachineSmoothPath paths;

	[Space(20)]
	public float rotationSpeed;
	public float rotationSensitivity;
	[Space(15)]
	public RotationType rotationType;
	public bool clockwiseRotation;
	public bool allowRotate;

	public enum RotationType { FullySelfRotate, FullyUserControl, FullAutomated }

	CinemachineTrackedDolly dolly;
	int pathAmt;
	float currentPath;
	float timeRotation;

	void Start()
	{
		dolly = trackerCam.GetCinemachineComponent<CinemachineTrackedDolly>();
		pathAmt = paths.m_Waypoints.Length;

		currentPath = 0;
		dolly.m_PathPosition = currentPath;
	}

	void Update()
	{
		if(allowRotate && CityRise.Instance.allowCityRise) UpdateCityRotation();

	}

	void UpdateCityRotation()
	{
		timeRotation = clockwiseRotation? Time.deltaTime : Time.deltaTime * -1;

		if(rotationType == RotationType.FullySelfRotate)
		   SelfRotate();
		else if(rotationType == RotationType.FullyUserControl)
		{
			if(Input.GetMouseButton(0))
				UserControlRotate();
		} 
		else 
		{
			if(Input.GetMouseButton(0))
				UserControlRotate();
			else 
				SelfRotate();
		}
		dolly.m_PathPosition = currentPath;
	}

	void SelfRotate()
	{
		 currentPath += rotationSpeed * timeRotation;
	}
	void UserControlRotate()
	{
		currentPath += rotationSensitivity * Input.GetAxis("Mouse X");
	}
}
