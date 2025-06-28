using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapData Data;

    [SerializeField]
    private MapData[] maps;
    [SerializeField]
    private MapUI mapPrefab;
    [SerializeField]
    private Transform mapParent;
    [SerializeField, Range(1, 30)]
    private int makeMapCount = 3;
    private List<MapUI> mapPooling;

    private void Awake()
    {
        SetupPooling();
        SetupUI();
    }

    private void SetupPooling()
    {
        mapPooling = new List<MapUI>(makeMapCount);

        for (int i = 0; i < makeMapCount; i++)
        {
            MakeMap();
        }
    }

    private void SetupUI()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            AddMap(maps[i]);
        }
    }

    private void AddMap(MapData data)
    {
        MapUI instance = GetMap();

        instance.Setup(data);
        instance.gameObject.SetActive(true);
    }

    private MapUI MakeMap()
    {
        MapUI instance = Instantiate(mapPrefab, mapParent);

        instance.gameObject.SetActive(false);

        mapPooling.Add(instance);

        return instance;
    }

    private MapUI GetMap()
    {
        for (int i = mapPooling.Count - 1; i >= 0; i--)
        {
            if (!mapPooling[i].gameObject.activeSelf)
            {
                return mapPooling[i];
            }
        }

        return MakeMap();
    }
}
