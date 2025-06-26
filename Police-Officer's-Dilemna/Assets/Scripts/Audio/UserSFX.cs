using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSFX : MonoBehaviour
{
    [SerializeField] private AudioClip m_ShootSFX, m_ClearSFX;

    [SerializeField] private AudioClip m_CorrectSFX, m_InCorrectSFX;


    [SerializeField] private float m_SFXVolume;

    private void Start()
    {
        DataCollector.OnUserResponse += RunSFX;
    }

    private void RunSFX(UserResponse response)
    {
        switch (response.ResponseType)
        {
            case ResponseType.Shoot:
                AudioController.PlaySFX(m_ShootSFX, m_SFXVolume);
                break;

            case ResponseType.Clear:
                AudioController.PlaySFX(m_ClearSFX, m_SFXVolume);
                break;

            case ResponseType.NoResponse:
                break;

            case ResponseType.Other:
                break;

            default:
                break;
        }


        switch (response.Correct)
        {
            case true:
                AudioController.PlaySFX(m_CorrectSFX, m_SFXVolume);
                break;
            
            case false:
                AudioController.PlaySFX(m_InCorrectSFX, m_SFXVolume);
                break;
            
            default:
                break;
        }
    }
}
