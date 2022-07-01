using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GoogleMapView : MonoBehaviour {

    private const string GOOGLE_API_KEY = "AIzaSyAvbRz3Gw3_ZzvnevXCTNTrt20lLEH2rKo";

    [SerializeField]
    private Image _mapImage;

	// Use this for initialization
	void Start () {
		if(Input.location.isEnabledByUser)
        {
        }
        StartCoroutine(GetMap());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator GetMap()
    {
        var request = UnityWebRequestTexture.GetTexture("https://maps.googleapis.com/maps/api/staticmap?size=512x512&zoom=15&center=Brooklyn&style=feature:road.local%7Celement:geometry%7Ccolor:0x00ff00&style=feature:landscape%7Celement:geometry.fill%7Ccolor:0x000000&style=element:labels%7Cinvert_lightness:true&style=feature:road.arterial%7Celement:labels%7Cinvert_lightness:false&key="
            + GOOGLE_API_KEY);
        yield return request.Send();

        if(request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Texture2D texture = (request.downloadHandler as DownloadHandlerTexture).texture;
            Debug.Log(texture == null);
            _mapImage.sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), Vector2.one * 0.5f);
        }
    }
}
