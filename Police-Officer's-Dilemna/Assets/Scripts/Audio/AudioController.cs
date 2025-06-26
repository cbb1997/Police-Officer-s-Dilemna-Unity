using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private GameObject m_SFX;

    private static AudioController Instance;

    private void Start()
    {
        Instance = this as AudioController;        
    }

    public static void PlaySFX(AudioClip sfx, float volume = 1.0f)
    {
        if (sfx == null) return;

        GameObject newSfx = Instantiate(Instance.m_SFX);

        newSfx.GetComponent<AudioSource>().volume = volume;

        newSfx.GetComponent<AudioSource>().clip = sfx;

        newSfx.GetComponent<AudioSource>().Play();

        newSfx.GetComponent<SimpleSFX>().enabled = true;
    }
}
