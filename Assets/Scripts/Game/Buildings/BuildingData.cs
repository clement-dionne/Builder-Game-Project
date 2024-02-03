using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data{
    public Item item;
    public int AmoountToGive;
    public float TimeBetweenGive;
    public bool Waiting = false;
}

public class BuildingData : MonoBehaviour
{
    #region Unity Public
    public string Name;
    public int lvl = 0;
    public Data[] ItemToAdd;
    #endregion

    public void Update()
    {
        // augemente Un item en fonction du temps

        for (int index = 0; index < ItemToAdd.Length; index ++)
        {
            if (!ItemToAdd[index].Waiting)
            {
                StartCoroutine(Wait(ItemToAdd[index]));
            }
        }
    }

    IEnumerator Wait(Data data)
    {
        data.Waiting = true;
        yield return new WaitForSeconds(data.TimeBetweenGive);
        data.item.NumberOfObject += data.AmoountToGive;
        data.Waiting = false;
    }
}
