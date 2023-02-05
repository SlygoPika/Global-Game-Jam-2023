using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public float fadeLength;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
        }
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        s.source.Play();
    }

    public void PlayOnLoop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        s.source.loop = true;
        s.source.Play();
    }

    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        s.source.Stop();
    }

    public void FadeOut(Sound s, float time)
    {
        time += Time.deltaTime;
        if (time < fadeLength)
        {
            s.source.volume = 1 - time / fadeLength;
            FadeOut(s, time);
            return;
        }
        s.source.Stop();
    }
}
