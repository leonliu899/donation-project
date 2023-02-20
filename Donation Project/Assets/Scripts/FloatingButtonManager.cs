using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingButtonManager : MonoBehaviour
{
    public bool windowIsOpen;
    public FloatingButton donation;
    public FloatingButton home;
    public FloatingButton stats;
    public FloatingButton auth;

    public static FloatingButtonManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        windowIsOpen = donation.window.activeSelf || home.window.activeSelf || stats.window.activeSelf | auth.window.activeSelf;
    }
}
