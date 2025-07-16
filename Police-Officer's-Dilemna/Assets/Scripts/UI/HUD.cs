using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text m_ScoreUI, m_FeedbackUI;
    [SerializeField] private TMP_Text m_LeftText, m_RightText;

    [SerializeField] private Button m_ShootButton, m_ClearButton;

    [SerializeField] private float m_TextFadeTime;

    private string m_BaseScoreText;
    private int m_Score, m_Difference;

    private void Start()
    {
        //SetButtonEnabled(false);

        m_BaseScoreText = m_ScoreUI.text;
        UpdateScoreText(0);

        DataCollector.OnScoreChanged += UpdateScoreText;
        DataCollector.OnUserResponse += UpdateVisuals;

        MainMenu.OnGameStart += SetControlsText;
    }

    private void OnDestroy()
    {
        DataCollector.OnScoreChanged -= UpdateScoreText;
        DataCollector.OnUserResponse -= UpdateVisuals;

        MainMenu.OnGameStart -= SetControlsText;
    }

    public void SetControlsText(int hand)
    {
        switch (hand)
        {
            case 1:
                m_LeftText.text = "[Left Arrow]: Clear";
                m_RightText.text = "[Right Arrow]: Shoot";
                break;
            case 2:
                m_LeftText.text = "[Left Arrow]: Shoot";
                m_RightText.text = "[Right Arrow]: Clear";
                break;
            default:
                break;
        }
    }

    private void UpdateScoreText(int score)
    {
        //Debug.Log($"{m_Score} {score}");

        m_Difference = Math.Abs(m_Score - score);

        m_Score = score;

        m_ScoreUI.text = $"{m_BaseScoreText} {m_Score}";

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

            m_FeedbackUI.text += $" +{m_Difference}";
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

            m_FeedbackUI.text += $" -{m_Difference}";
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
