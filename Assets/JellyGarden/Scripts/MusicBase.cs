﻿using UnityEngine;
using System.Collections;

public class MusicBase : MonoBehaviour {

    public static MusicBase Instance;
    public AudioClip[] music;

    ///MusicBase.Instance.audio.PlayOneShot(MusicBase.Instance.music[0]);


    // Use this for initialization
    void Awake()
    {
        if (transform.parent == null)
        {
            transform.parent = Camera.main.transform;
            transform.localPosition = Vector3.zero;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
