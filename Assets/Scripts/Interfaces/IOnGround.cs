using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnGround
{
    public event Action OnFail;
    public bool IsOnGround { get; }
}
