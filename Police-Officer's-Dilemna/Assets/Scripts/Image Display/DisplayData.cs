using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DisplayData", menuName = "ScriptableObjects/DisplayData")]
public class DisplayData : ScriptableObject
{
    [SerializeField] private float m_MinImageTime, m_MaxImageTime;
    public float MinImageTime { get => m_MinImageTime; }
    public float MaxImageTime { get => m_MaxImageTime; }

    [SerializeField] private float m_MinPersonTime, m_MaxPersonTime;
    public float MaxPersonTime { get => m_MaxPersonTime; }
    public float MinPersonTime { get => m_MinPersonTime; }


    [SerializeField] private float m_DisplayDelay;
    public float DisplayDelay { get => m_DisplayDelay; }

    [SerializeField] private float m_PersonDisplayRate;
    public float PersonDisplayRate { get => m_PersonDisplayRate; }

    private void Awake()
    {
        
    }
}
