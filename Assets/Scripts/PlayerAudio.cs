using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    public AudioSource walkingAudio;
    public AudioSource rageAudio;
    public AudioSource hitAudio;
    public AudioSource dyingAudio;

    void Start () {
        AudioSource [] audioz = GetComponents<AudioSource>();
        walkingAudio = audioz[0];
        rageAudio = audioz[1];
        hitAudio = audioz[2];
        dyingAudio = audioz[3];
	}

	public void PlayRage()
    {
      if(!rageAudio.isPlaying)
        rageAudio.Play();
    }

  public  void PlayHit()
    {
      if(!hitAudio.isPlaying)
        hitAudio.Play();
    }

  public  void PlayWalking()
    {
      if(!walkingAudio.isPlaying)
        walkingAudio.Play();
    }

  public  void PlayDying()
    {
      if(!dyingAudio.isPlaying)
        dyingAudio.Play();
    }
}
