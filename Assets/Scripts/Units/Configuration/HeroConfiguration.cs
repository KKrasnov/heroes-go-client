using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using General;

public class HeroConfiguration : IIdentifiable
{
    public Guid ID
    {
        get;
        set;
    }

    public string NameKey;

    public FractionType Fraction;

    public int SquadLimit;

    public string PreviewSpritePath;

    public int BaseHP;
    //public int BaseMP;
    public int BaseAttackPower;

    //choose one of following (or both, but it's complicated to develop and balance:

    /*public int HPGainPerLevel;
    public int MPGainPerLevel;
    public int AttackPowerGainPerLevel;*/

    /*public int BaseStrengthAmount;
    public int BaseAgilityAmount;
    public int BaseIntelligenceAmount;

    public int StrengthGainPerLevel;
    public int AgilityGainPerLevel;
    public int IntelligenceGainPerLevel;*/

}
