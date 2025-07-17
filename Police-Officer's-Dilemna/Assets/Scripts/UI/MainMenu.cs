using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_StartButton, m_NextButton;
    [SerializeField] private TMP_Dropdown m_HandDropdown;

    [SerializeField] private GameObject[] m_Instructions;

    [SerializeField] private GameObject m_TrialObjects;

    private int m_DominantHand = 1;

    private int m_CurrentScreen;

    public static Action<int> OnGameStart;

    private void Start()
    {
        m_StartButton.onClick.AddListener(StartGame);
        m_NextButton.onClick.AddListener(Next);

        m_HandDropdown.onValueChanged.AddListener(SetDominantHand);
    }

    private void OnDestroy()
    {
        m_StartButton.onClick.RemoveListener(StartGame);
        m_NextButton.onClick.RemoveListener(Next);

        m_HandDropdown.onValueChanged.RemoveListener(SetDominantHand);
    }

    public void SetDominantHand(int hand)
    {
        m_DominantHand = hand + 1;
    }

    private void Next()
    {
        m_HandDropdown.gameObject.SetActive(false);

        m_CurrentScreen++;

        if (m_CurrentScreen >= m_Instructions.Length - 1)
        {
            m_NextButton.gameObject.SetActive(false);
            m_StartButton.gameObject.SetActive(true);
        }

        for (int i = 0; i < m_Instructions.Length; i++)
        {
            m_Instructions[i].SetActive(i == m_CurrentScreen);
        }
    }

    private void StartGame()
    {
        m_TrialObjects.SetActive(true);

        OnGameStart?.Invoke(m_DominantHand);

        gameObject.SetActive(false);
    }
}
