using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EditorInvokeButton;

[System.Serializable]
public class PersonDict
{
    [ReadOnly][SerializeField] private PersonRace m_PersonRace;
    internal PersonRace PersonRace { get => m_PersonRace; }

    [ReadOnly][SerializeField] private ObjectType m_ObjectType;
    internal ObjectType ObjectType { get => m_ObjectType; }

    [ReadOnly][SerializeField] private List<int> m_Data;
    internal List<int> Data { get => m_Data; }

    internal PersonDict(PersonRace personRace, ObjectType objectType)
    {
        m_PersonRace = personRace;
        m_ObjectType = objectType;
        m_Data = new List<int>();
    }

    internal PersonDict(int personRace, int objectType)
    {
        m_PersonRace = (PersonRace) personRace;
        m_ObjectType = (ObjectType) objectType;
        m_Data = new List<int>();
    }
}


public class ImageDatabase : MonoBehaviour
{
    #region Data Structures
    [SerializeField] private PersonData[] m_PersonRaw;
    [SerializeField] private List<PersonDict> m_PersonDatabase;

    [SerializeField] private BGData[] m_BGRaw;

    private List<int> m_PersonPool, m_BGPool;

    #endregion

    private void Start()
    {
        if (m_PersonDatabase == null || m_PersonDatabase.Count == 0) SortPersonData();

        m_PersonPool = new List<int>();
        m_BGPool = new List<int>();
    }

    [InvokeButton]
    private void SortPersonData()
    {
        m_PersonDatabase.Clear();

        for (int i = 1; i < Enum.GetValues(typeof(PersonRace)).Length; i++)
        {
            for (int j = 1; j < Enum.GetValues(typeof(ObjectType)).Length; j++)
            {
                m_PersonDatabase.Add(new PersonDict(i, j));
            }
        }

        for (int i = 0; i < m_PersonRaw.Length; i++)
        {
            for (int j = 0; j < m_PersonDatabase.Count; j++)
            {
                if (m_PersonRaw[i].PersonRace == m_PersonDatabase[j].PersonRace && m_PersonRaw[i].PersonObject == m_PersonDatabase[j].ObjectType)
                {
                    m_PersonDatabase[j].Data.Add(i);
                    break;
                }
            }
        }
    }

    public void MakePersonPool(int trialSize)
    {
        for (int i = 0; i < m_PersonDatabase.Count; i++)
        {
            for (int j = 0; j < trialSize / m_PersonDatabase.Count; j++)
            {
                m_PersonPool.Add(m_PersonDatabase[i].Data[UnityEngine.Random.Range(0, m_PersonDatabase[i].Data.Count - 1)]);
            }
        }
    }

    public void MakeBGPool(int trialSize)
    {

    }

    #region Getters
    public int GetPersonLength()
    {
        return m_PersonRaw.Length;
    }

    public int GetBGLength()
    {
        return m_BGRaw.Length;
    }

    public int GetPoolNumber(int i)
    {
        return m_PersonPool[i];
    }

    public PersonData GetPersonData(int i)
    {
        return m_PersonRaw[i];
    }

    public BGData GetBGData(int i)
    {
        return m_BGRaw[i];
    }

    public GameObject GetPersonPrefab(int i)
    {
        return m_PersonRaw[i].Prefab;
    }

    public GameObject GetBGPrefab(int i)
    {
        return m_BGRaw[i].Prefab;
    }

    #endregion
}
