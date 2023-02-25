using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Required][SerializeField] AudioSource musicSource;
    [SerializeField] float musicTransitionDuration;

    public static GameManager Instance;

    bool showSettings;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MuffleMusic();
    }

    public void ToggleSettings()
    {
        showSettings = !showSettings;
    }

    void OnGUI()
    {
        string[] names = QualitySettings.names;
        if(showSettings)
        {
            GUILayout.BeginVertical();
            for (int i = 0; i < names.Length; i++)
            {
                if (GUILayout.Button(names[i]))
                {
                    QualitySettings.SetQualityLevel(i, true);
                }
            }
            GUILayout.EndVertical();
        }
    }

    public void MuffleMusic()
    {
        musicSource.GetComponent<AudioReverbFilter>().dryLevel = -10000;
        musicSource.GetComponent<AudioReverbFilter>().decayHFRatio = 0.1f;
    }
    public void ClearMusic()
    {
        DOVirtual.Float(musicSource.GetComponent<AudioReverbFilter>().dryLevel, 0, musicTransitionDuration, x =>
        {
            musicSource.GetComponent<AudioReverbFilter>().dryLevel = x;
        }).SetEase(Ease.Linear);

        DOVirtual.Float(musicSource.GetComponent<AudioReverbFilter>().decayHFRatio, 0.5f, musicTransitionDuration, x =>
        {
            musicSource.GetComponent<AudioReverbFilter>().decayHFRatio = x;
        }).SetEase(Ease.Linear);
    }
}
