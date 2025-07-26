using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using EditorInvokeButton;

public class SaveController : MonoBehaviour
{
    private UserData m_UserData;

    private string m_SavePath, m_SaveDirectory;

    //[ReadOnly][SerializeField] private string m_Key;

    [ReadOnly][SerializeField] private byte[] m_Key, m_InitVector;

    private void Start()
    {
        DataCollector.OnDataUpdated += SetUserData;
        DataCollector.OnUserResponse += (response) => SaveGame();

    }

    private void Update()
    {
        
    }

    private void SetUserData(UserData data)
    {
        m_UserData = data;
    }

    [InvokeButton]
    private void GenerateAes()
    {
        Aes aes = Aes.Create();

        m_Key = aes.Key;
        m_InitVector = aes.IV;
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
        string json = JsonUtility.ToJson(m_UserData);

        byte[] bytes = AesEncrypt(json);

        Debug.Log(json == AesDecrypt(bytes));

        return;

        using (var stream = File.Open(m_SavePath, FileMode.Open))
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
            {
                writer.Write(AesEncrypt(json));
            }
        }
    }

    private byte[] AesEncrypt(string data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = m_Key;
            aes.IV = m_InitVector;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))    
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(data);
                swEncrypt.Close();

                return msEncrypt.ToArray();
            }

        }
    }

    private void LoadGame()
    {

    }

    private string AesDecrypt(byte[] data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = m_Key;
            aes.IV = m_InitVector;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream msDecrypt = new MemoryStream())
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}
