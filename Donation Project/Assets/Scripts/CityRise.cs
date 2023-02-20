using DG.Tweening;
using Krivodeling.UI.Effects;
using NaughtyAttributes;
using UnityEngine;

public class CityRise : MonoBehaviour
{
	[BoxGroup("Objects")][Required][SerializeField] Transform dynamicCity;
	[BoxGroup("Objects")][Required][SerializeField] Transform staticCity;
	[BoxGroup("Objects")][Required][SerializeField] CityCameraManager camMgr;
	
	[BoxGroup("Properties")][MinValue(0)] public float riseDuration;
	[BoxGroup("Properties")] public Vector2 cityRiseYRange;
	
	[BoxGroup("Transition")][MinValue(0)][SerializeField] float blurOffDuration;

	[BoxGroup("Debug")][ReadOnly] public bool risen;
	[BoxGroup("Debug")] public bool allowCityRise;

	public static CityRise Instance;

	void Awake()
	{
		Instance = this;
		risen = false;

	}

	void Start()
	{
		staticCity.gameObject.SetActive(false);
		dynamicCity.gameObject.SetActive(true);

		ResetCity();
	}
	
	public void RaiseCity()
	{
		if(allowCityRise && risen == false)
		{
			ResetCity();
			camMgr.allowRotate = true;
			GameManager.Instance.ClearMusic();
			dynamicCity.DOLocalMoveY(cityRiseYRange.y, riseDuration).SetEase(Ease.InOutSine).OnComplete(() => 
			{ 
				risen = true;  
				dynamicCity.gameObject.SetActive(false);
				staticCity.gameObject.SetActive(true);
			});
		}
	}

	public void RiseDown()
	{
		dynamicCity.gameObject.SetActive(true);
		staticCity.gameObject.SetActive(false);
		dynamicCity.DOLocalMoveY(cityRiseYRange.x, riseDuration).SetEase(Ease.InOutSine).OnComplete(() => 
		{ 
			risen = false;  
			CloudManager.Instance.Restart();
		});
	}

	public void ResetCity()
	{
		dynamicCity.localPosition = new Vector3(dynamicCity.localPosition.x, cityRiseYRange.x, dynamicCity.localPosition.z);
	} 
}
