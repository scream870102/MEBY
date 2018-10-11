using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public struct PlayState {
    //store gameMode
    public EGameMode gameMode;
    //store which map
    public EMap map;
    //store how many player in the game
    public int numOfPlayers;
    //store herotypes for game
    public List<EHero> heroes;
}
