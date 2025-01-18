using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private AnimationName _currentAnim;

    private void Start()
    {
        _currentAnim = AnimationName.Idle;
        anim.SetBool(AnimationTrigger.GetAnimationTrigger(_currentAnim), true);
        foreach (var animationName in (AnimationName[])System.Enum.GetValues(typeof(AnimationName)))
        {
            if (animationName == _currentAnim)
                continue;

            anim.SetBool(AnimationTrigger.GetAnimationTrigger(animationName), false);
        }
    }

    public void SetAnim(AnimationName animationName)
    {
        if (_currentAnim == animationName)
            return;

        anim.SetBool(AnimationTrigger.GetAnimationTrigger(_currentAnim), false);
        anim.SetBool(AnimationTrigger.GetAnimationTrigger(animationName), true);
        _currentAnim = animationName;
    }
}

public enum AnimationName
{
    Idle,
    Move,
    Blow,
}

public static class AnimationTrigger
{
    public static int GetAnimationTrigger(AnimationName animationName)
    {
        switch (animationName)
        {
            case AnimationName.Idle:
                return Animator.StringToHash("idle");
            case AnimationName.Move:
                return Animator.StringToHash("move");
            case AnimationName.Blow:
                return Animator.StringToHash("blow");
            default:
                return 0;
        }
    }
}
