using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public string[] AllAudioName;
    public AudioClip[] AllAudioClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PlaySound(AudioSource source, string clipName)
    {
        AudioClip clip = GetAudioByName(clipName);
        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("CAN'T FIND CLIP : " + clipName);
        }
    }

    private AudioClip GetAudioByName(string clipName)
    {
        for (int i = 0; i < AllAudioName.Length; i++)
        {
            if (AllAudioName[i] == clipName)
            {
                return AllAudioClip[i];
            }
        }
        return null;
    }
}
