export interface CampaignResult {
  customerId: number;
  fullName: string;
  dateOfBirth?: string;
  age: number;
  amountWithoutDiscount: number;
  discountAmount: number;
  amountWithDiscount: number;
  purchaseDateTime: string;
  agentId?: number;
  agentName?: string;
  rewardDate?: string;
}

export interface CampaignResultFilter {
  agentId?: number;
  agentName?: string;
  rewardDate?: string;
  purchaseDate?: string;
}
