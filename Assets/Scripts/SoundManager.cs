using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource effects;
    public AudioClip corteSound;
    public AudioClip dieSound;
    public AudioClip btnPlaySound;


    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip audio)
    {
        effects.clip = audio;
        effects.Play();
    }


}
