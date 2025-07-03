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

    [SerializeField] private int m_MinImages, m_MaxImages;
    public int MinImages { get => m_MinImages; }
    public int MaxImages { get => m_MaxImages; }

    [SerializeField] private float m_DisplayDelay, m_TrialDelay;
    public float DisplayDelay { get => m_DisplayDelay; }
    public float TrialDelay { get => m_TrialDelay; }

    /**
    [SerializeField] private float m_PersonDisplayRate;
    public float PersonDisplayRate { get => m_PersonDisplayRate; }
    */

    private void Awake()
    {
        
    }
}
