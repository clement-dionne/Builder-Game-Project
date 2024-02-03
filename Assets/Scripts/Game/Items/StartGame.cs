using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueAtStartFoItems
{
    public Item item;
    public int DefaultValue;
}

public class StartGame : MonoBehaviour
{
    public PlayerSelector playerSelector;
    public Item HDVHouse;
    public ValueAtStartFoItems[] ValueAtStart;

    void Start()
    {
        playerSelector.CurrentBuilding = HDVHouse.BuildingGameObject;

        for (int index = 0; index < ValueAtStart.Length; index ++)
        {
            ValueAtStart[index].item.NumberOfObject = ValueAtStart[index].DefaultValue;
        }
    }
}
