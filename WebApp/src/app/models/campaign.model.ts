export interface CampaignBasic {
  id: number;
  name: string;
  startDate: string;
  endDate: string;
}

export interface CampaignDetail {
  id: number;
  name: string;
  startDate: string;
  endDate: string;
  dailyLimitPerAgent: number;
}

export interface CreateCampaignRequest {
  name: string;
  startDate: string;
  endDate: string;
  dailyLimitPerAgent: number;
}

export interface CampaignRewardRequest {
  customerId: number;
}

export interface RewardCustomerResponse {
  id: number;
  status: string;
  message?: string;
}
