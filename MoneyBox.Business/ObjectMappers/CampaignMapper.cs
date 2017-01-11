using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBox.Business.DTO;

namespace MoneyBox.Business.ObjectMappers
{
    public class CampaignMapper
    {
        public static CampaignDTO ToDTO(Campaign campaign)
        {
            return new CampaignDTO
            {
                Id = campaign.Id,
                Name = campaign.Name,
                CompanyId = campaign.CompanyId,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                DiscountPercentage = campaign.DiscountPercentage
            };
        }

        public static Campaign ToModel(CampaignDTO campaignDTO)
        {
            return new Campaign
            {
                Id = campaignDTO.Id,
                Name = campaignDTO.Name,
                CompanyId = campaignDTO.CompanyId,
                StartDate = campaignDTO.StartDate,
                EndDate = campaignDTO.EndDate, 
                DiscountPercentage = campaignDTO.DiscountPercentage
            };
        }

        public static List<CampaignDTO> ToDTOList(List<Campaign> list)
        {
            return list.Select(ToDTO).ToList();
        }

        public static List<Campaign> ToModelList(List<CampaignDTO> list)
        {
            return list.Select(ToModel).ToList();
        }
    }
}
