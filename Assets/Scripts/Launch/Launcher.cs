using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Launch
{
    /// <summary>
    /// Application starting point.
    /// </summary>
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;

        private void Start()
        {
            _sceneContext.Run();
            
            //TODO: load resources
            
            SceneManager.LoadScene("Main");
        }
    }
}