using System.Collections;
using DG.Tweening;
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
	public Renderer baseButton;
	public UnityEvent onPress;
	public GameObject window;
	[Header("Button Animation")]
	public float scaleUpHover;
	public float scaleUpSpeed;

	Vector3 initScale;
	float initY;
	bool hovering;

	bool hasShown;
	void Start()
	{
		Init();
	}

	void Init()
	{
		initScale = transform.localScale;
		initY = transform.position.y;

		transform.position = new Vector3(transform.position.x, hiddenYLevel, transform.position.z);
		transform.localScale = Vector3.zero;
	}

	public void RiseUp()
	{
		if(!hasShown)
		{
			transform.DOMoveY(initY, showSpeed).SetEase(Ease.InOutSine);
			transform.DOScale(initScale, showSpeed).SetEase(Ease.OutSine);
			hasShown = true;
		}
	}

	void Update()
	{
		LookAtCamera();
	}

	void LookAtCamera()
	{
		Vector3 direction = cameraBrain.position - transform.position;
        direction.y = 0;
        transform.forward = direction.normalized;
        transform.Rotate(0, Time.deltaTime * 100, 0);
	}

	void OnMouseDown()
	{
		if(!FloatingButtonManager.Instance.windowIsOpen)
			onPress.Invoke();
	}
	
	void OnMouseUp()
	{			
		transform.DOScale(initScale*scaleUpHover, scaleUpSpeed).SetEase(Ease.InSine);
	}

	void OnMouseEnter()
	{
		if(!FloatingButtonManager.Instance.windowIsOpen)
		{
			transform.DOScale(initScale*scaleUpHover, scaleUpSpeed).SetEase(Ease.InSine);
		}
	}
	
	void OnMouseExit()
	{
		transform.DOScale(initScale, scaleUpSpeed).SetEase(Ease.OutSine);
	}
}
