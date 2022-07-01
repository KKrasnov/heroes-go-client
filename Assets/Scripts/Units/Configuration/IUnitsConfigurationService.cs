using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitsConfigurationService
{
    UnitConfiguration GetUnitConfiguration(Guid id);

    HeroConfiguration GetHeroConfiguration(Guid id);
}
