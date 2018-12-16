using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreanaMusicScript : MonoBehaviour {
    public GameObject SoundControlObject;
    // Use this for initialization
    void Start () {
        GetComponent<AudioSource>().volume = SoundControlObject.GetComponent<MainMenuScripy>().musicLevel;
        Debug.Log(SoundControlObject.GetComponent<MainMenuScripy>().musicLevel);
        GetComponent<AudioSource>().Play();

    }

}
