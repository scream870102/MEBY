using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public struct ItemProps {
    //define the item type
    public EItem itemType;
    //if item is an is instant item
    public bool bInstantItem;
    //if item is not an istant item then how long does the item effect exist
    public float continuousTime;
}
