using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDatabase : MonoBehaviour
{
    [SerializeField] private PersonData[] m_PersonDatabase;

    [SerializeField] private BGData[] m_BGDatabase;

    private void Start()
    {
        
    }

    public int GetPersonLength()
    {
        return m_PersonDatabase.Length;
    }

    public int GetBGLength()
    {
        return m_BGDatabase.Length;
    }

    public PersonData GetPersonData(int i)
    {
        return m_PersonDatabase[i];
    }

    public BGData GetBGData(int i)
    {
        return m_BGDatabase[i];
    }

    public GameObject GetPersonPrefab(int i)
    {
        return m_PersonDatabase[i].Prefab;
    }

    public GameObject GetBGPrefab(int i)
    {
        return m_BGDatabase[i].Prefab;
    }
}
