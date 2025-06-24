using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonData", menuName = "ScriptableObjects/PersonData")]
public class PersonData : ImageData
{
    [SerializeField] private PersonRace m_PersonRace;
    [SerializeField] private ObjectType m_PersonObject;
    public PersonRace PersonRace { get => m_PersonRace; }
    public ObjectType PersonObject { get => m_PersonObject; }

    protected override void Awake()
    {

    }
}
