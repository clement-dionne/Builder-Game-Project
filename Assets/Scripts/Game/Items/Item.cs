using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items/New Items", order = 1)]
public class Item : ScriptableObject
{
    public string Name = "No Name";
    public Sprite ItemSprite;

    public bool IsBuilding = false;
    public GameObject BuildingGameObject;

    public int NumberOfObject = 0;
}