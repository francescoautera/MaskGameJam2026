using UnityEngine;

namespace GameJam
{
    
public class ChangeAnimatorValue : MonoBehaviour
{
    [SerializeField]  string animationParameter;
    [SerializeField] Animator _animator;

    public void SetAnimationParameter(bool parameterValue)
    {
        _animator.SetBool(animationParameter,parameterValue);   
    }
}
}
