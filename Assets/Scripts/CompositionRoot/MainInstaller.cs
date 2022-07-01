using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGameMapManager>().FromInstance(GameObject.FindObjectOfType<GameMapManager>()).AsSingle();
        Container.Bind<IGameMapEntriesViewHelper>().FromInstance(GameObject.FindObjectOfType<GameMapEntriesViewHelper>()).AsSingle();
    }
}