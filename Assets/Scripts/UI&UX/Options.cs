using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider musicSlider;
    public Slider SFXSlider;

    private int firstPlay;

    public void SetVolume()
    {
        firstPlay = PlayerPrefs.GetInt("FirstPlay");

        if (firstPlay == 0)
        {
            musicSlider.value = 0.5f;
            AudioManager.AM.SetMusicVolume(musicSlider.value);

            SFXSlider.value = 0.5f;
            AudioManager.AM.SetSFXVolume(SFXSlider.value);

            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("Music");
            AudioManager.AM.SetMusicVolume(PlayerPrefs.GetFloat("Music"));

            SFXSlider.value = PlayerPrefs.GetFloat("SFX");
            AudioManager.AM.SetSFXVolume(PlayerPrefs.GetFloat("SFX"));
        }
    }

    public void SetMusicVolume()
    {
        AudioManager.AM.SetMusicVolume(musicSlider.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.AM.SetSFXVolume(SFXSlider.value);
    }
}
