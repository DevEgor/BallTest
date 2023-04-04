using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public event Action<int> OnEnter;
    public event Action<int> OnExit;

    public int Id;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke(Id);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit?.Invoke(Id);
    }

    public void HideAndDestroy()
    {
        _animator.SetTrigger("Hide");
    }

    public void DestroyAfterHide()
    {
        Destroy(gameObject);
    }
}
