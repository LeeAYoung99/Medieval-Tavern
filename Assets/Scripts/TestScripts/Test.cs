using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{

    [SerializeField]
    Slider loadingSlider;

    private void Start()
    {
        loadingSlider.value = 0;
        StartCoroutine(LoadAsyncScene());
    }
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadAsyncScene()
    {
        yield return null;

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("Tavern_Inside");
        asyncScene.allowSceneActivation = false;
        float timeC = 0;
        while (!asyncScene.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;
            if (asyncScene.progress >= 0.9f)
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, 1, timeC);
                if (loadingSlider.value == 1.0f)
                {
                    asyncScene.allowSceneActivation = true;

                }
            }
            else
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, asyncScene.progress, timeC);

                if (loadingSlider.value >= asyncScene.progress)
                {
                    timeC = 0f;
                }

            }
            
        }
    }
}