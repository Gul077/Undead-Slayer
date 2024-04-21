using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip sliced;
    public AudioClip thunder;

    private void Start()
    {
        // Play background music
        musicSource.clip = background;
        musicSource.Play();

        // Play thunder sound effect
        SFXSource.clip = thunder;
        SFXSource.Play();
    }

    // Method to play the slicing sound effect
    public void PlaySlicedSound()
    {
        SFXSource.PlayOneShot(sliced);
    }
}
