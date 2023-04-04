using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BallTest/PlatformGenerator/Simply")]
public class SimplePlatformGenerator : ScriptableObject, IGeneratePlatform
{
    public IGeneratePlatform.GeneratePlatformTypeEnum GenerateCrystalType => IGeneratePlatform.GeneratePlatformTypeEnum.Simply;

    [Range(0f, 1f)]
    public float TurnRightChance;
    public int SafePlatformCount;

    [NonSerialized]
    private int _safePlatformCount;

    public void TryGeneratePlatform(Platform lastPlatform, Action<Vector3, Vector3> instantiateMethod)
    {
        var platfromPos = lastPlatform.transform.position;
        bool rotate = UnityEngine.Random.Range(0f, 1f) <= TurnRightChance && _safePlatformCount <= 0;
        var dir = rotate ? lastPlatform.transform.right : lastPlatform.transform.forward;

        platfromPos.x += dir.x;
        platfromPos.z += dir.z;
        instantiateMethod?.Invoke(platfromPos, dir);

        if (rotate)
        {
            _safePlatformCount = SafePlatformCount;
        }
        else
        {
            _safePlatformCount--;
        }
    }
}
