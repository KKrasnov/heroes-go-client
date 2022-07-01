using System.Collections.Generic;
using System;
using UnityEngine;

public class MockGameMapService : IGameMapService
{
    private class MockBackendGameMapEntryData
    {
        public GameMapEntryData Entry;
        public bool IsDynamic = false;
        public bool IsIndividualGarrison = true;
        public bool IsViewWithEmptyGarrison = false;

        public Guid CurrentlyFightingUserId = Guid.Empty;

        /// <summary>
        /// key is user ID
        /// </summary>
        public Dictionary<Guid, ArmyData> SurvivedArmyForConcreteUser = new Dictionary<Guid, ArmyData>();

        public RewardData Reward = new RewardData();
    }

    private const float LAT_BOUND = 90;
    private const float LON_BOUND = 180;

    private Dictionary<Guid, MockBackendGameMapEntryData> Entries = new Dictionary<Guid, MockBackendGameMapEntryData>();

    private MockPlayerDataService _playerDataService;

    public MockGameMapService(IPlayerDataService playerDataService)
    {
        _playerDataService = (MockPlayerDataService)playerDataService;
        FillEntries();
    }

    private void FillEntries()
    {
        /*AddNewStaticEntry(new GameMapEntryData() {

            Fraction = FractionType.Neutral,
            OwnerUserId = Guid.NewGuid(),
            Type = GameMapEntryData.EntryType.Fortress,
            Lat_d = 49.801766f,
            Lon_d = 24.066f,
            EntryId = Guid.NewGuid()
        });*/

        //AddNewDynamicEntry(new GameMapEntryData()
        //{
        //        Fraction = FractionType.Neutral,
        //        OwnerUserId = Guid.NewGuid(),
        //        Type = GameMapEntryData.EntryType.Caravan,
        //        Lat_d = 49.8015f,
        //        Lon_d = 24.066f,
        //        EntryId = Guid.NewGuid()
        //});

        AddNewBackendEntry(new MockBackendGameMapEntryData()
        {
            Entry = new GameMapEntryData()
            {
                Fraction = FractionType.Orc,
                OwnerUserId = Guid.NewGuid(),
                Type = GameMapEntryData.EntryType.Person,
                Lat_d = 49.8015f,
                Lon_d = 24.066f,
                EntryId = Guid.NewGuid(),
                EntryNameKey = "orc_robbers_name",
                EntryDialogId = new Guid("00000000000000000000000000000001"),
                Garrison = new ArmyData(new HeroData[]
            {
                new HeroData()
                {
                    ID = new Guid("00000000000000000000000000030001"),
                    ExperiencePoints = HeroExperienceLevel.MinPoints,
                    StoryPoints = HeroStoryLevel.MinPoints,
                    Squad = new UnitData[]
                    {
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        }
                    }
                }
            })
            },
            Reward = new RewardData()
            {
                Gold = 100,
                ExperiencePoints = 1000
            }
        });

