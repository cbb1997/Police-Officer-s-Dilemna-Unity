using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSFX : MonoBehaviour
{
    private AudioSource m_SRC;

    private void Start()
    {
        m_SRC = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!m_SRC.isPlaying) Destroy(gameObject);
    }
}
