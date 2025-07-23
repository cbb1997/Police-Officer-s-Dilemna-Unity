using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DisplayController : MonoBehaviour
{
    #region Serialized References
    [SerializeField] private DisplayData m_DisplayData;
    [SerializeField] private GameObject m_ScreenFilter;
    [SerializeField] private ImageDatabase m_ImageDatabase;

    #endregion

    #region Actions
    public static Action<BGData> OnBGGenerated;
    public static Action<PersonData> OnPersonGenerated;
    public static Action OnTrialOver;
    public static Action OnPracticeOver;
    public static Action OnGameOver;

    #endregion

    #region Members
    private int m_TrialNumber, m_ImageNumber;
    private bool m_IsPractice;

    private float m_CurrentGenerationTime;
    private Vector2 m_CurrentImagePos;

    private int m_CurrentImages, m_CurrentMaxImages;

    private GameObject m_CurrentPerson, m_CurrentBG;

    #endregion

    #region Callbacks
    private void Start()
    {
        if (m_DisplayData == null)
        {
            Debug.LogError("Missing DisplayData. Terminating Application.");
            QuitGame();
        }

        if (m_ImageDatabase == null) m_ImageDatabase = GetComponent<ImageDatabase>();

        DataCollector.OnUserResponse += EndTrial;

        m_ImageDatabase.MakePersonPool(m_DisplayData.NumTrials);
        m_ImageDatabase.MakeBGPool(m_DisplayData.NumTrials * m_DisplayData.MaxImages);

        m_IsPractice = m_DisplayData.NumPracticeTrials > 0;

    }
    
    private void OnEnable()
    {
        m_TrialNumber = 0;
        NewTrial();
    }

    private void Update()
    {
        
    }

    #endregion

    #region ImageGeneration
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

        if (m_IsPractice)
        {
            if (m_TrialNumber >= m_DisplayData.NumPracticeTrials)
            {
                OnPracticeOver?.Invoke();
                StopAllCoroutines();
            }
            else
            {
                GenerateImage();
            }
        }
        else
        {
            if (m_TrialNumber >= m_DisplayData.NumTrials)
            {
                OnGameOver?.Invoke();
                StopAllCoroutines();
            }
            else
            {
                GenerateImage();
            }
        }
    }

    private void GenerateImage()
    {
        if (m_CurrentImages > m_CurrentMaxImages)
        {
            NextTrial();
            return;
        }

        StartCoroutine(GenerateHelper(m_DisplayData.DisplayDelay, m_CurrentImages == m_CurrentMaxImages));

        m_CurrentImages++;
        m_ImageNumber++;
    }

    private IEnumerator GenerateHelper(float delay, bool generatePerson)
    {
        if (m_CurrentBG == null && m_CurrentPerson == null)
        {
            m_ScreenFilter.SetActive(true);
        }

        yield return new WaitForSeconds(delay);

        if (generatePerson)
        {
            m_CurrentGenerationTime = m_DisplayData.FinalImageTime;
        }
        else
        {
            m_CurrentGenerationTime = UnityEngine.Random.Range(m_DisplayData.MinImageTime, m_DisplayData.MaxImageTime);
        }

        StartCoroutine(GenerateBG(m_CurrentGenerationTime));

        Debug.Log($"BG Display Time: {m_CurrentGenerationTime}");

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
        int currentBGIndex = m_ImageDatabase.GetBGPoolNumber(m_ImageNumber);

        OnBGGenerated?.Invoke(m_ImageDatabase.GetBGData(currentBGIndex));

        m_CurrentBG = Instantiate(m_ImageDatabase.GetBGPrefab(currentBGIndex));
        m_CurrentImagePos = m_ImageDatabase.GetBGData(currentBGIndex).GetDisplayPosition();

        yield return new WaitForSeconds(bgTime);

        RemoveImages();

        GenerateImage();
    }

    private IEnumerator GeneratePerson(float personTime)
    {
        Destroy(m_CurrentPerson);

        int currentPersonIndex = m_ImageDatabase.GetPersonPoolNumber(m_TrialNumber);

        yield return new WaitForSeconds(personTime);

        m_ImageDatabase.GetPersonData(currentPersonIndex).CurrentPosition = m_CurrentImagePos;
        OnPersonGenerated?.Invoke(m_ImageDatabase.GetPersonData(currentPersonIndex));
        m_CurrentPerson = Instantiate(m_ImageDatabase.GetPersonPrefab(currentPersonIndex), m_CurrentImagePos, Quaternion.identity);
    }

    private void RemoveImages()
    {
        if (m_CurrentBG != null) Destroy(m_CurrentBG);
        if (m_CurrentPerson != null) Destroy(m_CurrentPerson);

        m_CurrentBG = null;
        m_CurrentPerson = null;
    }

    private void NextTrial()
    {
        OnTrialOver?.Invoke();
        m_TrialNumber++;
        NewTrial();
    }

    private void EndTrial(UserResponse response = null)
    {
        if (response != null && response.ResponseType != ResponseType.EarlyResponse) return;

        RemoveImages();
        StopAllCoroutines();

        m_ScreenFilter.SetActive(true);

        NextTrial();
    }

    #endregion

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; 
#else
        Application.Quit();
#endif
    }
}
