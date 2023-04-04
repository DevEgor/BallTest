using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "BallTest/Settings")]
public class SettingsScriptableObject : ScriptableObjectInstaller<SettingsScriptableObject>
{
    public LevelGenerator.Settings LevelGeneratorSettings;
    public IGenerateCrystal.Settings CrystalGeneratorSettings;
    public IGeneratePlatform.Settings GeneratePlatformSettings;
    public PlayerMove.Settings PlayerMoveSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(LevelGeneratorSettings);
        Container.BindInstance(CrystalGeneratorSettings);
        Container.BindInstance(PlayerMoveSettings);
        Container.BindInstance(GeneratePlatformSettings);
    }
}
