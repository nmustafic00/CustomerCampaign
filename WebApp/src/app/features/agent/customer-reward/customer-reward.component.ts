import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CampaignService } from '../../../core/services/campaign.service';
import { CustomerService } from '../../../core/services/customer.service';
import { CustomerPreview } from '../../../models/customer.model';
import { CampaignBasic } from '../../../models/campaign.model';

@Component({
  selector: 'app-customer-reward',
  templateUrl: './customer-reward.component.html',
  styleUrls: ['./customer-reward.component.css']
})
export class CustomerRewardComponent implements OnInit {
  allCampaigns: CampaignBasic[] = [];
  campaigns: CampaignBasic[] = [];
  selectedCampaignId: number | null = null;
  customerId: string = '';
  customerPreview: CustomerPreview | null = null;
  loadingCustomer = false;
  rewarding = false;
  error: string = '';
  success: string = '';

  searchForm: FormGroup;

  constructor(
    private campaignService: CampaignService,
    private customerService: CustomerService,
    private fb: FormBuilder,
    public router: Router
  ) {
    this.searchForm = this.fb.group({
      campaignId: ['', [Validators.required]],
      customerId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadCampaigns();
  }

  loadCampaigns(): void {
    this.campaignService.getAllCampaigns().subscribe({
      next: (campaigns) => {
        this.allCampaigns = campaigns;
        // this.filterActiveCampaigns();
      },
      error: (error) => {
        this.error = 'Failed to load campaigns';
      }
    });
  }

  /*
  filterActiveCampaigns(): void {
    const now = new Date();
 
  this.campaigns = this.allCampaigns.filter(c => {
      const start = new Date(c.startDate);
      const end = new Date(c.endDate);
      return now >= start && now <= end;
    });
  }
  */
  
  

  searchCustomer(): void {
    if (this.searchForm.valid) {
      this.loadingCustomer = true;
      this.error = '';
      this.success = '';
      this.customerPreview = null;

      const formValue = this.searchForm.value;
      this.selectedCampaignId = formValue.campaignId;
      const customerId = formValue.customerId;

      this.customerService.getCustomerPreview(customerId).subscribe({
        next: (customer) => {
          this.customerPreview = customer;
          this.loadingCustomer = false;
        },
        error: (error) => {
          this.error = error.error?.message || 'Customer not found';
          this.loadingCustomer = false;
        }
      });
    }
  }

  rewardCustomer(): void {
    if (!this.selectedCampaignId || !this.customerPreview) {
      return;
    }

    this.rewarding = true;
    this.error = '';
    this.success = '';

    const customerIdNum = parseInt(this.customerPreview.customerId, 10);

    this.campaignService.rewardCustomer(this.selectedCampaignId, {
      customerId: customerIdNum
    }).subscribe({
      next: (response) => {
        this.success = 'Customer rewarded successfully!';
        this.rewarding = false;
        // Clear customer preview after successful reward
        setTimeout(() => {
          this.customerPreview = null;
          this.searchForm.patchValue({ customerId: '' });
        }, 2000);
      },
      error: (error) => {
        this.error = error.error?.message || 'Failed to reward customer';
        this.rewarding = false;
      }
    });
  }
}
