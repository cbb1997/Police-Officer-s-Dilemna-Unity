using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;

    [SerializeField] private GameObject m_TrialObjects;

    private void Start()
    {
        m_StartButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        m_TrialObjects.SetActive(true);
        gameObject.SetActive(false);
    }
}
