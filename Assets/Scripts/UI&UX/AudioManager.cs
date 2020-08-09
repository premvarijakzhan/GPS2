using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioTag
{ 

}

[System.Serializable]
public class AudioList
{
    public AudioClip clip;
    public AudioTag tag;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM = null;

    private AudioSource music;
    private AudioSource SFX;

    public List<AudioList> audioList;

    void Awake()
    {
        if (AM == null)
        {
            AM = this;
            DontDestroyOnLoad(this);
        }
        else if (AM != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        music = GetComponent<AudioSource>();
        SFX = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void SetMusicVolume(float vol)
    {
        music.volume = vol;
        PlayerPrefs.SetFloat("Music", vol);
    }

    public void SetSFXVolume(float vol)
    {
        SFX.volume = vol;
        PlayerPrefs.SetFloat("SFX", vol);
    }

    AudioClip FindAudioClip(AudioTag tag)
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].tag == tag)
                return audioList[i].clip;
        }

        return null;
    }

    public void PlaySFX(AudioTag tag)
    {
        SFX.PlayOneShot(FindAudioClip(tag));
    }

    public void StopAudio()
    {
        SFX.Stop();
    }
}
