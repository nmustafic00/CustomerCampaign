import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CampaignService } from '../../../core/services/campaign.service';
import { CreateCampaignRequest } from '../../../models/campaign.model';

@Component({
  selector: 'app-create-campaign',
  templateUrl: './create-campaign.component.html',
  styleUrls: ['./create-campaign.component.css']
})
export class CreateCampaignComponent {
  campaignForm: FormGroup;
  loading = false;
  error: string = '';

  constructor(
    private fb: FormBuilder,
    private campaignService: CampaignService,
    public router: Router
  ) {
    this.campaignForm = this.fb.group({
      name: ['', [Validators.required]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      dailyLimitPerAgent: ['', [Validators.required, Validators.min(1)]]
    });
  }

  onSubmit(): void {
    if (this.campaignForm.valid) {
      this.loading = true;
      this.error = '';

      const campaignRequest: CreateCampaignRequest = this.campaignForm.value;

      this.campaignService.createCampaign(campaignRequest).subscribe({
        next: (campaign) => {
          this.router.navigate(['/campaigns', campaign.id]);
        },
        error: (error) => {
          this.error = error.error?.message || 'Failed to create campaign';
          this.loading = false;
        }
      });
    }
  }
}
