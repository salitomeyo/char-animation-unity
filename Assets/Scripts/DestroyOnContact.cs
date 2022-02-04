using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Audio source que va a reproducir el sonido")]
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("Audio clip que se va a reproducir")]
    private AudioClip audioClip;
    private void OnTriggerEnter(Collider other) {
        gameObject.SetActive(false);
        // audioSource.Play(audioClip);
        // audioSource.PlayOneShot(audioClip);
        audioSource.Play();
    }
}
