using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PersonRace
{
    Black,
    White,
    Other
}

public enum ObjectType
{
    Weapon = 2,
    Innocuous = 1,
    Other = 3
}

public enum ResponseType
{
    Shoot = 2,
    Clear = 1,
    NoResponse = 0,
    Other = 3
}

[System.Serializable]
public class UserResponse
{
    [SerializeField] private PersonRace m_PersonRace;
    internal PersonRace PersonRace { get => m_PersonRace; }

    [SerializeField] private ObjectType m_ObjectType;
    internal ObjectType ObjectType { get => m_ObjectType; }

    [SerializeField] private ResponseType m_ResponseType;
    internal ResponseType ResponseType { get => m_ResponseType; }

    private float m_ResponseTime;
    internal float ResponseTime { get => m_ResponseTime; }

    private float m_BGTime, m_PersonTime;
    internal float BGTime { get => m_BGTime; }
    internal float PersonTime { get => m_PersonTime; }

    [SerializeField] private bool m_Correct;
    internal bool Correct { get => m_Correct; }

    internal UserResponse(ResponseType responseType, PersonRace personRace, ObjectType objectType)
    {
        m_ResponseType = responseType;

        m_PersonRace = personRace;

        m_ObjectType = objectType;

        if ((int)(m_ObjectType) == (int)(m_ResponseType))
        {
            m_Correct = true;
        }
        else
        {
            m_Correct = false;
        }
    }
}

[System.Serializable]
public class UserData
{
    [SerializeField] private int m_Score;
    internal int Score { get => m_Score; }

    [SerializeField] private List<UserResponse> m_Responses;

    public void ResetData()
    {
        m_Score = 0;

        if (m_Responses != null) 
        {
            m_Responses.Clear();
        }
        else
        {
            m_Responses = new List<UserResponse>();
        }
    }

    internal void AddResponse (UserResponse response)
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

    [SerializeField] private PersonData m_CurrentPersonData;

    public static Action<ResponseType> OnUserResponse;

    public static Action<int> OnScoreChanged;

    private bool m_Responded = true;

    private void Start()
    {
        m_CurrentUserData = new UserData();

        m_CurrentUserData.ResetData();

        DisplayController.OnPersonGenerated += SetPersonData;
    }

    private void OnDestroy()
    {
        DisplayController.OnPersonGenerated -= SetPersonData;
    }

    private void Update()
    {
        if (m_Responded) return;

        if (Input.GetAxis("Horizontal") < 0)
        {
            NewResponse(2);
            m_Responded = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            NewResponse(1);
            m_Responded = true;
        }
    }

    public void SetPersonData(PersonData data)
    {
        m_CurrentPersonData = data;
        
        if(!m_Responded)
        {
            NewResponse(0);
        }

        m_Responded = false;
    }

    public void NewResponse(int responseType)
    {
        m_CurrentUserData.AddResponse(new UserResponse((ResponseType)responseType, m_CurrentPersonData.PersonRace, m_CurrentPersonData.PersonObject));

        OnUserResponse?.Invoke((ResponseType)responseType);

        OnScoreChanged?.Invoke(m_CurrentUserData.Score);
    }
}
