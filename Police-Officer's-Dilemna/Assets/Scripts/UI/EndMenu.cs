using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_EndMenu;

    private void Start()
    {
        DisplayController.OnGameOver += () => m_EndMenu.SetActive(true);
    }

}
