using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public GameObject Platform;
        public GameObject Crystal;
        public GameObject Player;
        public int StartGeneratePathLength;
        public Vector2Int StartPlaceSize;
        public Vector3 StartGeneratePlacePos;
        public Vector3 StartGeneratePathPos;
        public Vector3 PlayerStartPos;
        public int AddPlatformOffset;
        public int RemovePlatformOffset;
    }

    [Inject]
    private Settings _settings;
    [Inject]
    private IGenerateCrystal _crystalGenerator;
    [Inject]
    private IGeneratePlatform _platformGenerator;
    [Inject]
    readonly SignalBus _signalBus;


    private LinkedList<Platform> _platformObjects = new LinkedList<Platform>();
    private LinkedList<GameObject> _crystalObjects = new LinkedList<GameObject>();
    private int _totalPlatfromCount;
    private GameObject _player;

    private void Start()
    {
        _signalBus.Subscribe<RestartSignal>(Restart);
        GenerateLevel();
    }

    private void Restart()
    {
        ClearLevel();
        GenerateLevel();
    }

    private void ClearLevel()
    {
        _totalPlatfromCount = 0;
        GameObject.Destroy(_player);
        foreach (var item in _platformObjects)
        {
            RemovePlatform(item);
        }

        _platformObjects.Clear();

        foreach (var item in _crystalObjects)
        {
            if (item != null)
            {
                GameObject.Destroy(item);
            }
        }

        _crystalObjects.Clear();
    }

    private void GenerateLevel()
    {
        GenerateStartPlace();
        GenerateStartPath();
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        _player = GameObject.Instantiate(_settings.Player);
        _player.transform.position = _settings.PlayerStartPos;
    }

    private void GenerateStartPlace()
    {
        for (int i = 0; i < _settings.StartPlaceSize.x; i++)
        {
            for (int j = 0; j < _settings.StartPlaceSize.y; j++)
            {
                var placePos = _settings.StartGeneratePlacePos;
                placePos.x += i;
                placePos.z += j;
                var platform = AddPlatform(placePos);
            }
        }
    }

    private void GenerateStartPath()
    {
        var platfromPos = _settings.StartGeneratePathPos;
        AddPlatform(platfromPos);

        for (int i = 1; i < _settings.StartGeneratePathLength; i++)
        {
            GeneratePlatform();
        }
    }

    private void GeneratePlatform()
    {
        var lastPlatform = _platformObjects.Last.Value;
        var platfromPos = lastPlatform.transform.position;

        _platformGenerator.TryGeneratePlatform(lastPlatform, AddPlatform);

        _crystalGenerator.TryGenerateCrystal(platfromPos, AddCrystal);
    }

    private void AddPlatform(Vector3 pos, Vector3 dir)
    {
        var platform = AddPlatform(pos);
        platform.transform.forward = dir;
    }

    private GameObject AddPlatform(Vector3 pos)
    {
        var platformGO = Instantiate(_settings.Platform);
        platformGO.transform.SetParent(transform);
        platformGO.transform.position = pos;
        var platform = platformGO.GetComponent<Platform>();
        _platformObjects.AddLast(platform);

        platform.OnEnter += TryGeneratePlatform;
        platform.OnExit += TryRemovePlatform;

        platform.Id = _totalPlatfromCount;

        _totalPlatfromCount++;

        return platformGO;
    }

    private void AddCrystal(Vector3 pos)
    {
        var crystal = Instantiate(_settings.Crystal);
        crystal.transform.SetParent(transform);
        crystal.transform.position = pos;
        _crystalObjects.AddLast(crystal);
    }

    private void TryGeneratePlatform(int platformId)
    {
        if (platformId + _settings.AddPlatformOffset > _totalPlatfromCount)
        {
            GeneratePlatform();
        }
    }

    private void TryRemovePlatform(int platformId)
    {
        var platformsToRemoveCount = _platformObjects.Count(x => x.Id <= platformId - _settings.RemovePlatformOffset);

        for (int i = 0; i < platformsToRemoveCount; i++)
        {
            var platform = _platformObjects.First();
            RemovePlatform(platform);
            _platformObjects.RemoveFirst();
        }
    }

    private void RemovePlatform(Platform platform)
    {
        platform.OnEnter -= TryGeneratePlatform;
        platform.OnExit -= TryRemovePlatform;
        platform.HideAndDestroy();
        // GameObject.Destroy(platform.gameObject);
    }
}
