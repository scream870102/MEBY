using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public struct PlayerProps {
    //hero type
    public EHero hero;
    //what color it is
    public EColor color;
    //hero weight
    //ref 5.f
    public float mass;
    //basic health
    public float basicHealth;
    //basic attack range
    public float basicAttackRange;
    //basic attack speed
    public float basicAttackSpeed;
    //basic attack point
    public float basicAttackPoint;
    //player move speed
    //ref 300.0f
    public float basicSpeed;
    //speed for player in air
    //ref 150.0f
    public float airSpeed;
    //how many times can player jump without touch ground
    public int numMaxJump;
    //force to add when player jump
    //ref 2000.0f
    public float jumpForce;
    //radius for player to detect if it touch ground
    //ref .2f
    public float groundRadius;
    //determine if player can move horizontal in air state
    public bool isAirControl;
}
