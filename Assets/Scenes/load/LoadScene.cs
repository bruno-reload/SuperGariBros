using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public Slider slider;

    private AsyncOperation async;

    public void load(int lvl)
    {
        StartCoroutine(barLoad(lvl));
    }

    private IEnumerator barLoad(int lvl)
    {
        async = SceneManager.LoadSceneAsync(lvl);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1.0f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
