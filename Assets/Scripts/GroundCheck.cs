using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GroundCheck : MonoBehaviour, IOnGround
{
    [Inject]
    private GameSystem _gameSystem;
    
    public event Action OnFail;
    public bool IsOnGround => _isOnGround;

    private bool _isOnGround = true;
    private int _layerMask;


    private void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (_isOnGround && _gameSystem.IsOnGame)
        {
            if (!Physics.Raycast(transform.position + Vector3.up, Vector3.down, 3.0f, _layerMask))
            {
                _isOnGround = false;
                OnFail?.Invoke();
                _gameSystem.GameFail();
            }
        }
    }
}
