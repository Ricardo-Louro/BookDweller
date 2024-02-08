using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = System.Random;

public class PlayAudio : MonoBehaviour
{
    public bool randomizePitch = true;
    public float pitchRandomRange = 0.2f;
    
    [SerializeField] private AudioClip[] _audioClip;
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _isLoop;
    [SerializeField] private bool _playOnStart;

    private Random rnd;
    private void Start()
    {
        rnd = new Random();

        _audioSource.outputAudioMixerGroup = _audioMixer;
        
        ApplyLoop();
        if(_playOnStart) Play();
    }

    public void Play()
    {
        if (_audioClip.Length > 1) ChooseAVariant(); 
        else _audioSource.clip = _audioClip[0];
        
        _audioSource.pitch = randomizePitch ? UnityEngine.Random.Range(1.0f - pitchRandomRange, 1.0f + pitchRandomRange) : 1.0f;
        _audioSource.Play();
    }

    private void ChooseAVariant()
    {
        _audioSource.clip = _audioClip[rnd.Next(0, _audioClip.Length)];
    }

    private void ApplyLoop()
    {
        _audioSource.loop = _isLoop;
    }
}
