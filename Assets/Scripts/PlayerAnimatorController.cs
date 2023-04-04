using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    private Animator _animator;
    private IOnGround _groundCheck;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _groundCheck = GetComponent<IOnGround>();

        _groundCheck.OnFail += PlayFailAnimation;
    }

    private void PlayFailAnimation()
    {
        _animator.SetTrigger("Drop");
    }

    private void OnDestroy()
    {
        _groundCheck.OnFail -= PlayFailAnimation;
    }
}
