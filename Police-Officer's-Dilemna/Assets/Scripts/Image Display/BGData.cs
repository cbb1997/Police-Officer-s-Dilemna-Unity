using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGData", menuName = "ScriptableObjects/BGData")]
public class BGData : ImageData
{
    [SerializeField] private Vector2[] m_DisplayPositions;

    public Vector2 GetDisplayPostion()
    {
        return m_DisplayPositions[UnityEngine.Random.Range(0, m_DisplayPositions.Length -1)];
    }

    protected override void Awake()
    {

    }
}
