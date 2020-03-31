using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{
    AudioSource audio;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ToggleMute()
    {
        audio.mute = !audio.mute;
    }
}
