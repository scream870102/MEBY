using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public struct Attribution {
    //store all prefab for hero object
    public List<GameObject> allHeroPrefab;
    //set all hero props 
    public List<PlayerProps> allHeroProps;
    //set all skill props
    public List<SkillProps> allSkillProps;
    //ref for all color
    public List<Color> allColors;
    //set all itemProps
    public List<ItemProps> allItemProps;
    //ref for all map prefab
    public List<GameObject> allMapPrefab;

    public float itemSpawnIntervalTime;
    public int maxItemNum;
}
