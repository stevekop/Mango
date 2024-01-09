using Mango.Services.RewardsAPI.Data;
using Mango.Services.RewardsAPI.Message;
using Mango.Services.RewardsAPI.Models;
using Mango.Services.RewardsAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Mango.Services.RewardsAPI.Services
{
    public class RewardService : IRewardService
    {
        private DbContextOptions<AppDBContext> _dbOptions;

        public RewardService(DbContextOptions<AppDBContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

      
        public async Task UpdateRewards(RewardMessage rewardMessage)
        {
            try
            {
                Rewards rewards = new()
                {
                    OrderId = rewardMessage.OrderId,
                    RewardsActivity =  rewardMessage.RewardsActivity,
                    UserId = rewardMessage.UserId,
                    RewardsDate = DateTime.Now,
                    };

                await using var _db = new AppDBContext(_dbOptions);
                await _db.Rewards.AddAsync(rewards);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}
