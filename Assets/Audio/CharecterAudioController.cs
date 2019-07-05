using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterAudioController : MonoBehaviour
{
    public AudioClip StepClip;
    public AudioClip JumpClip;
    public AudioClip LandClip;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (StepClip == null)
        {
            Debug.LogError("Audio clip for StepClip not provided");
        }
    }

    private void Step()
    {
        _audioSource.PlayOneShot(StepClip);
    }
}
