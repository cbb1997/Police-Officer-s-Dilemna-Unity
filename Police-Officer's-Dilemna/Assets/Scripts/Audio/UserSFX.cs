using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSFX : MonoBehaviour
{
    [SerializeField] private AudioClip m_ShootSFX, m_ClearSFX;

    [SerializeField] private AudioClip m_CorrectSFX, m_IncorrectSFX, m_MissSFX;


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
            case ResponseType.EarlyResponse:
                break;

            case ResponseType.Other:
                break;

            default:
                break;
        }


        if (response.Correct)
        {
            AudioController.PlaySFX(m_CorrectSFX, m_SFXVolume);
        }
        else
        {
            switch (response.ResponseType)
            {
                case ResponseType.Shoot:
                case ResponseType.Clear:
                    AudioController.PlaySFX(m_IncorrectSFX, m_SFXVolume);
                    break;

                case ResponseType.NoResponse:
                case ResponseType.EarlyResponse:
                    AudioController.PlaySFX(m_MissSFX, m_SFXVolume);
                    break;

                case ResponseType.Other:
                    break;

                default:
                    break;
            }
        }

        //
    }
}
