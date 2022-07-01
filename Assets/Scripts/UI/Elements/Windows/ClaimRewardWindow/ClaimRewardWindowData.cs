using System;

namespace Assets.Scripts.UI
{
    public class ClaimRewardWindowData
    {
        public RewardData Reward;
        public Action OnViewed;

        public ClaimRewardWindowData(RewardData reward, Action onViewed)
        {
            Reward = reward;
            OnViewed = onViewed;
        }
    }
}