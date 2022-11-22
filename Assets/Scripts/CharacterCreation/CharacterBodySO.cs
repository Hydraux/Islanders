// Created with help from tutmo (youtube.com/tutmo)
// following tutorial https://youtu.be/PNWK5o9l54w

using UnityEngine;

[CreateAssetMenu(menuName ="Data/CharacterBodySO") ]
public class CharacterBodySO : ScriptableObject
{
    
    public BodyPart[] BodyParts = {};
}

[System.Serializable]
public class BodyPart{
    public string bodyPartName;
    public CharacterBodyPartSO bodyPart;
}