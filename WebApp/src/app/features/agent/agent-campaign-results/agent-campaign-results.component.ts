import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CampaignService } from '../../../core/services/campaign.service';
import { CampaignResult, CampaignResultFilter } from '../../../models/campaign-result.model';

@Component({
  selector: 'app-agent-campaign-results',
  templateUrl: './agent-campaign-results.component.html',
  styleUrls: ['./agent-campaign-results.component.css']
})
export class AgentCampaignResultsComponent implements OnInit {
  campaignId: number = 0;
  results: CampaignResult[] = [];
  filteredResults: CampaignResult[] = [];
  loading = false;
  error: string = '';
  
  filter: CampaignResultFilter = {};
  
  constructor(
    private route: ActivatedRoute,
    private campaignService: CampaignService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.campaignId = parseInt(id, 10);
      this.loadResults();
    }
  }

  loadResults(): void {
    this.loading = true;
    this.error = '';

    this.campaignService.getCampaignResults(this.campaignId).subscribe({
      next: (results) => {
        this.results = results;
        this.applyFilters();
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load campaign results';
        this.loading = false;
      }
    });
  }

  applyFilters(): void {
    this.filteredResults = [...this.results];

    if (this.filter.agentId) {
      this.filteredResults = this.filteredResults.filter(r => r.agentId === this.filter.agentId);
    }

    if (this.filter.agentName) {
      const agentNameLower = this.filter.agentName.toLowerCase();
      this.filteredResults = this.filteredResults.filter(r => 
        r.agentName?.toLowerCase().includes(agentNameLower)
      );
    }

    if (this.filter.rewardDate) {
      const rewardDateStr = new Date(this.filter.rewardDate).toDateString();
      this.filteredResults = this.filteredResults.filter(r => {
        if (!r.rewardDate) return false;
        return new Date(r.rewardDate).toDateString() === rewardDateStr;
      });
    }

    if (this.filter.purchaseDate) {
      const purchaseDateStr = new Date(this.filter.purchaseDate).toDateString();
      this.filteredResults = this.filteredResults.filter(r => {
        return new Date(r.purchaseDateTime).toDateString() === purchaseDateStr;
      });
    }
  }

  onFilterChange(): void {
    this.applyFilters();
  }

  clearFilters(): void {
    this.filter = {};
    this.applyFilters();
  }
}
