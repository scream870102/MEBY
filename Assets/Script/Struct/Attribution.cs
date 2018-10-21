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

    public List<Color> allColors;
}
