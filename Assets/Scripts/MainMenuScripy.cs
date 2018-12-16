using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScripy : MonoBehaviour {
    public static float EL=1.0f;
    public static float ML=0.0f;
    public static float SL=1.0f;

    public float musicLevel= ML;
    public float speechLevel=SL;
    public float effectsLevel=EL;
  
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame() {
        Application.Quit();
    }

    public void MusicLevel(float Single)
    {
        ML = Single;
    }
    public void SpeechLevel(float Single)
    {
        SL = Single;

    }
    public void EffectsLevel(float Single)
    {
        EL = Single;

    }

}
