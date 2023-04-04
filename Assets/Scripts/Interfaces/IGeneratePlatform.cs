using System;
using System.Linq;
using UnityEngine;

public interface IGeneratePlatform
{
    public enum GeneratePlatformTypeEnum
    {
        Simply,
        Difficulty,
    }

    [Serializable]
    public class Settings
    {
        public GeneratePlatformTypeEnum SelectedGenerator;
        public ScriptableObject[] PossibleGenerators;

        public IGeneratePlatform GetSelectedGenerator()
        {
            return PossibleGenerators.FirstOrDefault(x => (x as IGeneratePlatform).GenerateCrystalType == SelectedGenerator) as IGeneratePlatform;
        }
    }

    public GeneratePlatformTypeEnum GenerateCrystalType { get; }

    public void TryGeneratePlatform(Platform lastPlatform, Action<Vector3, Vector3> instantiateMethod);
}