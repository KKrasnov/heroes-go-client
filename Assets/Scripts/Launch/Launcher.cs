using UnityEngine;
using UnityEngine.SceneManagement;

namespace Launch
{
    /// <summary>
    /// Application starting point.
    /// </summary>
    public class Launcher : MonoBehaviour
    {
        private void Start()
        {
            //TODO: load resources
            
            SceneManager.LoadScene("Main");
        }
    }
}