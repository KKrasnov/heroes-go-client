using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MockPlayerDataService : IPlayerDataService
{
    private int _goldAmount = 0;
    private int _experienceAmount = 0;
    private FractionType _fraction;
    private Guid _playerId;
    private ArmyData _army;
    private UnitData[] _unitsDraft;

    public MockPlayerDataService()
    {
        _playerId = new Guid("00000000000000000000000000001111");
        _goldAmount = 1000;
        _experienceAmount = 0;
        _fraction = FractionType.Human;
        FillHeroes();
    }

    public FractionType GetFraction()
    {
        return _fraction;
    }

    public int GetGoldAmount()
    {
        return _goldAmount;
    }

    /*public int GetCrystalsAmount()
    {

    }*/

    public ArmyData GetArmyData()
    {
        return _army.GetDeepCopy();
    }

    public HeroData GetHeroData(Guid heroId)
    {
        foreach(var hero in _army.Heroes)
        {
            if (hero.ID == heroId)
                return hero.GetDeepCopy();
        }

        throw new NullReferenceException("No hero with id " + heroId + " was found!");
    }

    public UnitData GetUnitData(Guid instanceId)
    {
        foreach(var hero in _army.Heroes)
        {
            foreach(var unit in hero.Squad)
            {
                if (unit.InstanceID == instanceId)
                    return unit.GetDeepCopy();
            }
        }

        throw new NullReferenceException("No unit with instance id " + instanceId + " was found!");
    }

    public UnitData[] GetUnitsDraft()
    {
        return _unitsDraft;
    }

    public void AddGold(int amount)
    {
        _goldAmount += amount;
    }

    public void AddExperience(int amount)
    {
        _experienceAmount += amount;
    }

    /*public void AddCrystals(int amount)
    {

    }*/

    #region MOCK Services Interaction

    public Guid GetPlayerId()
    {
        return _playerId;
    }

    public void UpdatePlayerArmy(ArmyData army)
    {
        _army = army.GetDeepCopy();
    }

    public void ApplyReward(RewardData reward)
    {
        AddGold(reward.Gold);
        AddExperience(reward.ExperiencePoints);
    }

    #endregion

    private void FillHeroes()
    {
        List<HeroData> heroes = new List<HeroData>();
        heroes.Add(new HeroData() {
            ID = new Guid("00000000000000000000000000010001"),
            ExperiencePoints = HeroExperienceLevel.MinPoints,
            StoryPoints = HeroStoryLevel.MinPoints,
            Squad = new UnitData[]
            {
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010001")
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010003"),
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010003"),
                },
                new UnitData()
                {
                    ID = new Guid("00000000000000000000000000010003"),
                }
            }
        });

        heroes.Add(new HeroData()
        {
            ID = new Guid("00000000000000000000000000010002"),
            ExperiencePoints = HeroExperienceLevel.MinPoints,
            StoryPoints = HeroStoryLevel.MinPoints,
            Squad = new UnitData[]
            {
                new UnitData(new Guid("00000000000000000000000000010001")),
                new UnitData(new Guid("00000000000000000000000000010001")),
                new UnitData(new Guid("00000000000000000000000000010001")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002")),
                new UnitData(new Guid("00000000000000000000000000010002"))
            }
        });

        _army = new ArmyData(heroes.ToArray());
    }
}
