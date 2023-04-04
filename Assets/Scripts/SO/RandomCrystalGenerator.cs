using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "BallTest/CrystalGenerator/Random")]
public class RandomCrystalGenerator : ScriptableObject, IGenerateCrystal
{
    [Serializable]
    public class Settings
    {
        public int CrystalStepCount;
    }

    [SerializeField]
    private Settings _settings;
    private int _nextCrystalStep;
    private int _currentStep;

    public IGenerateCrystal.GenerateCrystalTypeEnum GenerateCrystalType => IGenerateCrystal.GenerateCrystalTypeEnum.Random;

    public void OnEnable()
    {
        Debug.Log("OnEnable");

        GenerateNextCrystalStep();
    }

    public void TryGenerateCrystal(Vector3 pos, Action<Vector3> instantiateMethod)
    {
        if (_currentStep == _nextCrystalStep)
        {
            instantiateMethod?.Invoke(pos);
        }
        _currentStep++;
        if (_currentStep >= _settings.CrystalStepCount)
        {
            _currentStep = 0;
            GenerateNextCrystalStep();
        }
    }

    private void GenerateNextCrystalStep()
    {
        _nextCrystalStep = UnityEngine.Random.Range(0, _settings.CrystalStepCount);
    }
}