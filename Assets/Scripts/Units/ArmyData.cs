using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyData
{
    public int ArmyForceRating
    {
        get
        {
            return GetArmyForceRating();
        }
    }

    public int TotalUnitsCount
    {
        get
        {
            return GetTotalUnitsCount();
        }
    }

    public HeroData[] Heroes;

    public ArmyData()
    {
    }

    public ArmyData(HeroData[] heroes)
    {
        Heroes = heroes;
    }

    public void WipeArmy()
    {
        foreach(HeroData hero in Heroes)
        {
            hero.CurrentHP = 0;
            hero.Squad = new UnitData[0];
        }
    }

    public ArmyData GetDeepCopy()
    {
        ArmyData newInst = (ArmyData)this.MemberwiseClone();
        newInst.Heroes = new HeroData[this.Heroes.Length];
        
        for(int i = 0; i < newInst.Heroes.Length; i++)
        {
            newInst.Heroes[i] = this.Heroes[i].GetDeepCopy();
        }
        return newInst;
    }

    private int GetArmyForceRating()
    {
        int rating = 0;
        foreach(var hero in Heroes)
        {
            rating += hero.TotalForceRating;
        }
        return rating;
    }

    private int GetTotalUnitsCount()
    {
        int counter = 0;
        foreach(var hero in Heroes)
        {
            counter += hero.SquadSize;
        }
        return counter;
    }
}
