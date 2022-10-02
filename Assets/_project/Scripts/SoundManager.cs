using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance {get; private set;}

    AudioSource _audioSource;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy (this);
        }

        else {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlaySoundEffect (AudioClip clip, float volume = 1f) {
        _audioSource.PlayOneShot(clip, volume);        
    }

}
