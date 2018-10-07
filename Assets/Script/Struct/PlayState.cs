using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public struct PlayState {
    public EGameMode gameMode;
    public EMap map;
    public int numOfPlayers;
    public List<EHero> heroes;

}
