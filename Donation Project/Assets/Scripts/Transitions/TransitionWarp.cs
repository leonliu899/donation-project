using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWarp : MonoBehaviour
{
    public string defaultTransition;
    public TransitionsIndex[] transitions;

    void Start()
    {
        if(!(string.IsNullOrEmpty(defaultTransition) || string.IsNullOrWhiteSpace(defaultTransition)))
        {
            TransitionToIndex(defaultTransition);
        }
    }

    public void TransitionToIndex(string name)
    {
        foreach(TransitionsIndex index in transitions)
        {
            if(index.transitionName.Equals(name))
            {
                foreach(GameObject disable in index.camsToDisable)
                {
                    disable.SetActive(false);
                }
                index.camToEnable.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class TransitionsIndex
{
    public string transitionName;
    public GameObject[] camsToDisable;
    public GameObject camToEnable;
}
