// Created with help from tutmo (youtube.com/tutmo)
// following tutorial https://youtu.be/PNWK5o9l54w

using UnityEngine;

[CreateAssetMenu(menuName = "Data/BodySO")]
public class CharacterBodyPartSO : ScriptableObject
{
    public string bodyPartName = "BodyPartName";
    public int AnimationID = 0;
    public AnimationClip[] Animations = {};
}
