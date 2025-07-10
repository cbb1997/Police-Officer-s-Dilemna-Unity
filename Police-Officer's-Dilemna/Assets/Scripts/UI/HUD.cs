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
        //SetButtonEnabled(false);

        m_BaseScoreText = m_ScoreUI.text;
        UpdateScoreText(0);

        DataCollector.OnScoreChanged += UpdateScoreText;
        DataCollector.OnUserResponse += UpdateVisuals;

        //DisplayController.OnPersonGenerated += (imageData) => SetButtonEnabled(true);
    }

    private void UpdateScoreText(int score)
    {
        m_ScoreUI.text = $"{m_BaseScoreText} {score}";

        //SetButtonEnabled(false);
    }

    private void UpdateVisuals(UserResponse response)
    {
        SetTextOpacity(m_FeedbackUI, 0);

        if (response.Correct)
        {
            switch (response.ResponseType)
            {
                case ResponseType.Shoot:
                    m_FeedbackUI.text = "Correct Hit!";
                    break;

                case ResponseType.Clear:
                    m_FeedbackUI.text = "Correct Clear!";
                    break;

                case ResponseType.NoResponse:
                case ResponseType.EarlyResponse:
                case ResponseType.Other:
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

        FadeOutText(m_FeedbackUI);
    }

    private void FadeOutText(TMP_Text textUI)
    {
        StartCoroutine(TextFade(textUI, 1.0f, 0, m_TextFadeTime));
    }

    private IEnumerator TextFade(TMP_Text textUI, float startingOpacity, float endingOpacity, float duration)
    {
        SetTextOpacity(textUI, startingOpacity);

        float time = 0f;
        while (time < duration)
        {
            SetTextOpacity(textUI, Mathf.Lerp(startingOpacity, endingOpacity, time / duration));
            time += Time.deltaTime;
            yield return null;
        }

        SetTextOpacity(textUI, endingOpacity);
    }

    private void SetTextOpacity(TMP_Text textUI, float alpha)
    {
        textUI.color = new Color(textUI.color.r, textUI.color.g, textUI.color.b, alpha);
    }

    private void SetButtonEnabled(bool enabled)
    {
        m_ShootButton.interactable = enabled;
        m_ClearButton.interactable = enabled;
    }
}
