using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public interface IWindowController : IDisposable
    {
        BaseWindowView View
        {
            get;
        }

        void Initialize(BaseWindowView view);
        void ApplyData(object data);
    }
}