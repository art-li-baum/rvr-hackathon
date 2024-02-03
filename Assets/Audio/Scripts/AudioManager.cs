using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]private AudioSource triggerdAudio;
    [SerializeField]private AudioSource backgroundAudio;
    [SerializeField]private AudioMixer audioMixer;

    [SerializeField, Range(-80, 0)] private float ambienceVolume = -40; 

    [SerializeField] private AudioClip calmBackground;

    Coroutine audioMixerRetreat;//Stors the courotine so we can stop it

    private void Start()
    {   
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        //Starts the backgorund audio
        PlayBackgroundAudio(calmBackground);
    }
    /// <summary>
    /// Plays any audio like dialouge 
    /// </summary>
    /// <param name="clip"></param>
    public void PlayTriggerAudio(AudioClip clip)
    {
        
        if(triggerdAudio.isPlaying) 
        {   //Stops the audio so there is now overlapping
            triggerdAudio.Stop();
        }
        if(audioMixerRetreat != null)
        {   //Resets the timer
            StopCoroutine(audioMixerRetreat); 
        }
        if (triggerdAudio.spatialize)
        {
            triggerdAudio.spatialize = false;
        }
        triggerdAudio.clip = clip;
        triggerdAudio.Play();
        audioMixer.SetFloat("AmbienceVolume", ambienceVolume);

        //Starts the timer that resets the audio 
        audioMixerRetreat = StartCoroutine(AudioMixerRetreat(clip.length));

    }
    public void PlayTriggerAudioSpatial(AudioClip clip, Transform tr, float range)
    {
        if (triggerdAudio.isPlaying)
        {   //Stops the audio so there is now overlapping
            triggerdAudio.Stop();
        }
        if (audioMixerRetreat != null)
        {   //Resets the timer
            StopCoroutine(audioMixerRetreat);
        }
        if (triggerdAudio.spatialize)
        {
            triggerdAudio.spatialize = true;
        }
        triggerdAudio.transform.position = tr.position; //Sets the position
        triggerdAudio.spatialBlend = range; //Sets the range
        triggerdAudio.clip = clip;
        triggerdAudio.Play();
        audioMixer.SetFloat("AmbienceVolume", ambienceVolume);

        //Starts the timer that resets the audio 
        audioMixerRetreat = StartCoroutine(AudioMixerRetreat(clip.length));

    }
    /// <summary>
    /// In case tou want to add any sort of extra noise
    /// </summary>
    /// <param name="clip"></param>
    public void PlayOneShot(AudioClip clip)
    {
        triggerdAudio.PlayOneShot(clip);
    }
    /// <summary>
    /// Plays the backgorund audio
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBackgroundAudio(AudioClip clip) 
    { 
        backgroundAudio.clip = clip;
        backgroundAudio.loop = true;
        backgroundAudio.Play();
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// Resets ambience after no sounds are playing
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator AudioMixerRetreat(float time)
    {
        yield return new WaitForSeconds(time);
        audioMixer.SetFloat("AmbienceVolume", 0);
    }
}
