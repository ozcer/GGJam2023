using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public List<int> audioClipMonsterIds;
    public AudioSource audioSource;

    Dictionary<int, List<AudioClip>> mapping;

    // Start is called before the first frame update
    void Start()
    {
        mapping = new Dictionary<int, List<AudioClip>>();
        for (int audioClipIndex = 0; audioClipIndex < audioClips.Count; audioClipIndex++) {
            int monsterId = audioClipMonsterIds[audioClipIndex];
            AudioClip audioClip = audioClips[audioClipIndex];
            if (!mapping.ContainsKey(monsterId)) {
                mapping.Add(monsterId, new List<AudioClip>() {audioClip});
            } else {
                mapping[monsterId].Add(audioClip);
            }
        }
    }

    public void playEndingMusic (int index){
        audioSource.Stop();
        List<AudioClip> validClips = mapping[index];
        AudioClip clip = validClips[Random.Range(0, validClips.Count)];
        audioSource.PlayOneShot(clip, 0.5f);
    }
}
