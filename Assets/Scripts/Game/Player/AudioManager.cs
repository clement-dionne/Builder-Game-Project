using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ClickSoundEffect;
    public AudioClip LevelUpSoundEffect;
    public AudioClip SellAndBuySoundEffect;
    public AudioClip BuildSoundEffect;
    public AudioClip GameAmbientMusic;

    public AudioSource SoundEffect;
    public AudioSource Ambient;

    void Start()
    {
        SoundEffect.clip = LevelUpSoundEffect;
        SoundEffect.volume = 1;

        SoundEffect.Play();

        Ambient.clip = GameAmbientMusic;
        Ambient.Play();
    }

    public void BuySellEffect()
    {
        SoundEffect.pitch = 1f;
        SoundEffect.clip = SellAndBuySoundEffect;
        SoundEffect.Play();
    }

    public void ClickEffect()
    {
        SoundEffect.pitch = 1f;
        SoundEffect.clip = ClickSoundEffect;
        SoundEffect.Play();
    }

    public void LevelUpEffect()
    {
        SoundEffect.pitch = 1f;
        SoundEffect.clip = LevelUpSoundEffect;
        SoundEffect.Play();
    }

    public void BuildEffect()
    {
        SoundEffect.pitch = 1.5f;
        SoundEffect.clip = BuildSoundEffect;
        SoundEffect.Play();
    }
}
