using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Place
{
    public GameObject place;
    public Material DefaultMaterials;
}

public class PlayerSelector : MonoBehaviour
{
    #region Unity Public
    public Material Selected;
    public Material Normal;
    public Material Disabled;
    public Place[] Places;
    public GameObject MenuPanel;
    public GameObject CurrentBuilding = null;
    public Dictionary dictionary;
    public GameObject SettingsPanel;
    public AudioManager Audio;

    public int CurrentChunk = 0;
    #endregion

    private GameObject[] P;
    private GameObject[] Chunk;

    void Start()
    {
        MenuPanel.SetActive(false);
        P = GameObject.FindGameObjectsWithTag("Place");
        Chunk = GameObject.FindGameObjectsWithTag("Chunk");
        int index = 0;

        foreach (GameObject p in P)
        {
                Places[index].place = p;
                Places[index].DefaultMaterials = p.GetComponent<MeshRenderer>().material;
                index += 1;
        }

        foreach (GameObject chunk in Chunk)
        {
            if (chunk.name != "MiniChunk0")
            {
                for (int id = 0; id < chunk.transform.childCount; id ++)
                {
                    foreach (Place place in Places)
                    {
                        if (chunk.transform.GetChild(id).gameObject == place.place)
                        {
                            chunk.transform.GetChild(id).GetComponent<MeshRenderer>().material = Disabled; 
                            place.DefaultMaterials = Disabled;
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        // RayCast pour select une Place

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        for (int index = 0; index < Places.Length; index++)
        {
            if (Physics.Raycast(ray, out hit) && !MenuPanel.activeInHierarchy && !SettingsPanel.activeInHierarchy)
            {
                if (hit.transform.name == "Place" && Places[index].DefaultMaterials != Disabled)
                {
                    // Colorier la place selectionner

                    if (index < P.Length)
                    {
                        if (hit.transform.gameObject == Places[index].place)
                        {
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material = Selected;

                            // Build sur la place select

                            if (Input.GetMouseButton(0))
                            {
                                if (CheckCanBuild(hit.transform.gameObject) && CurrentBuilding != null)
                                {
                                    Build(hit.transform.gameObject, CurrentBuilding);
                                }
                                else
                                {
                                    // ouvrir un menu du building ??
                                }
                            }
                        }
                        else
                        {
                            Places[index].place.GetComponent<MeshRenderer>().material = Places[index].DefaultMaterials;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && dictionary.InventoryButton.activeInHierarchy)
        {
            OpenCloseInventory();
        }
    }

    // ouvrir le menu du jeu

    public void OpenCloseInventory()
    {
        if (MenuPanel.activeInHierarchy) MenuPanel.SetActive(false);
        else MenuPanel.SetActive(true);
        Audio.ClickEffect();
    }

    // verif si possible de build

    public bool CheckCanBuild(GameObject Place)
    {
        if (Place.transform.childCount == 0) return true;
        return false;
    }

    // Build sur la place

    public void Build(GameObject Place, GameObject Building)
    {
        Instantiate(Building,Place.transform);
        CurrentBuilding = null;
        Audio.BuildEffect();
    }

    public void ChunkUnlocker(int ChunkNumber)
    {
        Audio.LevelUpEffect();
        foreach (GameObject chunk in Chunk)
        {
            if (chunk.name == "MiniChunk" + ChunkNumber.ToString())
            {
                for (int id = 0; id < chunk.transform.childCount; id++)
                {
                    foreach (Place place in Places)
                    {
                        if (chunk.transform.GetChild(id).gameObject == place.place)
                        {
                            chunk.transform.GetChild(id).GetComponent<MeshRenderer>().material = Normal;
                            place.DefaultMaterials = Normal;
                        }
                    }
                }
            }
        }
    }
}