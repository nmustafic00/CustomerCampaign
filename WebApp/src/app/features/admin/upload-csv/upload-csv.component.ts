import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CampaignService } from '../../../core/services/campaign.service';

@Component({
  selector: 'app-upload-csv',
  templateUrl: './upload-csv.component.html',
  styleUrls: ['./upload-csv.component.css']
})
export class UploadCsvComponent implements OnInit {
  campaignId: number = 0;
  selectedFile: File | null = null;
  loading = false;
  error: string = '';
  success: string = '';

  constructor(
    private route: ActivatedRoute,
    private campaignService: CampaignService,
    public router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.campaignId = parseInt(id, 10);
    }
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      if (file.type === 'text/csv' || file.name.endsWith('.csv')) {
        this.selectedFile = file;
        this.error = '';
      } else {
        this.error = 'Please select a CSV file';
        this.selectedFile = null;
      }
    }
  }

  uploadFile(): void {
    if (!this.selectedFile) {
      this.error = 'Please select a CSV file';
      return;
    }

    this.loading = true;
    this.error = '';
    this.success = '';

    this.campaignService.uploadCampaignCsv(this.campaignId, this.selectedFile).subscribe({
      next: () => {
        this.success = 'CSV file uploaded successfully!';
        this.selectedFile = null;
        this.loading = false;
      },
      error: (error) => {
        this.error = error.error?.message || 'Failed to upload CSV file';
        this.loading = false;
      }
    });
  }
}
