using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UnitInfoWindowControllerData
    {
        public enum UnitOccupancy
        {
            None,
            Squad,
            Draft,
        }

        public UnitOccupancy Occupancy;

        public Guid ID;

        public Action<bool> OnViewed;
    }
}