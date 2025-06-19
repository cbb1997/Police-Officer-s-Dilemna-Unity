using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonRace
{
    Black,
    White,
    Other
}

public enum ObjectType
{
    Weapon,
    Innocuous,
    Other
}

public enum ResponseType
{
    Shoot,
    Clear,
    NoResponse,
    Other
}

[System.Serializable]
public class UserResponse
{
    private PersonRace m_PersonRace;
    internal PersonRace PersonRace { get => m_PersonRace; }

    private ObjectType m_ObjectType;
    internal ObjectType ObjectType { get => m_ObjectType; }

    private ResponseType m_ResponseType;
    internal ResponseType ResponseType { get => m_ResponseType; }

    private float m_ResponseTime;
    internal float ResponseTime { get => m_ResponseTime; }

    private float m_BGTime, m_PersonTime;
    internal float BGTime { get => m_BGTime; }
    internal float PersonTime { get => m_PersonTime; }

    private bool m_Correct;
    internal bool Correct { get => m_Correct; }

    internal UserResponse()
    {

    }
}

[System.Serializable]
public class UserData
{
    [SerializeField] private float m_Score;
    internal float Score { get => m_Score; }

    [SerializeField] private List<UserResponse> m_Responses;

    public void ResetData()
    {
        m_Score = 0;
        if(m_Responses != null) m_Responses.Clear();
    }

    public void AddResponse (UserResponse response)
    {
        if (response.Correct) 
        { 
            m_Score++; 
        }
        else
        {
            m_Score--;
        }

        m_Responses.Add(response);
    }
}

public class DataCollector : MonoBehaviour
{
    [SerializeField] private UserData m_CurrentUserData;

    private void Start()
    {
        m_CurrentUserData = new UserData();

        m_CurrentUserData.ResetData();
    }

    private void Update()
    {
        
    }
}
