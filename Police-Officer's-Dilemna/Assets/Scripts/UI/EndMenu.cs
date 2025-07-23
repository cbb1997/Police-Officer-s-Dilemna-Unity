using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_PracticeEndMenu, m_EndMenu, m_TrialObjects;

    [SerializeField] private TMP_Text m_PracticeEndText, m_EndText;

    private int m_Score;

    private void Start()
    {
        DisplayController.OnPracticeOver += PracticeEndDisplay;
        DisplayController.OnGameOver += EndDisplay;

        DataCollector.OnScoreChanged += SaveScore;
    }

    private void SaveScore(int score)
    {
        m_Score = score;
    }

    private void EndDisplay()
    {
        m_TrialObjects.SetActive(false);
        m_EndMenu.SetActive(true);

        m_EndText.text += $" {m_Score}";
    }

    private void PracticeEndDisplay()
    {
        m_TrialObjects.SetActive(false);
        m_PracticeEndMenu.SetActive(true);

        m_PracticeEndText.text += $" {m_Score}";
    }

}
