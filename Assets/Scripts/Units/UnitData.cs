using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class UnitData : IIdentifiable
{
    private UnitConfiguration _Configuration
    {
        get
        {
            if (_configuration == null)
                _configuration = CompositionRoot.Container.Resolve<IUnitsConfigurationService>().GetUnitConfiguration(ID);
            return _configuration;
        }
    }

    private UnitConfiguration _configuration;

    public int UnitForceRating
    {
        get
        {
            return GetUnitForceRating();
        }
    }

    public int AttackPower
    {
        get
        {
            return _Configuration.AttackPower;
        }
    }

    public int MaxHP
    {
        get
        {
            return _Configuration.MaxHP;
        }
    }

    public string NameKey
    {
        get
        {
            return _Configuration.NameKey;
        }
    }

    public string PreviewSpritePath
    {
        get
        {
            return _Configuration.PreviewSpritePath;
        }
    }

    public int Rarity
    {
        get
        {
            return _Configuration.Rarity;
        }
    }

    #region Response variables
    public Guid ID
    {
        get;
        set;
    }

    public Guid InstanceID
    {
        get;
        set;
    }
    #endregion
    
    public UnitData()
    {
        InstanceID = Guid.NewGuid();
    }

    public UnitData(Guid id) : this()
    {
        ID = id;
    }

    public UnitData GetDeepCopy()
    {
        UnitData newInst = (UnitData)this.MemberwiseClone();
        return newInst;
    }

    private int GetUnitForceRating()
    {
        return MaxHP + AttackPower;
    }

    #region Comparsion

    public static int CompareByRating(UnitData x, UnitData y)
    {
        if (x.UnitForceRating > y.UnitForceRating)
            return -1;
        else if (y.UnitForceRating > x.UnitForceRating)
            return 1;
        else
            return 0;
    }

    #endregion
}
