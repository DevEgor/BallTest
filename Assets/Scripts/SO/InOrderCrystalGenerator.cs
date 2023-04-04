using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "BallTest/CrystalGenerator/InOrder")]
public class InOrderCrystalGenerator : ScriptableObject, IGenerateCrystal
{
    [Serializable]
    public class Settings
    {
        public int CrystalStepCount;
    }

    [SerializeField]
    private Settings _settings;
    
    [NonSerialized]
    private int _nextCrystalStep;
    [NonSerialized]
    private int _currentStep;

    public IGenerateCrystal.GenerateCrystalTypeEnum GenerateCrystalType => IGenerateCrystal.GenerateCrystalTypeEnum.InOrder;

    public void TryGenerateCrystal(Vector3 pos, Action<Vector3>  instantiateMethod)
    {
        if (_nextCrystalStep == _currentStep)
        {
            instantiateMethod?.Invoke(pos);
        }

        _currentStep++;

        if (_currentStep >= _settings.CrystalStepCount)
        {
            _currentStep = 0;

            _nextCrystalStep++;

            if (_nextCrystalStep >= _settings.CrystalStepCount)
            {
                _nextCrystalStep = 0;
            }
        }
    }
}
