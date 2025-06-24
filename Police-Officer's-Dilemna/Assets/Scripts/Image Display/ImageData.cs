using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ImageData", menuName = "ScriptableObjects/ImageData")]
public class ImageData : ScriptableObject
{
    [SerializeField] protected string m_ImageName;
    public string Name { get => m_ImageName; }

    [SerializeField] protected Sprite m_ImageRef;
    public Sprite Image { get => m_ImageRef; }
    
    [SerializeField] protected GameObject m_PrefabRef;
    public GameObject Prefab { get => m_PrefabRef; }

    protected virtual void Awake()
    {
        
    }
}
