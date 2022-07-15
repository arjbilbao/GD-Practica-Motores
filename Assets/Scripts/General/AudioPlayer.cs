using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource _audio;

    public void AudioPlay(){

        _audio.Play();
    }
}