        AddNewBackendEntry(new MockBackendGameMapEntryData()
        {
            Entry = new GameMapEntryData()
            {
                Fraction = FractionType.Orc,
                OwnerUserId = Guid.NewGuid(),
                Type = GameMapEntryData.EntryType.Person,
                Lat_d = 49.801f,
                Lon_d = 24.066f,
                EntryId = Guid.NewGuid(),
                EntryNameKey = "orc_robbers_name",
                EntryDialogId = new Guid("00000000000000000000000000000001"),
                Garrison = new ArmyData(new HeroData[]
            {
                new HeroData()
                {
                    ID = new Guid("00000000000000000000000000030001"),
                    ExperiencePoints = HeroExperienceLevel.MinPoints,
                    StoryPoints = HeroStoryLevel.MinPoints,
                    Squad = new UnitData[]
                    {
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        },
                        new UnitData()
                        {
                            ID = new Guid("00000000000000000000000000030001")
                        }
                    }
                }
            })
            },
            Reward = new RewardData()
            {
                Gold = 100,
                ExperiencePoints = 1000
            }
        });
    }

    public List<GameMapEntryData> GetNearbyStaticEntries(Vector2 playerGeoPos)
    {
        return GetEntriesList(false);
    }

    public List<GameMapEntryData> GetNearbyDynamicEntries(Vector2 playerGeoPos)
    {
        return GetEntriesList(true);
    }

    private List<GameMapEntryData> GetEntriesList(bool isDynamic)
    {
        List<GameMapEntryData> entries = new List<GameMapEntryData>();

        foreach (var entry in Entries)
        {
            GameMapEntryData newEntry = entry.Value.Entry.GetDeepCopy();

            if (entry.Value.IsDynamic != isDynamic)
            {
                continue;
            }
            if (entry.Value.IsIndividualGarrison && entry.Value.SurvivedArmyForConcreteUser.ContainsKey(_playerDataService.GetPlayerId()))
            {
                if(!entry.Value.IsViewWithEmptyGarrison)
                    if(entry.Value.SurvivedArmyForConcreteUser[_playerDataService.GetPlayerId()].ArmyForceRating == 0)
                        continue;
                newEntry.Garrison = entry.Value.SurvivedArmyForConcreteUser[_playerDataService.GetPlayerId()].GetDeepCopy();
            }

            entries.Add(newEntry);
        }

        return entries;
    }

    public GameMapEntryData GetEntryData(Guid id)
    {
        if(Entries.ContainsKey(id))
            return Entries[id].Entry.GetDeepCopy();
        return null;
    }


    public bool RegisterEntryBattle(Guid entryId)
    {
        if (Entries[entryId].CurrentlyFightingUserId == Guid.Empty)
        {
            Entries[entryId].CurrentlyFightingUserId = _playerDataService.GetPlayerId();
            return true;
        }
        return false;
    }

    public RewardData CommitEntryBattleResult(Guid entryId, BattleResultData result)
    {
        if (Entries[entryId].CurrentlyFightingUserId != _playerDataService.GetPlayerId())
            throw new Exception("Entry battle was not registered with current user!");

        Entries[entryId].CurrentlyFightingUserId = Guid.Empty;

        UpdateEntryGarrison(entryId, result.Enemy.SurvivedArmy);
        _playerDataService.UpdatePlayerArmy(result.Ally.SurvivedArmy);
        _playerDataService.ApplyReward(Entries[entryId].Reward);
        

        return Entries[entryId].Reward;
    }

    private void UpdateEntryGarrison(Guid entryId, ArmyData army)
    {
        if(Entries[entryId].IsIndividualGarrison)
        {
            if(Entries[entryId].SurvivedArmyForConcreteUser.ContainsKey(_playerDataService.GetPlayerId()))
            {
                Entries[entryId].SurvivedArmyForConcreteUser[_playerDataService.GetPlayerId()] = army.GetDeepCopy();
            }
            else
            {
                Entries[entryId].SurvivedArmyForConcreteUser.Add(_playerDataService.GetPlayerId(), army.GetDeepCopy());
            }
        }
        else
        {
            Entries[entryId].Entry.Garrison = army.GetDeepCopy();
        }
    }

    private bool AddNewBackendEntry(MockBackendGameMapEntryData data)
    {
        Entries.Add(data.Entry.EntryId, data);
        return true;
    }

    private bool AddNewStaticEntry(GameMapEntryData data)
    {
        Entries.Add(data.EntryId, new MockBackendGameMapEntryData()
        {
            Entry = data,
            IsDynamic = false
        });
        return true;
    }

    private bool AddNewDynamicEntry(GameMapEntryData data)
    {
        Entries.Add(data.EntryId, new MockBackendGameMapEntryData()
        {
            Entry = data,
            IsDynamic = true
        });
        return true;
    }
}
