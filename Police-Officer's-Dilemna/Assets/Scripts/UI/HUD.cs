using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text m_ScoreUI, m_FeedbackUI;

    [SerializeField] private Button m_ShootButton, m_ClearButton;

    private string m_BaseScoreText;

    private void Start()
    {
        SetButtonEnabled(false);

        m_BaseScoreText = m_ScoreUI.text;
        UpdateScoreText(0);

        DataCollector.OnScoreChanged += UpdateScoreText;
        DataCollector.OnUserResponse += UpdateVisuals;

        DisplayController.OnPersonGenerated += (imageData) => SetButtonEnabled(true);
    }

    private void UpdateScoreText(int score)
    {
        m_ScoreUI.text = $"{m_BaseScoreText} {score}";

        SetButtonEnabled(false);
    }

    private void UpdateVisuals(UserResponse response)
    {

    }

    private void SetButtonEnabled(bool enabled)
    {
        m_ShootButton.interactable = enabled;
        m_ClearButton.interactable = enabled;
    }
}
