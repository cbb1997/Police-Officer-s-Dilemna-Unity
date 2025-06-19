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
        m_CurrentGenerationTime = UnityEngine.Random.Range(m_DisplayData.MinImageTime, m_DisplayData.MaxImageTime);

        StartCoroutine(GenerateHelper(m_CurrentGenerationTime));
    }

    private IEnumerator GenerateHelper(float seconds)
    {
        m_CurrentBG = Instantiate(m_BG[new System.Random().Next(0, m_BG.Length)]);

        yield return new WaitForSeconds(seconds);

        Destroy(m_CurrentBG);

        GenerateImage();
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
