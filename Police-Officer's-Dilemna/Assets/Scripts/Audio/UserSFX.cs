using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSFX : MonoBehaviour
{
    [SerializeField] private AudioClip m_ShootSFX;

    [SerializeField] private float m_SFXVolume;

    private void Start()
    {
        DataCollector.OnUserResponse += RunSFX;
    }

    private void RunSFX(ResponseType response)
    {
        switch (response)
        {
            case ResponseType.Shoot:
                AudioController.PlaySFX(m_ShootSFX, m_SFXVolume);
                break;

            case ResponseType.Clear:
                break;

            case ResponseType.NoResponse:
                break;

            case ResponseType.Other:
                break;

            default:
                break;
        }
    }
}
