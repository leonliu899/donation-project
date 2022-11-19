using Krivodeling.UI.Effects;
using NaughtyAttributes;
using UnityEngine;

public class CityRise : MonoBehaviour
{
	[BoxGroup("Objects")][Required][SerializeField] Transform city;
	[BoxGroup("Objects")][Required][SerializeField] ReflectionProbe reflectionProbe;
	
	[BoxGroup("Properties")][MinValue(0)] public float speed;
	[BoxGroup("Properties")] public Vector2 cityRiseYRange;
	
	[BoxGroup("Blur")][Required][SerializeField] UIBlur blurScript;
	[BoxGroup("Blur")][MinValue(0)][SerializeField] float blurTransitionSpeed;

	[BoxGroup("Debug")][ReadOnly] public bool risen;
	[BoxGroup("Debug")] public bool allowCityRise;

	[BoxGroup("Mini-Island")][Required][SerializeField] Transform miniIsland;
	[BoxGroup("Mini-Island")][SerializeField] float miniIslandYRaise;
	[BoxGroup("Mini-Island")][SerializeField][MinValue(0)] float miniIslandRaiseSpeed;

	Color white = Color.white;
	float miniIslandInitY;
	[HideInInspector] public bool toggleMiniRaise;

	public static CityRise Instance;

	void Awake()
	{
		Instance = this;

		risen = false;
		allowCityRise = false;
		// Put city in the ground
		city.position = new Vector3(city.position.x, cityRiseYRange.x, city.position.z);
		miniIsland.position = new Vector3(miniIsland.position.x, -100f, miniIsland.position.z);
	}

	void Start()
	{
		blurScript.gameObject.SetActive(true);
		miniIslandInitY = miniIsland.position.y;
	}
	
	void Update()
	{
		if(allowCityRise)
		{
			RaiseCity();
		}
	}
	
	void RaiseCity()
	{
		city.position = Vector3.MoveTowards(city.position, 
						new Vector3(city.position.x, cityRiseYRange.y, city.position.z),
						speed * Time.deltaTime); // Time.deltaTime --> over time

		if(city.position.y >= cityRiseYRange.y && risen)
			city.position = new Vector3(city.position.x, cityRiseYRange.y, city.position.z);


		if(risen)
		{
			if(blurScript != null)
			{
				blurScript.Color = white;
				blurScript.Intensity = 0;
				blurScript.enabled = false;

				reflectionProbe.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.ViaScripting;
			}
		}
		else
		{
			if(blurScript != null)
			{
				var targetColor = Color.Lerp(blurScript.Color, white, Time.deltaTime * blurTransitionSpeed);
				var targetIntensity = Mathf.Lerp(blurScript.Intensity, 0, Time.deltaTime * blurTransitionSpeed);
				if(targetColor != null)
					blurScript.Color = targetColor;
				blurScript.Intensity = targetIntensity;
			}
		}


		miniIsland.position = toggleMiniRaise? Vector3.Lerp(miniIsland.position, 
								   new Vector3(miniIsland.position.x, miniIslandYRaise, miniIsland.position.z), Time.deltaTime * miniIslandRaiseSpeed) :
								   Vector3.Lerp(miniIsland.position, 
								   new Vector3(miniIsland.position.x, miniIslandInitY, miniIsland.position.z), Time.deltaTime * miniIslandRaiseSpeed);
	}

	public void ToggleMiniIslandRaise()
	{
		toggleMiniRaise = !toggleMiniRaise;
	}
}
