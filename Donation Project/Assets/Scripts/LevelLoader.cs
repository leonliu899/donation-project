using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [Header("UI")]
    public Image loadingBar;
    public float loadSpeed;

    [Space(20)]
    public int indexToLoad;

    void Awake()
    {
        StartCoroutine(DoLoadingAnimation());
        
    }

    void Start()
    {
        LoadScene(indexToLoad);
    }

    public async void LoadScene(int index)
    {
        var scene = SceneManager.LoadSceneAsync(index);
        scene.allowSceneActivation = false;

        do 
        {
        } while (scene.progress < 0.9f);


        scene.allowSceneActivation = true;
    }

    IEnumerator DoLoadingAnimation()
    {
        if(loadingBar != null)
        {
            loadingBar.fillAmount = 0;
            if(loadingBar.fillMethod == Image.FillMethod.Radial360)
            {
                while(loadingBar.fillAmount < 0.95f)
                {
                    loadingBar.fillAmount = Mathf.MoveTowards(loadingBar.fillAmount, 1, Time.deltaTime * loadSpeed);
                    yield return new WaitForSeconds(0.01f);
                }
                
                if(loadingBar.fillAmount >= 0.95f)
                    loadingBar.fillAmount = 1;

                yield return new WaitForSeconds(1);
                loadingBar.fillClockwise = !loadingBar.fillClockwise;

                while(loadingBar.fillAmount > 0.05f)
                {
                    loadingBar.fillAmount = Mathf.MoveTowards(loadingBar.fillAmount, 0, Time.deltaTime * loadSpeed);
                    yield return new WaitForSeconds(0.01f);
                }
                
                if(loadingBar.fillAmount <= 0.05f)
                    loadingBar.fillAmount = 0;

                yield return new WaitForSeconds(1);
                loadingBar.fillClockwise = !loadingBar.fillClockwise;
                StartCoroutine(DoLoadingAnimation());
            }
        }
    }
}
