using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ImageData", menuName = "ScriptableObjects/ImageData")]
public class ImageData : ScriptableObject
{
    [SerializeField] private Sprite m_PersonImage, m_BGImage;

    [SerializeField] private PersonRace m_PersonRace;
    [SerializeField] private ObjectType m_PersonObject;
    public PersonRace PersonRace { get => m_PersonRace; }
    public ObjectType PersonObject { get => m_PersonObject; }

    [SerializeField] private GameObject m_PrefabReference;
    public GameObject Prefab { get => m_PrefabReference; }

    private void Awake()
    {
        
    }
}
