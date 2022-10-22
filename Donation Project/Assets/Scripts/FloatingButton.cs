using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FloatingButton : MonoBehaviour
{
    public Transform target;
    public float followSpeed;
    public float showSpeed;

    [Space(15)]
    public float hiddenYLevel = -500;

    Vector3 initScale;
    float initY;

    void Start()
    {
        initY = transform.position.y;
        initScale = transform.localScale;

        transform.position = new Vector3(transform.position.x, hiddenYLevel, transform.position.z);
        transform.localScale = Vector3.zero;

        StartCoroutine(Show());
    }

    void Update()
    {
        if(CityRise.Instance.risen)
        {
            StopCoroutine(Show());
            transform.position = new Vector3(transform.position.x, initY, transform.position.z);
            transform.localScale = initScale;
        }

        transform.LookAt(followSpeed * transform.position - target.position);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);
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
}
