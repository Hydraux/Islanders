// Code written by tutmo (youtube.com/tutmo)
// For help, check out the tutorial - https://youtu.be/PNWK5o9l54w

using System.Collections.Generic;
using UnityEngine;

public class BodyPartsManager : MonoBehaviour
{
    // ~~ 1. Updates All Animations to Match Player Selections

    [SerializeField] public CharacterBodySO characterBody;

    // String Arrays
    [SerializeField] public string[] bodyPartTypes;
    [SerializeField] public string[] characterStates;
    [SerializeField] public string[] characterDirections;
    
    // Animation
    private Animator animator;
    public AnimationClip walkAnimationClip;
    public AnimationClip idleAnimationClip;
    public AnimationClip axeAnimationClip;
    private AnimatorOverrideController animatorOverrideController;
    public AnimationClipOverrides defaultAnimationClips;

    private void Start()
    {
        // Set animator
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        defaultAnimationClips = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(defaultAnimationClips);

        // Set body part animations
        UpdateBodyParts();
    }

    public void UpdateBodyParts()
    {
        // Override default animation clips with character body parts
        for (int partIndex = 0; partIndex < bodyPartTypes.Length; partIndex++)
        {
            // Get current body part
            string partType = bodyPartTypes[partIndex];
            // Get current body part ID
            string partID = characterBody.BodyParts[partIndex].bodyPart.AnimationID.ToString();

            
            
            for (int directionIndex = 0; directionIndex < characterDirections.Length; directionIndex++)
            {
                string direction = characterDirections[directionIndex];

                // Get players animation from player body
                // ***NOTE: Unless Changed Here, Animation Naming Must Be: "[Type]_[Index]_[state]_[direction]" (Ex. Body_0_idle_down)
                walkAnimationClip = Resources.Load<AnimationClip>("Animations/Walk/" + partType + "/" +partID + "/" + partType + "_walk_" + direction);

                idleAnimationClip = Resources.Load<AnimationClip>("Animations/Idle/" + partType + "/" +partID + "/" + partType + "_idle_" + direction);

                axeAnimationClip = Resources.Load<AnimationClip>("Animations/Axe/" + partType + "/" +partID + "/" + partType + "_idle_" + direction);
                
                // Override default animation
                defaultAnimationClips[partType + "_walk_" + direction] = walkAnimationClip;
                defaultAnimationClips[partType + "_idle_" + direction] = idleAnimationClip;
                defaultAnimationClips[partType + "_" + direction] = idleAnimationClip;
            }
            
        }

        // Apply updated animations
        animatorOverrideController.ApplyOverrides(defaultAnimationClips);
    }

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}