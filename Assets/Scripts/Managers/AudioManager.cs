﻿using UnityEngine;
using System.Collections;

public class AudioManager : SingletonComponent<AudioManager>
{
    private AudioSource source;
    private GameObject sound;
    public GameObject gameSoundManager;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        sound = Instantiate(gameSoundManager);
        source.Stop();
        DontDestroyOnLoad(sound);
    }

    public void EndGame()
    {
        Destroy(sound);
    }
}
