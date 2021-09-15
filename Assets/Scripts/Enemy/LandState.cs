using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LandState : State
{
    private Animator _animator;
    private bool _isOnLand = false;

    public bool IsOnLand => _isOnLand;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("Land");
    }
}
