using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticBattleService : IFastBattleService
{
    private const float ARMY_POWERDOWN_COEF_MULTIPLIER = 0.75f;
    private const float DAMAGED_HERO_TO_UNITS_KILLS_RATIO = 0.5f;
    private const float BALANCE_MULTIPLIER_ADDITION = 0.1f;

    private Action<BattleResultData> _onBattleEnded;

    public void StartBattle(BattleSetupData battleSetup, Action<BattleResultData> onBattleEnded)
    {
        _onBattleEnded = onBattleEnded;
        ProccessBattle(battleSetup);
    }

    private void ProccessBattle(BattleSetupData battleSetup)
    {
        BattleResultData result = new BattleResultData();

        if (battleSetup.AllyArmy.ArmyForceRating >= battleSetup.EnemyArmy.ArmyForceRating)
        {
            result.IsVictory = true;
            result.Ally = PowerDownArmy(battleSetup.AllyArmy, ((float)battleSetup.EnemyArmy.ArmyForceRating / (float)battleSetup.AllyArmy.ArmyForceRating) * ARMY_POWERDOWN_COEF_MULTIPLIER);
            result.Enemy = WipeArmy(battleSetup.EnemyArmy);
        }
        else
        {
            result.IsVictory = false;
            result.Enemy = PowerDownArmy(battleSetup.EnemyArmy, ((float)battleSetup.AllyArmy.ArmyForceRating / (float)battleSetup.EnemyArmy.ArmyForceRating) * ARMY_POWERDOWN_COEF_MULTIPLIER);
            result.Ally = WipeArmy(battleSetup.AllyArmy);
        }

        EndBattle(result);
    }

    private void EndBattle(BattleResultData result)
    {
        _onBattleEnded(result);
        _onBattleEnded = null;
    }

    private ResultArmyData WipeArmy(ArmyData army)
    {
        ResultArmyData result = new ResultArmyData();
        result.SurvivedArmy = army;

        foreach (var hero in result.SurvivedArmy.Heroes)
        {
            foreach (var unit in hero.Squad)
            {
                result.DeadUnits.Add(unit);
            }
        }

        result.SurvivedArmy.WipeArmy();

        return result;
    }

    private ResultArmyData PowerDownArmy(ArmyData army, float coef)
    {
        ResultArmyData resultArmy = new ResultArmyData();
        resultArmy.SurvivedArmy = army;
        int testCounter = 0;

        int ratingBefore = army.ArmyForceRating;
        int difference = (int)(army.ArmyForceRating * coef);
        int ratingAfter = army.ArmyForceRating - difference;

        float balanceMultiplier = 1f;

        do
        {
            foreach(var hero in army.Heroes)
            {
                if(UnityEngine.Random.Range(0, 5) > 0)
                    hero.CurrentHP = hero.CurrentHP - (int)(hero.MaxHP * coef * DAMAGED_HERO_TO_UNITS_KILLS_RATIO);
            }

            int unitToKillForceRating = (int)(difference * balanceMultiplier);
            List<HeroData> heroesWithUnits = new List<HeroData>();
            foreach (var hero in army.Heroes)
            {
                if (hero.SquadSize > 0)
                {
                    bool isBalancedUnitsToKill = false;
                    foreach(var unit in hero.Squad)
                    {
                        if(unit.UnitForceRating < unitToKillForceRating)
                        {
                            isBalancedUnitsToKill = true;
                            break;
                        }
                    }
                    if(isBalancedUnitsToKill)
                        heroesWithUnits.Add(hero);
                }
            }

            if (heroesWithUnits.Count > 0)
            {
                int heroIndex = UnityEngine.Random.Range(0, heroesWithUnits.Count);
                List<UnitData> unitsToKill = new List<UnitData>();

                foreach(var unit in heroesWithUnits[heroIndex].Squad)
                {
                    if(unit.UnitForceRating < unitToKillForceRating)
                    {
                        unitsToKill.Add(unit);
                    }
                }

                int unitIndex = UnityEngine.Random.Range(0, unitsToKill.Count);

                resultArmy.DeadUnits.Add(heroesWithUnits[heroIndex].RemoveUnit(unitsToKill[unitIndex].ID));
            }

            balanceMultiplier += BALANCE_MULTIPLIER_ADDITION;

            testCounter++;
            if (testCounter >= 1000)
                throw new Exception("testCounter in AutomaticBattleService >= 1000!");
        }
        while (army.ArmyForceRating > ratingAfter);

        return resultArmy;
    }
}
