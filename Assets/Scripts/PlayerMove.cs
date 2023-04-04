using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMove : MonoBehaviour
{
    [Serializable]
    public class Settings {
        public float Speed;
    }

    [Inject]
    private Settings _settings;
    [Inject]
    private GameSystem _gameSystem;

    private  IOnGround _onGround;

    // Start is called before the first frame update
    void Start()
    {
       _onGround = GetComponent<IOnGround>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (_onGround.IsOnGround && _gameSystem.IsOnGame)
        {
            var nextPos = transform.forward + transform.position;
            transform.MoveToTarget(nextPos, _settings.Speed);
        }
    }
}
