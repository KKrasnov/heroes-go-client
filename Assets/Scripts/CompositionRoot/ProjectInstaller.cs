using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        ServicesInstaller.Install(Container);
        
        //TODO: refactor this! Move to scene context and prefer to use factory
        Container.Bind<IGameMapManager>().FromMethod(() => GameObject.FindObjectOfType<GameMapManager>()).AsSingle();
        Container.Bind<IGameMapEntriesViewHelper>().FromMethod(() => GameObject.FindObjectOfType<GameMapEntriesViewHelper>()).AsSingle();

    }
}