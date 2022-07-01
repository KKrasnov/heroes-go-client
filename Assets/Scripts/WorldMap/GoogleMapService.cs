using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleMapService
{
    private readonly GoogleMapView _mapView;

    public GoogleMapService()
    {
        _mapView = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/GoogleMap/MapView")).GetComponent<GoogleMapView>();
    }
}
