using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FloatingButton : MonoBehaviour
{
	[Header("Bubble Camera Follow")]
	public Transform cameraBrain;
	public float followSpeed;

	[Space(20)]
	[Header("City Rise Animation")]
	public float showSpeed;
	public float hiddenYLevel = -500;

	[Space(20)]
	[Header("Button Properties")]
	public Color normal;
	public Color pressed;
	public Color hover;
	public Renderer baseButton;
	public UnityEvent onPress;
	[Header("Button Animation")]
	public float scaleUpHover;
	public float scaleUpSpeed;

	Vector3 initScale;
	float initY;
	float initMouseEnterScale;
	bool hovering;

	bool hasShown;
	void Start()
	{
		initScale = transform.localScale;
		initMouseEnterScale = transform.localScale.x;
		initY = transform.position.y;

		baseButton.material.SetColor( "_EmissionColor", normal );

		transform.position = new Vector3(transform.position.x, hiddenYLevel, transform.position.z);
		transform.localScale = Vector3.zero;

	}

	void Update()
	{
		if(!hasShown && CityRise.Instance.allowCityRise)
		{
			StartCoroutine(Show());
			hasShown = true;
		}
		
		if(CityRise.Instance.risen)
		{
			StopCoroutine(Show());

			transform.localScale = hovering? Vector3.Lerp(transform.localScale, 
										 new Vector3(initMouseEnterScale * scaleUpHover, initMouseEnterScale * scaleUpHover, initMouseEnterScale * scaleUpHover),
										 Time.deltaTime * scaleUpSpeed) :
										 Vector3.Lerp(transform.localScale, 
										 new Vector3(initMouseEnterScale, initMouseEnterScale, initMouseEnterScale),
										 Time.deltaTime * scaleUpSpeed);
		}

		

		transform.LookAt(new Vector3(cameraBrain.position.x, -1*(transform.position.y - cameraBrain.position.y), cameraBrain.position.z));
	}

	IEnumerator Show()
	{
		while(transform.position.y < initY)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, initY, transform.position.z)
											, Time.deltaTime * showSpeed);
			transform.localScale = Vector3.Lerp(transform.localScale, initScale, Time.deltaTime * showSpeed);

			yield return new WaitForSeconds(0.01f);
		}
	}

	void OnMouseDown()
	{
		baseButton.material.SetColor( "_EmissionColor", pressed );
		onPress.Invoke();
		hovering = false;
	}
	
	void OnMouseUp()
	{
		baseButton.material.SetColor( "_EmissionColor", hover );
		hovering = true;
	}

	void OnMouseEnter()
	{
		baseButton.material.SetColor( "_EmissionColor", hover );
		initMouseEnterScale = transform.localScale.x;
		hovering = true;
	}
	
	void OnMouseExit()
	{
		baseButton.material.SetColor( "_EmissionColor", normal );
		hovering = false;
	}
}
