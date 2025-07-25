using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveController : MonoBehaviour
{
    [ReadOnly][SerializeField] private UserData m_UserData;

    private string m_SavePath, m_SaveDirectory;

    private void Start()
    {
        DataCollector.OnDataUpdated += SetUserData;
    }

    private void Update()
    {
        
    }

    private void SetUserData(UserData data)
    {
        m_UserData = data;
    }

    private void CreateNewSave()
    {
        if (!Directory.Exists(m_SaveDirectory))
        {
            Directory.CreateDirectory(m_SaveDirectory);
        }

        if (!File.Exists(m_SavePath))
        {
            File.Create(m_SavePath).Close();
        }
    }

    private void SaveGame()
    {
        string jsonData = JsonUtility.ToJson(m_UserData);

        Debug.Log(jsonData);
    }

    private void LoadGame()
    {

    }
}
