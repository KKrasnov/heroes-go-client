using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using General;

public class UnitConfiguration : IIdentifiable
{
    public Guid ID
    {
        get;
        set;
    }

    public string NameKey;
    public string PreviewSpritePath;

    public FractionType Fraction;
    public int Rarity;

    public int MaxHP;
    public int AttackPower;
}
