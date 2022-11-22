// Created with help from tutmo (youtube.com/tutmo)
// following tutorial https://youtu.be/PNWK5o9l54w


using UnityEngine;
using UnityEngine.UI;

public class BodyPartsSelector : MonoBehaviour
{
    [SerializeField] private CharacterBodySO characterBody;
    [SerializeField] private BodyPartSelection[] bodyPartSelections;

    private void Start()
    {
        for(int i = 0; i < bodyPartSelections.Length; i++)
        {
            GetCurrentBodyParts(i);
        }
    }

    public void NextBodyPart(int partIndex)
    {
        if(ValidateIndexValue(partIndex))
        {
            if(bodyPartSelections[partIndex].bodyPartCurrentIndex < bodyPartSelections[partIndex].bodyPartOptions.Length - 1)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex++;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = 0;
            }

            UpdateCurrentPart(partIndex);
        }
    }

    public void PreviousBody(int partIndex)
    {
        if(ValidateIndexValue(partIndex))
        {
            if(bodyPartSelections[partIndex].bodyPartCurrentIndex > 0)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex--;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = bodyPartSelections[partIndex].bodyPartOptions.Length - 1;
            }
            UpdateCurrentPart(partIndex);
        }
    }

    private bool ValidateIndexValue(int partIndex)
    {
        if(partIndex > bodyPartSelections.Length || partIndex < 0)
        {
            Debug.Log("Index value does not match any body parts!");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GetCurrentBodyParts(int partIndex)
    {
        // Get current body part name
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = characterBody.BodyParts[partIndex].bodyPart.bodyPartName;
        // Get current body part animation id
        bodyPartSelections[partIndex].bodyPartCurrentIndex = characterBody.BodyParts[partIndex].bodyPart.AnimationID;
    }

    private void UpdateCurrentPart(int partIndex)
    {
        // Update Selection Name Text
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex].bodyPartName;
        // Update Character Body Part
        characterBody.BodyParts[partIndex].bodyPart = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex];
    }

}
    [System.Serializable]
    public class BodyPartSelection
    {
        public string bodyPartName;
        public CharacterBodyPartSO[] bodyPartOptions;
        public Text bodyPartNameTextComponent;
        [HideInInspector] public int bodyPartCurrentIndex;
    }
