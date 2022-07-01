using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerLocationService
{
    GeoPoint loc
    {
        get;
        set;
    }

    bool locServiceIsRunning
    {
        get;
    }

    IEnumerator _StartLocationService();
    IEnumerator RunLocationService();
}
