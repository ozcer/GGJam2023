using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
/*       AudioClip clip = audioClips[0];
      audioSource.Play();
      audioSource.clip = clip;  */ 
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
    public void playEndingMusic (int index){
        audioSource.Stop();
        AudioClip clip = audioClips[index];
        audioSource.PlayOneShot(clip, 0.5f);
    }
}
