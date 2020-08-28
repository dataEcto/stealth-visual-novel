using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found! Did you type it right?");
            return;
        }

        //Temp solution to fix the
        //bug in where after stopping the sound and trying to play it again, the values of volume and pitch are set weirdly
        //I set it to be 1, but eventually it should take the values from the volume slider.

        s.source.volume = 1;
        s.source.pitch = 1;
        s.source.Play();
        Debug.Log("Play Music");
        
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found! Can't stop it now.");
            return;
        }

        s.source.volume = 0;
        s.source.pitch = 0;



        s.source.Stop ();
        
        Debug.Log("Stop Music");
    }

    //A theory: This audio manager should stay in all scenes and gets activated when something in the game activates it.
    // The main thing to use is the fungus flowchart, as that can hold variables that we can check using this script.
    //Instead of using a public, maybe we could use the FindObjectOfType
    //And return null to save memory or something
    private void Start()
    {
        Play("Title");
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StopPlaying("Title");
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Play("Title");
        }
    }
}
