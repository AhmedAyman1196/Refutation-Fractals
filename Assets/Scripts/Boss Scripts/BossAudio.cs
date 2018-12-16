using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour {
    bool once ;
    public AudioSource walkingAudio;
    public AudioSource detectAudio;
    public AudioSource hitAudio;
    public AudioSource dyingAudio;

  void Start () {
    once  = false;
	}

	 public void PlayDetect()  {
     if(!detectAudio.isPlaying && !once)
        detectAudio.Play();
        once = true ;
    }

    public  void PlayHit()  {
      if(!hitAudio.isPlaying)
        hitAudio.Play();
    }

    public  void PlayWalking()  {
      if(!walkingAudio.isPlaying)
      walkingAudio.Play();
    }

    public void PlayDying()  {
      if(!dyingAudio.isPlaying)
        dyingAudio.Play();
    }
}
