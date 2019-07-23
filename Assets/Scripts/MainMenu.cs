using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private float volume;

    public void  StartScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }

    public void UpdateVolume(Slider slider)
    {
        this.volume = slider.value;
    }
}
