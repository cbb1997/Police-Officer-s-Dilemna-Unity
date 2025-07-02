using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BGData", menuName = "ScriptableObjects/BGData")]
public class BGData : ImageData
{
    [SerializeField] private Vector2[] m_DisplayPositions;

    public Vector2 GetDisplayPosition()
    {
        try
        {
            return m_DisplayPositions[UnityEngine.Random.Range(0, m_DisplayPositions.Length - 1)];
        }
        catch (Exception e)
        {
            Debug.Log($"Error in Position Generation: {e}");
            return new Vector2(0, -2);
        }
    }

    protected override void Awake()
    {

    }
}
