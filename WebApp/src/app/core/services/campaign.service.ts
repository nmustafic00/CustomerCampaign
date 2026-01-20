import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CampaignBasic, CampaignDetail, CreateCampaignRequest, CampaignRewardRequest, RewardCustomerResponse } from '../../models/campaign.model';
import { CampaignResult } from '../../models/campaign-result.model';

@Injectable({
  providedIn: 'root'
})
export class CampaignService {
  private apiUrl = '/api/campaign'; // Use relative URL to go through proxy in development

  constructor(private http: HttpClient) {}

  getAllCampaigns(): Observable<CampaignBasic[]> {
    return this.http.get<CampaignBasic[]>(`${this.apiUrl}`);
  }

  getCampaignById(id: number): Observable<CampaignDetail> {
    return this.http.get<CampaignDetail>(`${this.apiUrl}/${id}`);
  }

  createCampaign(campaign: CreateCampaignRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/create`, campaign);
  }

  rewardCustomer(campaignId: number, reward: CampaignRewardRequest): Observable<RewardCustomerResponse> {
    return this.http.post<RewardCustomerResponse>(`${this.apiUrl}/${campaignId}/reward`, reward);
  }

  getCampaignResults(campaignId: number): Observable<CampaignResult[]> {
    return this.http.get<CampaignResult[]>(`${this.apiUrl}/results/${campaignId}`);
  }

  uploadCampaignCsv(campaignId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('csvFile', file);
    return this.http.post(`${this.apiUrl}/results/${campaignId}`, formData);
  }
}
