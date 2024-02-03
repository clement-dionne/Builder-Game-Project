using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class DataSavedManager : MonoBehaviour
{
    public InputField NameInput;

    public string PlayerName = "";

    private string DataGame;
    public List<string> AllName = new List<string>();

    void Start()
    {
        WebClient webClient = new WebClient();
        DataGame = webClient.DownloadString("https://www.dropbox.com/s/wuhco6reeakcqij/PlayersData.txt?dl=1");
        foreach (string Ligne in DataGame.Split('$'))
        {
            if (Ligne.Split(':')[0] != "")
            {
                AllName.Add(Ligne.Split(':')[0]);
            }
        }
        NameInput.transform.gameObject.SetActive(true);
    }

    public void Update()
    {
        
    }

    public void SetPlayerName()
    {
        PlayerName = NameInput.text;
        foreach (string Name in AllName)
        {
            if (PlayerName == Name)
            {

            }
        }
    }
}
