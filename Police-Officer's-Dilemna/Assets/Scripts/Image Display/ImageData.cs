using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ImageData", menuName = "ScriptableObjects/ImageData")]
public class ImageData : ScriptableObject
{
    [SerializeField] private Sprite m_PersonImage, m_BGImage;

    [SerializeField] private PersonRace m_ImagePersonRace;

    private void Awake()
    {
        
    }
}
