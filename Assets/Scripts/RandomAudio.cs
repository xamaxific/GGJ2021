using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_clipsToPlay;

    public void PlayRandomClip() {
        int i = Random.Range( 0, m_clipsToPlay.Length );
        m_audioSource.clip = m_clipsToPlay[ i ];
        m_audioSource.Play();
    }
}
