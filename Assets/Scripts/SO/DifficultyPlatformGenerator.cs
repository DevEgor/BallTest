using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BallTest/PlatformGenerator/Difficulty")]
public class DifficultyPlatformGenerator : ScriptableObject, IGeneratePlatform
{

    public IGeneratePlatform.GeneratePlatformTypeEnum GenerateCrystalType => IGeneratePlatform.GeneratePlatformTypeEnum.Difficulty;

    [Range(0f, 1f)]
    public float TurnRightChance;
    public int TilesCount;

    [NonSerialized]
    private int SafePlatform;

    public void TryGeneratePlatform(Platform lastPlatform, Action<Vector3, Vector3> instantiateMethod)
    {
        var platform = lastPlatform;
        var platfromPos = platform.transform.position;
        bool rotate = UnityEngine.Random.Range(0f, 1f) <= TurnRightChance && SafePlatform <= 0;
        var dir = rotate ? platform.transform.right : platform.transform.forward;

        platfromPos.x += dir.x;
        platfromPos.z += dir.z;
        var lastPlatformPos = platfromPos;


        int sign = -1;
        int advancedPlatformIndex = 1;
        for (int i = 1; i < TilesCount; i++)
        {
            Vector3 offset = platform.transform.right * i * sign;
            if (rotate)
            {
                offset = platform.transform.forward * i * sign;

                if (i % 2 == 0)
                {
                    var pos = platform.transform.position + platform.transform.forward * advancedPlatformIndex;
                    instantiateMethod?.Invoke(pos, dir);
                    pos -= platform.transform.right;
                    instantiateMethod?.Invoke(pos, dir);
                    advancedPlatformIndex++;
                }
            }

            platfromPos.x += offset.x;
            platfromPos.z += offset.z;
            instantiateMethod?.Invoke(platfromPos, dir);

            sign *= -1;
        }

        if (rotate)
        {
            for (int i = 1; i < TilesCount; i++)
            {

            }

            SafePlatform = TilesCount;
        }
        else
        {
            SafePlatform--;
        }

        instantiateMethod?.Invoke(lastPlatformPos, dir);
    }
}
