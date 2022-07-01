using System;
using System.Collections.Generic;

public class GameMapEntryData
{
    public enum EntryType
    {
        Headquarters,
        Fortress,
        Caravan,
        Person
    }

    public Guid EntryId;

    public EntryType Type;
    public FractionType Fraction;
    public float Lat_d, Lon_d;

    public Guid OwnerUserId = new Guid("00000000000000000000000000000000");

    public DateTime ExpirationTime = DateTime.MaxValue;

    public int Variation = 0;

    public string EntryNameKey;

    public Guid EntryDialogId;

    public ArmyData Garrison;

    public GameMapEntryData GetDeepCopy()
    {
        GameMapEntryData newInst = (GameMapEntryData)this.MemberwiseClone();
        newInst.Garrison = this.Garrison.GetDeepCopy();
        return newInst;
    }
}
