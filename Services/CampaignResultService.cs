using CsvHelper.Configuration;
using CsvHelper;
using CustomerCampaign.DTOs;
using CustomerCampaign.Infrastructure.Integrations.Soap;
using CustomerCampaign.Services.Interfaces;
using System.Globalization;
using System;
using System.Text;
using CustomerCampaign.Data;
using Microsoft.EntityFrameworkCore;
using CustomerCampaign.Entities;

namespace CustomerCampaign.Services
{
    public class CampaignResultService : ICampaignResultService
    {
        private readonly CustomerCampaignDbContext _dbContext;
        private readonly ISoapCustomerClient _soapClient;

        public CampaignResultService(CustomerCampaignDbContext dbContext, ISoapCustomerClient soapClient)
        {
            _dbContext = dbContext;
            _soapClient = soapClient;
        }

        public async Task<IEnumerable<CampaignResult>> SaveCsvAsync(int campaignId, IFormFile csvFile)
        {
            List<PurchaseCsvRowDto> csvRows;

            using (var reader = new StreamReader(csvFile.OpenReadStream(), Encoding.UTF8))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            }))
            {
                csvRows = csv.GetRecords<PurchaseCsvRowDto>().ToList();
            }

            var rewards = await _dbContext.AgentRewardEntries
                .Where(r => r.CampaignId == campaignId)
                .ToListAsync();

            var rowsToSave = new List<CampaignResult>();

            foreach (var row in csvRows)
            {
                if (!int.TryParse(row.CustomerId, out var customerIdInt))
                    continue; 

                var reward = rewards.FirstOrDefault(r => r.CustomerId == customerIdInt); 
                if (reward == null) continue; 

                rowsToSave.Add(new CampaignResult
                {
                    CampaignId = campaignId,
                    CustomerId = customerIdInt,
                    PurchaseDateTime = row.PurchaseDateTime,
                    AmountWithoutDiscount = row.AmountWithoutDiscount,
                    DiscountAmount = row.DiscountAmount,
                    AmountWithDiscount = row.AmountWithDiscount
                });
            }

            _dbContext.CampaignResults.AddRange(rowsToSave);
            await _dbContext.SaveChangesAsync();

            return rowsToSave;
        }

        public async Task<IEnumerable<CampaignResultDto>> GetCampaignResultsAsync(int campaignId)
        {
            var results = await _dbContext.CampaignResults
                .Where(r => r.CampaignId == campaignId)
                .ToListAsync();

            var mergedResults = new List<CampaignResultDto>();

            foreach (var row in results)
            {
                var customerPreview = await _soapClient.FindPersonAsync(row.CustomerId.ToString());
                var reward = await _dbContext.AgentRewardEntries
                    .Where(r => r.CampaignId == campaignId && r.CustomerId == row.CustomerId)
                    .FirstOrDefaultAsync();

                string agentName = null;
                if (reward != null)
                {
                    var agentUser = await _dbContext.Users
                        .FirstOrDefaultAsync(u => u.Id == reward.AgentId);
                    agentName = agentUser?.Username;
                }

                mergedResults.Add(new CampaignResultDto
                {
                    CustomerId = row.CustomerId,
                    FullName = customerPreview?.FullName ?? "",
                    DateOfBirth = customerPreview?.DateOfBirth,
                    Age = customerPreview?.Age ?? 0,
                    AgentId = reward?.AgentId,
                    AgentName = agentName,
                    RewardDate = reward?.RewardDate,
                    PurchaseDateTime = row.PurchaseDateTime,
                    AmountWithoutDiscount = row.AmountWithoutDiscount,
                    DiscountAmount = row.DiscountAmount,
                    AmountWithDiscount = row.AmountWithDiscount
                });
            }

            return mergedResults;
        }
    }
}
