using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Needed
{
    public Item Object;
    public int Cost;
}

[System.Serializable]
public class Batiment
{
    public Item Building;
    public Needed[] ObjectNeed;
    public bool AlreadyShow = false;
    public bool CanBeBuy = false;
}

public class Dictionary : MonoBehaviour
{
    #region Unity Public
    public PlayerSelector playerSelector;
    public GameObject Content;
    public GameObject ButtonInventory;
    public GameObject ImageForNeed;
    public Batiment[] Batiments;
    public Item[] objet;
    public GameObject TextObjetContent;
    public GameObject TextObjet;
    public Item Coins;
    public AudioManager Audio;

    public GameObject TimeSpeedSelector;
    public HDV Hdv;

    public GameObject InventoryButton;
    #endregion

    private int LastValue = 0;

    public void Start()
    {
        foreach (Item o in objet)
        {
            GameObject t = Instantiate(TextObjet, TextObjetContent.transform);
            t.GetComponent<Text>().text = o.name + " : " + o.NumberOfObject;
            t.tag = "ItemText";
            t.name = o.name + "ItemText";
        }

        Time.timeScale = 1;
        InventoryButton.SetActive(false);
    }

    public void Update()
    {
        if (LastValue != Coins.NumberOfObject)
        {
            LastValue = Coins.NumberOfObject;
            RestetInventory();
        }

        // afficher les batiments disponible a l'achat

        for (int Bindex = 0; Bindex < Batiments.Length; Bindex++)
        {
            Batiments[Bindex].CanBeBuy = true;
            for (int index = 0; index < Batiments[Bindex].ObjectNeed.Length; index ++)
            {
                if (Batiments[Bindex].ObjectNeed[index].Object.NumberOfObject < Batiments[Bindex].ObjectNeed[index].Cost)
                {
                    Batiments[Bindex].CanBeBuy = false;
                }
            }

            if (Batiments[Bindex].CanBeBuy && !Batiments[Bindex].AlreadyShow)
            {
                GameObject button = Instantiate(ButtonInventory, Content.transform);
                button.tag = "InvBut";
                button.name = Batiments[Bindex].Building.name;
                button.GetComponent<Image>().sprite = Batiments[Bindex].Building.ItemSprite;

                int a = Bindex; // pourquoi sa marche pas ??? JSP !

                button.GetComponent<Button>().onClick.AddListener(() => Buy(a));

                for (int index = 0; index < Batiments[Bindex].ObjectNeed.Length; index++)
                {
                    GameObject image = Instantiate(ImageForNeed, button.transform);
                    image.GetComponent<Image>().sprite = Batiments[Bindex].ObjectNeed[index].Object.ItemSprite;
                    image.GetComponentInChildren<Text>().text = Batiments[Bindex].ObjectNeed[index].Cost.ToString();
                }
                Batiments[Bindex].AlreadyShow = true;
            }
        }

        GameObject[] objecttext = GameObject.FindGameObjectsWithTag("ItemText");
        foreach (GameObject text in objecttext)
        {
            foreach (Item o in objet)
            {
                if (text.name == o.name + "ItemText")
                {
                    text.GetComponent<Text>().text = o.name + " : " + o.NumberOfObject;
                }
            }
        }

        Time.timeScale = TimeSpeedSelector.GetComponent<Slider>().value;

        if (playerSelector.CurrentBuilding == null)
        {
            InventoryButton.SetActive(true);
        }
    }

    // selectioner les Buildings et
    // retirer aux items les bonnes valeurs
    public void Buy(int Bindex)
    {
        playerSelector.CurrentBuilding = Batiments[Bindex].Building.BuildingGameObject;

        for (int index = 0; index < Batiments[Bindex].ObjectNeed.Length; index++)
        {
            Batiments[Bindex].ObjectNeed[index].Object.NumberOfObject -= Batiments[Bindex].ObjectNeed[index].Cost;
        }

        if (Batiments[Bindex].Building.name == "ChunkCount")
        {
            playerSelector.CurrentChunk += 1;
            playerSelector.ChunkUnlocker(playerSelector.CurrentChunk);
            Batiments[Bindex].Building.NumberOfObject += 1;
            Batiments[Bindex].ObjectNeed[0].Cost = Mathf.FloorToInt((Batiments[Bindex].ObjectNeed[0].Cost * 0.33f) + Batiments[Bindex].ObjectNeed[0].Cost);
        }
        else
        {
            Audio.BuySellEffect();
        }

        RestetInventory();
        Hdv.ResetHDV();

        playerSelector.MenuPanel.SetActive(false);
        InventoryButton.SetActive(false);
    }

    public void RestetInventory()
    {
        for (int index = 0; index < Batiments.Length; index++)
        {
            Batiments[index].AlreadyShow = false;
        }

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("InvBut");
        foreach (GameObject but in buttons)
        {
            Destroy(but);
        }
        buttons = null;
    }
}