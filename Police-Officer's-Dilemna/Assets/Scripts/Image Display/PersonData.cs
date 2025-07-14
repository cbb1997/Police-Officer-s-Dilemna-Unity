using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonData", menuName = "ScriptableObjects/PersonData")]
public class PersonData : ImageData
{
    [SerializeField] private PersonRace m_PersonRace;
    public PersonRace PersonRace { get => m_PersonRace; }

    [SerializeField] private ObjectType m_PersonObject;
    public ObjectType PersonObject { get => m_PersonObject; }

    [SerializeField] private Vector2 m_CurrentImagePosition;
    public Vector2 CurrentPosition { get => m_CurrentImagePosition; set => m_CurrentImagePosition = value; }

    protected override void Awake()
    {

    }
}
