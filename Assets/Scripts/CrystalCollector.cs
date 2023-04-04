using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollector : MonoBehaviour, IAddCrystal
{
    private int _crystalCounter;

    public void AddCrystal()
    {
        _crystalCounter++;
    }
}
