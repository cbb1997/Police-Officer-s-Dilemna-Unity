using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonRace
{
    Black,
    White,
    Other
}

public class ImageData : ScriptableObject
{
    [SerializeField] private Sprite m_PersonImage;

    [SerializeField] private PersonRace m_ImagePersonRace;

    private void Awake()
    {
        
    }
}
