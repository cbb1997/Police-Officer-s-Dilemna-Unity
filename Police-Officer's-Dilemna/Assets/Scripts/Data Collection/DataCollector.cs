using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum PersonRace
{
    Black = 1,
    White = 2,
    Other = 0
}

public enum ObjectType
{
    Innocuous = 1,
    Shoot = 2,
    Other = 0
}

public enum ResponseType
{
    Clear = 1,
    Shoot = 2,
    NoResponse = 3,
    EarlyResponse = 4,
    Other = 0
}

public enum DominantHand
{
    Left = 1,
    Right = 2,
    Ambidextrous = 3,
    Other = 0
}

[System.Serializable]
public class UserResponse
{
    [ReadOnly][SerializeField] private PersonRace m_PersonRace;
    internal PersonRace PersonRace { get => m_PersonRace; }

    [ReadOnly][SerializeField] private ObjectType m_ObjectType;
    internal ObjectType ObjectType { get => m_ObjectType; }

    [ReadOnly][SerializeField] private ResponseType m_ResponseType;
    internal ResponseType ResponseType { get => m_ResponseType; }

    [ReadOnly][SerializeField] private string m_BGName, m_PersonName;
    internal string BGName { get => m_BGName; }
    internal string PersonName { get => m_PersonName; }

    [ReadOnly][SerializeField] private Vector2 m_PersonDisplayPosition;
    internal Vector2 PersonDisplayPos { get => m_PersonDisplayPosition; }

    [ReadOnly][SerializeField] private int m_CurrentScore;
    internal int CurrentScore { set => m_CurrentScore = value; }

    [ReadOnly][SerializeField] private float m_ResponseTime;
    internal float ResponseTime { get => m_ResponseTime; }

    [ReadOnly][SerializeField] private float m_BGTime, m_PersonTime;
    internal float BGTime { get => m_BGTime; }
    internal float PersonTime { get => m_PersonTime; }

    [ReadOnly][SerializeField] private bool m_Correct;
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
    [ReadOnly][SerializeField] private int m_Score;
    internal int Score { get => m_Score; }

    [ReadOnly][SerializeField] private DominantHand m_UserDominantHand;
    internal DominantHand UserDominantHand { get => m_UserDominantHand; set => m_UserDominantHand = value; }
    internal DominantHand NonDominantHand
    {
        get
        {
            if (m_UserDominantHand == DominantHand.Left) return DominantHand.Right;
            else if (m_UserDominantHand == DominantHand.Right) return DominantHand.Left;
            
            return DominantHand.Other;
        }
    }

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

    internal UserResponse AddResponse (UserResponse response)
    {
        UpdatePoints(response);

        m_Responses.Add(response);

        return response;
    }

    private void UpdatePoints(UserResponse response)
    {
        if (response.Correct)
        {
            switch (response.ResponseType)
            {
                case ResponseType.Shoot:
                    m_Score += 10;
                    break;

                case ResponseType.Clear:
                    m_Score += 5;
                    break;

                case ResponseType.NoResponse:
                case ResponseType.EarlyResponse:
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (response.ResponseType)
            {
                case ResponseType.Shoot:
                    m_Score -= 20;
                    break;

                case ResponseType.Clear:
                    m_Score -= 40;
                    break;

                case ResponseType.NoResponse:
                case ResponseType.EarlyResponse:
                    m_Score -= 10;
                    break;

                default:
                    break;
            }
        }

        response.CurrentScore = m_Score;
    }
}

public class DataCollector : MonoBehaviour
{
    [SerializeField] private UserData m_CurrentUserData;

    [ReadOnly][SerializeField] private PersonData m_CurrentPersonData;
    [ReadOnly][SerializeField] private BGData m_CurrentBGData;

    public static Action<UserResponse> OnUserResponse;

    public static Action<int> OnScoreChanged;

    private bool m_Responded = true;

    private void Start()
    {
        m_CurrentUserData = new UserData();

        m_CurrentUserData.ResetData();

        DisplayController.OnBGGenerated += SetBGData;
        DisplayController.OnPersonGenerated += SetPersonData;

        SetDominantHand(0);
        FindObjectOfType<TMP_Dropdown>().onValueChanged.AddListener(SetDominantHand);
    }

    private void OnDestroy()
    {
        DisplayController.OnPersonGenerated -= SetPersonData;
    }

    private void Update()
    {
        if (m_Responded) return;

        float inputX = Input.GetAxis("Horizontal");

        m_Responded = inputX != 0;

        if (m_Responded && m_CurrentPersonData == null)
        {
            NewResponse(ResponseType.EarlyResponse);
            return;
        }

        if (inputX < 0)
        {
            NewResponse((int) m_CurrentUserData.UserDominantHand);
        }
        else if (inputX > 0)
        {
            NewResponse((int) m_CurrentUserData.NonDominantHand);
        }
    }

    private void SetBGData (BGData data)
    {
        m_CurrentBGData = data;
        
        if (!m_Responded && m_CurrentPersonData != null)
        {
            NewResponse(ResponseType.NoResponse);
        }

        m_CurrentPersonData = null;
        m_Responded = false;
    }

    private void SetPersonData(PersonData data)
    {
        m_CurrentPersonData = data;
    }

    public void SetDominantHand (int hand)
    {
        m_CurrentUserData.UserDominantHand = (DominantHand)hand + 1;
    }

    public void NewResponse(int responseType)
    {
        OnUserResponse?.Invoke(m_CurrentUserData.AddResponse(new UserResponse((ResponseType)responseType, m_CurrentPersonData.PersonRace, m_CurrentPersonData.PersonObject)));

        OnScoreChanged?.Invoke(m_CurrentUserData.Score);
    }

    public void NewResponse(ResponseType responseType)
    {
        OnUserResponse?.Invoke(m_CurrentUserData.AddResponse(new UserResponse(responseType, m_CurrentPersonData.PersonRace, m_CurrentPersonData.PersonObject)));

        OnScoreChanged?.Invoke(m_CurrentUserData.Score);
    }
}
