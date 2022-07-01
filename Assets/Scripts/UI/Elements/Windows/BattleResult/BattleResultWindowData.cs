using System;

namespace Assets.Scripts.UI
{
    public class BattleResultWindowData 
    {
        public BattleResultData Result;

        public Action OnViewed;

        public BattleResultWindowData(BattleResultData result, Action onViewed)
        {
            Result = result;
            OnViewed = onViewed;
        }
    }
}