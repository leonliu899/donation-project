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
    public GameObject readyText;
    public GameObject waitLoadingText;
    public float loadSpeed;

    [Space(20)]
    public int indexToLoad;
    public bool allowLoadScene;

    AsyncOperation scene;

    void Awake()
    {
        StartCoroutine(DoLoadingAnimation());
        
    }

    void Start()
    {
        readyText.SetActive(false);
        waitLoadingText.SetActive(false);
        loadingBar.gameObject.SetActive(true);
        LoadScene(indexToLoad);
    }

    public async void LoadScene(int index)
    {
        scene = SceneManager.LoadSceneAsync(index);
        loadingBar.gameObject.SetActive(true);
        scene.allowSceneActivation = false;

        do 
        {
            await Task.Delay(1);
        } while (scene.progress < 0.9f);


        allowLoadScene = true;
        loadingBar.gameObject.SetActive(false);
        readyText.SetActive(true);
    }

    public void LoadReadyScene()
    {
        if(allowLoadScene)
            scene.allowSceneActivation = true;
        else
            StartCoroutine(WaitLoadText());
    }

    IEnumerator WaitLoadText()
    {
        waitLoadingText.SetActive(true);
        yield return new WaitForSeconds(2);
        waitLoadingText.SetActive(false);
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
