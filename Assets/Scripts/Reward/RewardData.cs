using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;

public class RewardData
{
    public int Gold = 0;
    public int ExperiencePoints = 0;

    public ValuePair<Guid, int>[] HeroStoryPoints;

    public UnitData[] Units;
}
