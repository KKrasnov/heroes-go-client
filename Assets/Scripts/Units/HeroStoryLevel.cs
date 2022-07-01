using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HeroStoryLevel
{
    public static readonly int[] Limits =
    {
        10,
        25,
        50,
        80,
        145,
        230,
        330
    };

    public static int MinPoints
    {
        get
        {
            return Limits[0];
        }
    }

    public static int MaxPoints
    {
        get
        {
            return Limits[Limits.Length - 1];
        }
    }

    public static int PointsToLevel(int points)
    {
        for (int i = 0; i < Limits.Length; i++)
        {
            if (points < Limits[i])
                return i;
        }
        return Limits.Length;
    }
}
