using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//skill shun is the personal skill for MASANARI
//this class inherit from ISkill
public class SkillShun : ISkill {
    //To store if player set the mark on somewhere
    private bool bMark;
    //store the mark position
    private Vector3 markPos = new Vector3 ( );
    protected override void Start ( ) {
        //include init isActive and timer and isInCD
        base.Start ( );
        //Get skill coolDown time Number from GameManager instance attribution
        coolDown = GameManager.instance.attribution.allSkillProps [(int) ESkill.SHUN].skillCD;
    }
    protected override void Update ( ) {
        //include timer accumulation isInCD setting
        base.Update ( );
        //if skill is active and cooldown finish
        if (bActive && !bInCD) {
            //if mark position not setting
            if (!bMark) {
                //Get playnumber string from parent(IPlayer) 
                if (Input.GetButtonDown (Parent.NumPlayer + buttonString)) {
                    //set the current position from gameObject
                    markPos = gameObject.transform.position;
                    bMark = true;
                }
            }
            //if mark position has already setting
            else if (bMark) {
                if (Input.GetButtonDown (Parent.NumPlayer + buttonString)) {
                    //set position to mark position
                    gameObject.transform.position = markPos;
                    bMark = false;
                    //make skill start to calculate cooldown
                    ResetCoolDown ( );
                }
            }
        }
    }
}
