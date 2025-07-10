using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;

    [SerializeField] private GameObject m_TrialObjects;

    private int m_DominantHand = 1;

    public static Action<int> OnGameStart;

    private void Start()
    {
        m_StartButton.onClick.AddListener(StartGame);

        FindObjectOfType<TMP_Dropdown>().onValueChanged.AddListener(SetDominantHand);
    }

    public void SetDominantHand(int hand)
    {
        m_DominantHand = hand + 1;
    }

    private void StartGame()
    {
        m_TrialObjects.SetActive(true);

        OnGameStart?.Invoke(m_DominantHand);

        gameObject.SetActive(false);
    }
}
