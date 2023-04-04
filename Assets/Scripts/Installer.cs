using Zenject;
using UnityEngine;
using System.Collections;

public class Installer : MonoInstaller
{
    [Inject]
    private IGenerateCrystal.Settings _crystalGeneratorSettings;
    [Inject]
    private IGeneratePlatform.Settings _platformGeneratorSettings;

    public override void InstallBindings()
    {
        Container.Bind<IGenerateCrystal>().FromInstance(_crystalGeneratorSettings.GetSelectedGenerator());
        Container.Bind<IGeneratePlatform>().FromInstance(_platformGeneratorSettings.GetSelectedGenerator());
        Container.Bind<GameSystem>().AsSingle();

        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<RestartSignal>();
    }
}