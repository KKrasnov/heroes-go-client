using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public abstract class Spinner : MonoBehaviour
    {
        public abstract bool IsActive { get; set; }
    }
}
