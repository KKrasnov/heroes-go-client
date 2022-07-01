using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockUnitsConfigurationService : IUnitsConfigurationService
{
    private Dictionary<Guid, UnitConfiguration> _unitsConfiguration = new Dictionary<Guid, UnitConfiguration>();
    private Dictionary<Guid, HeroConfiguration> _heroesConfiguration = new Dictionary<Guid, HeroConfiguration>();

    public MockUnitsConfigurationService()
    {
        FillConfiguration();
    }

    private void FillConfiguration()
    {
        FillUnits();
        FillHeroes();
    }

    private void FillUnits()
    {
        AddUnitConfiguration(new UnitConfiguration()
        {
            ID = new Guid("00000000000000000000000000010001"),
            NameKey = "human_peasant_infantry",
            PreviewSpritePath = "Sprites/Heroes/Human_Warrior_1",
            Fraction = FractionType.Human,
            Rarity = 1,
            AttackPower = 25,
            MaxHP = 200
        });
        AddUnitConfiguration(new UnitConfiguration()
        {
            ID = new Guid("00000000000000000000000000010002"),
            NameKey = "human_peasant_bowman",
            PreviewSpritePath = "Sprites/Heroes/velena",
            Fraction = FractionType.Human,
            Rarity = 1,
            AttackPower = 50,
            MaxHP = 100
        });
        AddUnitConfiguration(new UnitConfiguration()
        {
            ID = new Guid("00000000000000000000000000010003"),
            NameKey = "human_bully",
            PreviewSpritePath = "Sprites/Heroes/Human_Warrior_1",
            Fraction = FractionType.Human,
            Rarity = 2,
            AttackPower = 40,
            MaxHP = 250
        });

        AddUnitConfiguration(new UnitConfiguration()
        {
            ID = new Guid("00000000000000000000000000030001"),
            NameKey = "goblin_worker",
            PreviewSpritePath = "Sprites/Units/Orc_Warrior_1",
            Fraction = FractionType.Orc,
            Rarity = 1,
            AttackPower = 20,
            MaxHP = 250
        });
    }

    private void FillHeroes()
    {
        AddHeroConfiguration(new HeroConfiguration()
        {
            ID = new Guid("00000000000000000000000000010001"),
            NameKey = "olgerd_the_warrior",
            PreviewSpritePath = "Sprites/Heroes/Human_Warrior_1",
            Fraction = FractionType.Human,
            BaseAttackPower = 150,
            BaseHP = 1000,
            SquadLimit = 20
        });
        AddHeroConfiguration(new HeroConfiguration()
        {
            ID = new Guid("00000000000000000000000000010002"),
            NameKey = "velena_furious_arrow",
            PreviewSpritePath = "Sprites/Heroes/velena",
            Fraction = FractionType.Human,
            BaseAttackPower = 200,
            BaseHP = 750,
            SquadLimit = 20
        });

        AddHeroConfiguration(new HeroConfiguration()
        {
            ID = new Guid("00000000000000000000000000030001"),
            NameKey = "gro-bagh_the_crusher",
            PreviewSpritePath = "Sprites/Heroes/gro-bagh",
            Fraction = FractionType.Orc,
            BaseAttackPower = 200,
            BaseHP = 800,
            SquadLimit = 10
        });
    }

    public UnitConfiguration GetUnitConfiguration(Guid id)
    {
        return _unitsConfiguration[id];
    }

    public HeroConfiguration GetHeroConfiguration(Guid id)
    {
        return _heroesConfiguration[id];
    }

    private void AddUnitConfiguration(UnitConfiguration unitConfig)
    {
        _unitsConfiguration.Add(unitConfig.ID, unitConfig);
    }

    private void AddHeroConfiguration(HeroConfiguration heroConfig)
    {
        _heroesConfiguration.Add(heroConfig.ID, heroConfig);
    }
}
