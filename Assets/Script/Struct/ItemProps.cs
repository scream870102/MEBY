using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public struct ItemProps {
    public EItem itemName;
    //define the item type
    public EItemType itemType;
    //if item is not an istant item then how long does the item effect exist
    public float continuousTime;
}
