using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private const string AnimationState = "AnimationName";
    public void SetAnim(AnimationName animationName)
    {
        anim.SetInteger(AnimationState, (int)animationName);
    }
}

public enum AnimationName
{
    Idle,
    Move,
    Blow
}