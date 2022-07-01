using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Assets.Scripts.UI;

public class GameMapManager : MonoBehaviour, IGameMapManager
{
    private const float MAP_INACTIVE_UPDATE_INTERVAL_STATIC = 60f; //sec
    private const float MAP_INACTIVE_UPDATE_INTERVAL_DYNAMIC = 10f; //sec

    [SerializeField]
    private Transform _mapObjectsParent;

    private IPlayerLocationService _playerLocationService;
    private IGameMapService _gameMapService;


    private List<GameMapEntryData> _mapDynamicEntries = new List<GameMapEntryData>();
    private List<GameMapEntryData> _mapStaticEntries = new List<GameMapEntryData>();
    private Dictionary<Guid, GameMapEntryPresentation> _entriesPresentation = new Dictionary<Guid, GameMapEntryPresentation>();

    private float _timeSinceLastStaticUpdate = 0;
    private float _timeSinceLastDynamicUpdate = 0;

    public void RefreshMap()
    {
        _timeSinceLastStaticUpdate = MAP_INACTIVE_UPDATE_INTERVAL_STATIC;
        _timeSinceLastDynamicUpdate = MAP_INACTIVE_UPDATE_INTERVAL_DYNAMIC;
    }

    public void RefreshMapEntry(Guid entryId)
    {
        RefreshMap();
    }

    private void Awake()
    {
        _playerLocationService = CompositionRoot.Container.Resolve<IPlayerLocationService>();
        _gameMapService = CompositionRoot.Container.Resolve<IGameMapService>();
        _timeSinceLastStaticUpdate = MAP_INACTIVE_UPDATE_INTERVAL_STATIC;
        _timeSinceLastDynamicUpdate = MAP_INACTIVE_UPDATE_INTERVAL_DYNAMIC;
        StartCoroutine(ProccessGameMapStaticUpdating());
        StartCoroutine(ProccessGameMapDynamicUpdating());
    }

    private void OnMapCenterChanged()
    {
        RefreshMap();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerCenterChanged += OnMapCenterChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerCenterChanged -= OnMapCenterChanged;
    }

    IEnumerator ProccessGameMapStaticUpdating()
    {
        while (!GameManager.Instance.locationServicesIsRunning)
            yield return new WaitForEndOfFrame();
        while(true)
        {
            while(_timeSinceLastStaticUpdate < MAP_INACTIVE_UPDATE_INTERVAL_STATIC)
            {
                yield return new WaitForEndOfFrame();
                _timeSinceLastStaticUpdate += Time.deltaTime;
            }

            _timeSinceLastStaticUpdate = 0;

            List<GameMapEntryData> newStaticEntries = _gameMapService.GetNearbyStaticEntries(_playerLocationService.loc.ToVector2());

            foreach(var entry in _mapStaticEntries)
            {
                if (newStaticEntries.Find(ent => ent.EntryId == entry.EntryId) == null && _entriesPresentation.ContainsKey(entry.EntryId))
                {
                    GameObject.Destroy(_entriesPresentation[entry.EntryId].gameObject);
                    _entriesPresentation.Remove(entry.EntryId);
                }
            }

            _mapStaticEntries = newStaticEntries;

            foreach(var entry in _mapStaticEntries)
            {
                if(!_entriesPresentation.ContainsKey(entry.EntryId))
                    _entriesPresentation.Add(entry.EntryId, CreateNewEntryPresentation());

                _entriesPresentation[entry.EntryId].ApplyEntry(entry);
            }
        }
    }

    IEnumerator ProccessGameMapDynamicUpdating()
    {
        while (!GameManager.Instance.locationServicesIsRunning)
            yield return new WaitForEndOfFrame();
        while (true)
        {
            while (_timeSinceLastDynamicUpdate < MAP_INACTIVE_UPDATE_INTERVAL_DYNAMIC)
            {
                yield return new WaitForEndOfFrame();
                _timeSinceLastDynamicUpdate += Time.deltaTime;
            }

            _timeSinceLastDynamicUpdate = 0;

            List<GameMapEntryData> newDynamicEntries = _gameMapService.GetNearbyDynamicEntries(_playerLocationService.loc.ToVector2());

            foreach (var entry in _mapDynamicEntries)
            {
                if (newDynamicEntries.Find(ent => ent.EntryId == entry.EntryId) == null && _entriesPresentation.ContainsKey(entry.EntryId))
                {
                    GameObject.Destroy(_entriesPresentation[entry.EntryId].gameObject);
                    _entriesPresentation.Remove(entry.EntryId);
                }
            }

            _mapDynamicEntries = newDynamicEntries;

            foreach (var entry in _mapDynamicEntries)
            {
                if (!_entriesPresentation.ContainsKey(entry.EntryId))
                    _entriesPresentation.Add(entry.EntryId, CreateNewEntryPresentation());

                _entriesPresentation[entry.EntryId].ApplyEntry(entry);
            }
        }
    }

    private GameMapEntryPresentation CreateNewEntryPresentation()
    {
        GameObject entryObject = GameObject.Instantiate(Resources.Load<GameObject>(string.Format("Prefabs/GameMap/Entry")));
        entryObject.transform.SetParent(_mapObjectsParent);
        entryObject.transform.localPosition = Vector3.zero;
        entryObject.transform.localEulerAngles = Vector3.zero;
        entryObject.transform.localScale = Vector3.one;

        GameMapEntryPresentation presentation = entryObject.GetComponent<GameMapEntryPresentation>();
        presentation.OnSelect += OnEntrySelected;
        return presentation;
    }

    private void OnEntrySelected(GameMapEntryData entry)
    {
        CompositionRoot.Container.Resolve<UIManager>().OpenWindow(WindowType.GameMapEntryDialog, entry.EntryId);
    }
}
