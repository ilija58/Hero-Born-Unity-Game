using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Character
{
    public Weapon weapon;


    public Paladin(string name, Weapon weapon) : base(name)
    {
        this.weapon = weapon;
    }

    public override void printStatsInfo()
    {
        Debug.LogFormat("Hail {0} - take up your {1}", name, weapon.name);
    }
}
