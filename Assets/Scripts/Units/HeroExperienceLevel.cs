public static class HeroExperienceLevel
{
    public static int[] Limits =
    {
        0,
        1000,
        2250,
        3800,
        5800,
        8300,
        11500,
        15500,
        20500,
        27000,
        35000,
        45000,
        57500,
        73000,
        92000,
        116000
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
