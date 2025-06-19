using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private DisplayData m_DisplayData;

    [SerializeField] private GameObject[] m_People, m_BG;

    private float m_CurrentGenerationTime;
    private GameObject m_CurrentPerson, m_CurrentBG;

    private void Start()
    {
        if (m_DisplayData == null)
        {
            Debug.LogError("Missing DisplayData. Terminating Application.");
            QuitGame();
        }

        GenerateImage();
    }
    
    private void Update()
    {
        
    }

    private void GenerateImage()
    {
        StartCoroutine(GenerateHelper(m_DisplayData.DisplayDelay));
    }

    private IEnumerator GenerateHelper(float delay)
    {
        yield return new WaitForSeconds(delay);

        m_CurrentGenerationTime = UnityEngine.Random.Range(m_DisplayData.MinImageTime, m_DisplayData.MaxImageTime);
        StartCoroutine(GenerateBG(m_CurrentGenerationTime));

        float maxTime = m_CurrentGenerationTime + (m_DisplayData.MaxPersonTime - m_DisplayData.MaxImageTime);

        m_CurrentGenerationTime = UnityEngine.Random.Range(m_DisplayData.MinImageTime, maxTime);
        StartCoroutine(GeneratePerson(m_CurrentGenerationTime));
    }

    private IEnumerator GenerateBG(float bgTime)
    {
        m_CurrentBG = Instantiate(m_BG[new System.Random().Next(0, m_BG.Length)]);

        yield return new WaitForSeconds(bgTime);

        Destroy(m_CurrentBG);
        Destroy(m_CurrentPerson);

        GenerateImage();
    }

    private IEnumerator GeneratePerson(float personTime)
    {
        Destroy(m_CurrentPerson);

        yield return new WaitForSeconds(personTime);

        m_CurrentPerson = Instantiate(m_People[new System.Random().Next(0, m_People.Length)]);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false; 
            return;
#endif

        Application.Quit();
    }
}
