using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BaseUIElement : MonoBehaviour
    {
        protected virtual void Start()
        {
        }

        protected virtual void Awake()
        {
        }

        protected virtual void Update()
        {

        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }

        protected virtual void OnDestroy()
        {
        }

		public virtual void Display(bool isVisible)
		{
			gameObject.SetActive (isVisible);
		}
    }
}