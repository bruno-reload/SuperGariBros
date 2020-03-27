using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class ButtonControll : MonoBehaviour
{
    public PostProcessVolume post;
    private bool paused = true;
    public void quit()
    {
        Application.Quit();
    }
    public void pause()
    {
        switch (paused)
        {
            case true:
                post.profile.GetSetting<ColorGrading>().saturation.value = -100.0f;
                Time.timeScale = 0;
                paused = false;
                break;
            case false:
                post.profile.GetSetting<ColorGrading>().saturation.value = 0.0f;
                Time.timeScale = 1.0f;
                paused = true;
                break;
        }
    }
    public void resetLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
        SpeedControl.speed = SpeedControl.defaultValue;
    }
}
