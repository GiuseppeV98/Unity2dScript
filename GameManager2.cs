using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
public enum GamePhase
{
    OutBattle,   // Fase al di fuori della battaglia
    ChooseMoveOut, // Fase di scelta
    InBattle,    // Fase in battaglia
    EndBattle    // Fase di fine battaglia
}
public class GameManager2 : MonoBehaviour
{
     public class PlayerCl
    {
        public string Name { get; set; }     // Nome del giocatore
       // public bool IsSelected { get; set; } // Flag per indicare se è selezionato
        public string Position{get; set;}
        public PlayerCl(string name)
        {
            Name = name;
            //IsSelected = false;
            Position = "-1 -1";
        }
    }
    public Tilemap myTilemap;
    //public unwalkable unW;
    public PlayerCl[] playerArr; // Array di oggetti utile per la selezione/deselezione personaggio
    public GamePhase currentPhase; // Fase attuale del gioco
    PlayerManager2[] PlayerManagers; // array di componenti Player Manager degli oggetti Player
    GameObject[] players; 
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length > 0) 
        {
            PlayerManagers = new PlayerManager2[players.Length];
            playerArr = new PlayerCl[players.Length];

        for (int x = 0; x < players.Length; x++)
            {
                playerArr[x] = new PlayerCl(players[x].name);//quest assegnazione non va bene
                Debug.Log(players[x].name);
                PlayerManagers[x] = players[x].GetComponent<PlayerManager2>();
            }     
        }
        else {
    Debug.Log("Nessun oggetto con il tag 'Player' trovato.");
}
    }
}
