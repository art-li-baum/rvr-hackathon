using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{
    public static ClipManager Instance;

    private AudioClip currentQuote;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    

    public void PlayQuote(AudioClip clip)
    {
        if (clip == currentQuote) return;

        currentQuote = clip;
        AudioManager.instance.PlayTriggerAudio(clip);
    }
}
