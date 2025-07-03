using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private DisplayData m_DisplayData;

    [SerializeField] private GameObject m_ScreenFilter;

    public static Action<BGData> OnBGGenerated;
    public static Action<PersonData> OnPersonGenerated;

    private ImageDatabase m_ImageDatabase;

    private float m_CurrentGenerationTime;
    private Vector2 m_CurrentImagePos;

    [ReadOnly][SerializeField] private int m_CurrentImages, m_CurrentMaxImages;

    private GameObject m_CurrentPerson, m_CurrentBG;

    private void Start()
    {
        if (m_DisplayData == null)
        {
            Debug.LogError("Missing DisplayData. Terminating Application.");
            QuitGame();
        }

        m_ImageDatabase = GetComponent<ImageDatabase>();

        NewTrial();
    }
    
    private void Update()
    {
        
    }

    private void NewTrial()
    {
        StartCoroutine(TrialHelper(m_DisplayData.TrialDelay));
    }

    private IEnumerator TrialHelper(float delay)
    {
        m_ScreenFilter.SetActive(true);
        
        m_CurrentImages = 0;
        m_CurrentMaxImages = UnityEngine.Random.Range(m_DisplayData.MinImages, m_DisplayData.MaxImages);

        yield return new WaitForSeconds(delay);

        GenerateImage();
    }

    private void GenerateImage()
    {
        if (m_CurrentImages > m_CurrentMaxImages)
        {
            NewTrial();
            return;
        }

        StartCoroutine(GenerateHelper(m_DisplayData.DisplayDelay, m_CurrentImages == m_CurrentMaxImages));

        m_CurrentImages++;
    }

    private IEnumerator GenerateHelper(float delay, bool generatePerson)
    {
        if (m_CurrentBG == null && m_CurrentPerson == null)
        {
            m_ScreenFilter.SetActive(true);
        }

        yield return new WaitForSeconds(delay);

        m_CurrentGenerationTime = UnityEngine.Random.Range(m_DisplayData.MinImageTime, m_DisplayData.MaxImageTime);
        StartCoroutine(GenerateBG(m_CurrentGenerationTime));

        //Debug.Log($"BG Display Time: {m_CurrentGenerationTime}");

        float maxTime = m_CurrentGenerationTime + (m_DisplayData.MaxPersonTime - m_DisplayData.MaxImageTime);

        //Debug.Log($"Person Max Display Time: {maxTime}");

        if (generatePerson)
        {
            m_CurrentGenerationTime = UnityEngine.Random.Range(m_DisplayData.MinPersonTime, maxTime);
            StartCoroutine(GeneratePerson(m_CurrentGenerationTime));

            //Debug.Log($"Person Display Time: {m_CurrentGenerationTime}");
        }

        m_ScreenFilter.SetActive(false);
    }

    private IEnumerator GenerateBG(float bgTime)
    {
        int seed = new System.Random().Next(0, m_ImageDatabase.GetBGLength());

        OnBGGenerated?.Invoke(m_ImageDatabase.GetBGData(seed));

        m_CurrentBG = Instantiate(m_ImageDatabase.GetBGPrefab(seed));
        m_CurrentImagePos = m_ImageDatabase.GetBGData(seed).GetDisplayPosition();

        yield return new WaitForSeconds(bgTime);

        RemoveImages();

        GenerateImage();
    }

    private IEnumerator GeneratePerson(float personTime)
    {
        Destroy(m_CurrentPerson);

        int seed = new System.Random().Next(0, m_ImageDatabase.GetPersonLength());

        yield return new WaitForSeconds(personTime);

        OnPersonGenerated?.Invoke(m_ImageDatabase.GetPersonData(seed));
        m_CurrentPerson = Instantiate(m_ImageDatabase.GetPersonPrefab(seed), m_CurrentImagePos, Quaternion.identity);
    }

    private void RemoveImages()
    {
        if (m_CurrentBG != null) Destroy(m_CurrentBG);
        if (m_CurrentPerson != null) Destroy(m_CurrentPerson);

        m_CurrentBG = null;
        m_CurrentPerson = null;
    }

    private void EndTrial()
    {
        RemoveImages();
        StopAllCoroutines();

        m_ScreenFilter.SetActive(true);

        NewTrial();
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
