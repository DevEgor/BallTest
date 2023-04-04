using System;
using System.Linq;
using UnityEngine;

public interface IGenerateCrystal
{
    public enum GenerateCrystalTypeEnum
    {
        Random,
        InOrder,
    }

    [Serializable]
    public class Settings
    {
        public GenerateCrystalTypeEnum SelectedGenerator;
        public ScriptableObject[] PossibleGenerators;

        public IGenerateCrystal GetSelectedGenerator()
        {
            return PossibleGenerators.FirstOrDefault(x => (x as IGenerateCrystal).GenerateCrystalType == SelectedGenerator) as IGenerateCrystal;
        }
    }

    public GenerateCrystalTypeEnum GenerateCrystalType { get; }

    public void TryGenerateCrystal(Vector3 pos, Action<Vector3> instantiateMethod);
}