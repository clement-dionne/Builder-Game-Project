using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HDVTable
{
    public Item objet;
    public int ValueOfTheItem;
    public bool CanBeShow;
    public bool AlreadyShow;
}

public class HDV : MonoBehaviour
{
    #region Unity Public
    public Dictionary dictionary;
    public HDVTable[] HdvTable;
    public Item Coin;
    public GameObject ButtonSellItem;
    public GameObject HDVContent;
    public InputField NumberToSell;
    public GameObject ImageInTheButton;
    public AudioManager Audio;
    #endregion

    private int LastValue;

    public void Update()
    {
        if (NumberToSell.text != "")
        {
            if (LastValue != Convert.ToInt32(NumberToSell.text))
            {
                LastValue = Convert.ToInt32(NumberToSell.text);
                ResetHDV();
            }
        }
        else
        {
            if (LastValue != 1)
            {
                LastValue = 1;
                ResetHDV();
            }
        }

        for (int index = 0; index < HdvTable.Length; index ++)
        {
            if (NumberToSell.text != "")
            {
                if (HdvTable[index].objet.NumberOfObject >= Convert.ToInt32(NumberToSell.text))
                {
                    HdvTable[index].CanBeShow = true;
                    // print(Convert.ToInt32(NumberToSell.text));
                }
            }
            else
            {
                if (HdvTable[index].objet.NumberOfObject >= 1)
                {
                    HdvTable[index].CanBeShow = true;
                    // print(1);
                }
            }

            if (HdvTable[index].CanBeShow && !HdvTable[index].AlreadyShow)
            {
                HdvTable[index].AlreadyShow = true;

                GameObject button = Instantiate(ButtonSellItem, HDVContent.transform);
                button.tag = "HDVBut";
                button.name = HdvTable[index].objet.name;
                button.GetComponent<Image>().sprite = HdvTable[index].objet.ItemSprite;

                int a = index; // pourquoi sa marche pas ??? JSP !

                button.GetComponent<Button>().onClick.AddListener(() => ChangeToCoins(a));
                button.GetComponent<Button>().onClick.AddListener(() => Audio.BuySellEffect());

                GameObject image = Instantiate(ImageInTheButton, button.transform);
                image.GetComponent<Image>().sprite = Coin.ItemSprite;
                image.GetComponentInChildren<Text>().text = HdvTable[index].ValueOfTheItem.ToString();
            }
        }
    }

    public void ChangeToCoins(int index)
    {
        // elever nb de l'item
        if (NumberToSell.text != "")
        {
            HdvTable[index].objet.NumberOfObject -= Convert.ToInt32(NumberToSell.text);
            Coin.NumberOfObject += (Convert.ToInt32(NumberToSell.text) * HdvTable[index].ValueOfTheItem);
        }
        else
        {
            HdvTable[index].objet.NumberOfObject -= 1;
            Coin.NumberOfObject += HdvTable[index].ValueOfTheItem;
        }

        dictionary.RestetInventory();
        ResetHDV();
    }

    public void ResetHDV()
    {
        for (int index = 0; index < HdvTable.Length; index++)
        {
            HdvTable[index].AlreadyShow = false;
            HdvTable[index].CanBeShow = false;
        }

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("HDVBut");
        foreach (GameObject but in buttons)
        {
            Destroy(but);
        }
        buttons = null;
    }
}