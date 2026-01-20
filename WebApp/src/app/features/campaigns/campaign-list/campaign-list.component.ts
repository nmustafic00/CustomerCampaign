import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CampaignService } from '../../../core/services/campaign.service';
import { AuthService } from '../../../core/services/auth.service';
import { CampaignBasic } from '../../../models/campaign.model';

@Component({
  selector: 'app-campaign-list',
  templateUrl: './campaign-list.component.html',
  styleUrls: ['./campaign-list.component.css']
})
export class CampaignListComponent implements OnInit {
  campaigns: CampaignBasic[] = [];
  loading = false;
  error: string = '';
  userRole: string | null = '';

  constructor(
    private campaignService: CampaignService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userRole = this.authService.getUserRole();
    this.loadCampaigns();
  }

  get isAdmin(): boolean {
    return this.userRole === 'Admin';
  }

  get isAgent(): boolean {
    return this.userRole === 'Agent';
  }

  loadCampaigns(): void {
    this.loading = true;
    this.error = '';

    this.campaignService.getAllCampaigns().subscribe({
      next: (campaigns) => {
        this.campaigns = campaigns;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load campaigns';
        this.loading = false;
      }
    });
  }

  viewCampaign(id: number): void {
    this.router.navigate(['/campaigns', id]);
  }

  viewResults(id: number): void {
    this.router.navigate(['/campaigns', id, 'results']);
  }

  uploadCsv(id: number): void {
    this.router.navigate(['/campaigns', id, 'upload-csv']);
  }

  viewAgentResults(id: number): void {
    this.router.navigate(['/campaigns', id, 'agent-results']);
  }
}
