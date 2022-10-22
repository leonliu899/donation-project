using System.Collections;
using System.Collections.Generic;
using Krivodeling.UI.Effects;
using UnityEngine;

public class CityRise : MonoBehaviour
{
    public float speed;
    public Vector2 yRange;

    [Space(20)]
    public UIBlur blurScript;
    public float blurSpeed;

    [Space(20)]
    public bool risen;
    Color white = Color.white;

    public static CityRise Instance;

    void Awake()
    {
        Instance = this;
        transform.position = new Vector3(transform.position.x, yRange.x, transform.position.z);
    }
    
    void Update()
    {
        if(transform.position.y >= yRange.y && !risen)
            transform.position = new Vector3(transform.position.x, yRange.y, transform.position.z);
        if(transform.position.y < yRange.y && !risen)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        } else 
        {
            risen = true;
        }

        if(risen)
        {
            this.enabled = false;
            blurScript.Color = white;
            blurScript.Intensity = 0;
            blurScript.enabled = false;
        }
        else
        {
            var targetColor = Color.Lerp(blurScript.Color, white, Time.deltaTime * blurSpeed);
            var targetIntensity = Mathf.Lerp(blurScript.Intensity, 0, Time.deltaTime * blurSpeed);
            blurScript.Color = targetColor;
            blurScript.Intensity = targetIntensity;
        }
    }
}
