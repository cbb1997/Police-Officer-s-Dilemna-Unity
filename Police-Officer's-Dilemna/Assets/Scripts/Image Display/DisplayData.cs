using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DisplayData", menuName = "ScriptableObjects/DisplayData")]
public class DisplayData : ScriptableObject
{
    [SerializeField] private float m_MinImageTime, m_MaxImageTime;
    public float MinImageTime { get => m_MinImageTime; }
    public float MaxImageTime { get => m_MaxImageTime; }

    private void Awake()
    {
        
    }
}
