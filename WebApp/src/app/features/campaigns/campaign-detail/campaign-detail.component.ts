import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CampaignService } from '../../../core/services/campaign.service';
import { CampaignDetail } from '../../../models/campaign.model';

@Component({
  selector: 'app-campaign-detail',
  templateUrl: './campaign-detail.component.html',
  styleUrls: ['./campaign-detail.component.css']
})
export class CampaignDetailComponent implements OnInit {
  campaign: CampaignDetail | null = null;
  loading = false;
  error: string = '';

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private campaignService: CampaignService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadCampaign(parseInt(id, 10));
    }
  }

  loadCampaign(id: number): void {
    this.loading = true;
    this.error = '';

    this.campaignService.getCampaignById(id).subscribe({
      next: (campaign) => {
        this.campaign = campaign;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load campaign';
        this.loading = false;
      }
    });
  }
}
