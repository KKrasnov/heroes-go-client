using UnityEngine;
using System.Collections;

public class PlayerLocationService : IPlayerLocationService
{
	public GeoPoint loc
    {
        get;
        set;
    }

	public float trueHeading;
	public bool locServiceIsRunning
    {
        get;
        private set;
    }

	public int maxWait = 30; // seconds
	private float locationUpdateInterval = 0.2f; // seconds
	private double lastLocUpdate = 0.0; //seconds

    public PlayerLocationService()
    {
        loc = new GeoPoint();
        locServiceIsRunning = false;
    }

	public IEnumerator _StartLocationService()
	{
		
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser) {
			Debug.LogWarning ("Locations is not enabled.");

			//NOTE: If location is not enabled, we initialize the postion of the player to somewhere in Los Angeles, just for demonstration purposes
			loc.setLatLon_deg (49.801766f, 24.066496f); 

			GameManager.Instance.playerStatus = GameManager.PlayerStatus.FreeFromDevice;
			// To get the game run on Editor without location services
			locServiceIsRunning = true;
			yield break;
		}

		// Start service before querying location
		Input.location.Start();
		// Wait until service initializes
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in maxWait seconds
		if (maxWait < 1)
		{
			Debug.Log("Locations services timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
            Debug.LogError("Location services failed");
			yield break;
		} else if (Input.location.status == LocationServiceStatus.Running){
			GameManager.Instance.playerStatus = GameManager.PlayerStatus.TiedToDevice;
			loc.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);
			Debug.Log ("Location: " + Input.location.lastData.latitude.ToString ("R") + " " + Input.location.lastData.longitude.ToString ("R") + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			locServiceIsRunning = true;
			lastLocUpdate = Input.location.lastData.timestamp;
		} else {
            Debug.LogError("Unknown Error!");
		}
		Debug.Log (loc.ToString());
	}

	public IEnumerator RunLocationService()
	{
		double lastLocUpdate = 0.0;
		while (true) {
			if (lastLocUpdate != Input.location.lastData.timestamp) {
				loc.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);
				trueHeading = Input.compass.trueHeading;
				Debug.Log ("Location: " + Input.location.lastData.latitude.ToString ("R") + " " + Input.location.lastData.longitude.ToString ("R") + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
				//locServiceIsRunning = true;
				lastLocUpdate = Input.location.lastData.timestamp;
			}
			yield return new WaitForSeconds(locationUpdateInterval);
		}
	}
}