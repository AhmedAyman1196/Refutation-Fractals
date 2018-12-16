using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuz : MonoBehaviour
{

    private Boolean show;

    void Start()
    {
        show = false;
        gameObject.SetActive(show);
    }

    public void ToggleMenu()
    {
        show = !show;
        gameObject.SetActive(show);
    }

    public void Play()
    {
        SceneManager.LoadScene("Grav");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1 - Time.timeScale;
    }

}