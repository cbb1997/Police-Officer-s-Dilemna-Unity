using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text m_ScoreUI, m_FeedbackUI;

    [SerializeField] private Button m_ShootButton, m_ClearButton;

    [SerializeField] private float m_TextFadeTime;

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
        if (response.Correct)
        {
            m_FeedbackUI.text = "Correct!";
            return;
        }

        switch (response.ResponseType)
        {
            case ResponseType.Shoot:
                m_FeedbackUI.text = "False Alarm!";
                break;

            case ResponseType.Clear:
                m_FeedbackUI.text = "Miss!";
                break;

            case ResponseType.NoResponse:
                m_FeedbackUI.text = "Too Late!";
                break;

            case ResponseType.EarlyResponse:
                m_FeedbackUI.text = "Too Early!";
                break;

            case ResponseType.Other:
                break;

            default:
                break;
        }
    }

    private void FadeOutText (TMP_Text textUI)
    {
        StartCoroutine(SetTextOpacity(textUI, 1.0f, 0, m_TextFadeTime));
    }

    private IEnumerator SetTextOpacity(TMP_Text textUI, float startingOpacity, float endingOpacity, float seconds)
    {
        yield return null;
    }

    private void SetButtonEnabled(bool enabled)
    {
        m_ShootButton.interactable = enabled;
        m_ClearButton.interactable = enabled;
    }
}
