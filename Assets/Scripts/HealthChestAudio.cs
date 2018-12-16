using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAudio : MonoBehaviour {

    private AudioSource collectAudio;

    void Start () {
        collectAudio = GetComponent<AudioSource>();
	}
	
	void Collect()
    {
        collectAudio.Play();
    }
}
