using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameMapEntryPresentation : MonoBehaviour
{
    [SerializeField]
    private ObjectPosition _mapObjectPosition;

    [SerializeField]
    private Image _entryViewImg;

    private GameMapEntryData _cachedEntryData;

    public event Action<GameMapEntryData> OnSelect;

    public void ApplyEntry(GameMapEntryData entry)
    {
        _cachedEntryData = entry;

        _mapObjectPosition.setPositionOnMap(new GeoPoint(_cachedEntryData.Lat_d, _cachedEntryData.Lon_d));

        _entryViewImg.sprite = ProjectContext.Instance.Container.Resolve<IGameMapEntriesViewHelper>().GetSpriteForEntry(_cachedEntryData);
    }

    public void Select()
    {
        if (OnSelect != null)
            OnSelect(_cachedEntryData);
    }
}
