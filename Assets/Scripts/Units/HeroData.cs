using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class HeroData : IIdentifiable
{
    private HeroConfiguration _Configuration
    {
        get
        {
            if (_configuration == null)
                _configuration = CompositionRoot.Container.Resolve<IUnitsConfigurationService>().GetHeroConfiguration(ID);
            return _configuration;
        }
    }

    private HeroConfiguration _configuration;

    public int TotalForceRating
    {
        get
        {
            return HeroForceRating + SquadForceRating;
        }
    }

    public int HeroForceRating
    {
        get
        {
            return GetHeroForceRating();
        }
    }

    /*public int InventoryForceRating
    {
        get
        {
            
        }
    }*/

    public int SquadForceRating
    {
        get
        {
            return GetSquadForceRating();
        }
    }

    public int SquadSize
    {
        get
        {
            return Squad.Length;
        }
    }

    public int MaxHP
    {
        get
        {
            return _Configuration.BaseHP;
        }
    }

    public int CurrentAttackPower
    {
        get
        {
            return _Configuration.BaseAttackPower;
        }
    }

    public FractionType Fraction
    {
        get
        {
            return _Configuration.Fraction;
        }
    }

    public string NameKey
    {
        get
        {
            return _Configuration.NameKey;
        }
    }

    public int SquadLimit
    {
        get 
        {
            return _Configuration.SquadLimit;
        }
    }

    public string PreviewSpritePath
    {
        get
        {
            return _Configuration.PreviewSpritePath;
        }
    }

    public int StoryLevel
    {
        get
        {
            return HeroStoryLevel.PointsToLevel(StoryPoints);
        }
    }

    public int ExperienceLevel
    {
        get
        {
            return HeroExperienceLevel.PointsToLevel(ExperiencePoints);
        }
    }

    #region Response variables
    private int _currentHP;

    public int CurrentHP
    {
        get
        {
            if (_currentHP == -1)
                _currentHP = MaxHP;
            return _currentHP;
        }
        set
        {
            _currentHP = Mathf.Clamp(value, 0, MaxHP);
        }
    }

    public int StoryPoints
    {
        get;
        set;
    }

    public int ExperiencePoints
    {
        get;
        set;
    }

    public Guid ID
    {
        get;
        set;
    }

    public UnitData[] Squad;
    #endregion

    public HeroData()
    {
        _currentHP = -1;
    }

    public HeroData(int currentHP)
    {
        _currentHP = currentHP;
    }

    public HeroData(Guid id)
    {
        ID = id;
        _currentHP = MaxHP;
    }

    public UnitData RemoveUnit(Guid unitId)
    {
        List<UnitData> unitList = new List<UnitData>(Squad);
        int removedUnitIndex = unitList.FindIndex(unit => unit.ID == unitId);
        UnitData removedUnit = unitList[removedUnitIndex];
        unitList.RemoveAt(removedUnitIndex);
        Squad = unitList.ToArray();
        return removedUnit;
    }

    public HeroData GetDeepCopy()
    {
        HeroData newInst = (HeroData)this.MemberwiseClone();
        newInst.Squad = new UnitData[this.Squad.Length];

        for(int i = 0; i < newInst.Squad.Length; i++)
        {
            newInst.Squad[i] = this.Squad[i].GetDeepCopy();
        }

        return newInst;
    }

    private int GetHeroForceRating()
    {
        return (CurrentAttackPower * (CurrentHP / MaxHP)) + CurrentHP;
    }

    private int GetSquadForceRating()
    {
        int rating = 0;
        foreach(var unit in Squad)
        {
            rating += unit.UnitForceRating;
        }
        return rating;
    }

    #region Comaprsion

    public static int CompareByTotalRating(HeroData x, HeroData y)
    {
        if (x.TotalForceRating > y.TotalForceRating)
            return -1;
        else if (y.TotalForceRating > x.TotalForceRating)
            return 1;
        else
            return 0;
    }

    #endregion
}
