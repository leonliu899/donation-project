using System.Collections;
using System.Collections.Generic;
using Krivodeling.UI.Effects;
using UnityEngine;

public class CityRise : MonoBehaviour
{
    public float speed;
    public Vector2 yRange;
    public ReflectionProbe reflectionProbe;

    [Space(20)]
    public UIBlur blurScript;
    public float blurSpeed;

    [Space(20)]
    public bool risen;

    [Space(20)]
    [Header("Island Raise")]
    public Transform islandToRaise;
    public float yRaiseLevel;
    public float raiseSpeed;

    Color white = Color.white;
    float islandInitY;
    bool toggleRaise;

    public static CityRise Instance;

    void Awake()
    {
        Instance = this;
        transform.position = new Vector3(transform.position.x, yRange.x, transform.position.z);
    }

    void Start()
    {
        blurScript.gameObject.SetActive(true);
        if(islandToRaise != null)
            islandInitY = islandToRaise.position.y;
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
                var targetColor = Color.Lerp(blurScript.Color, white, Time.deltaTime * blurSpeed);
                var targetIntensity = Mathf.Lerp(blurScript.Intensity, 0, Time.deltaTime * blurSpeed);
                if(targetColor != null)
                    blurScript.Color = targetColor;
                blurScript.Intensity = targetIntensity;
            }
        }

        if(islandToRaise != null)
        {
        islandToRaise.position = toggleRaise? Vector3.Lerp(islandToRaise.position, 
                                   new Vector3(islandToRaise.position.x, yRaiseLevel, islandToRaise.position.z), Time.deltaTime * raiseSpeed) :
                                   Vector3.Lerp(islandToRaise.position, 
                                   new Vector3(islandToRaise.position.x, islandInitY, islandToRaise.position.z), Time.deltaTime * raiseSpeed);
        }
    }

    public void ToggleIslandRaise()
    {
        toggleRaise = !toggleRaise;
    }
}
