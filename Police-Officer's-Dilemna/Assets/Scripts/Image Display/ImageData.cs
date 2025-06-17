using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonRace
{
    Black,
    White,
    Other
}

[CreateAssetMenu(fileName = "ImageData", menuName = "ScriptableObjects/ImageData")]
public class ImageData : ScriptableObject
{
    [SerializeField] private Sprite m_PersonImage;

    [SerializeField] private PersonRace m_ImagePersonRace;

    private void Awake()
    {
        
    }
}
