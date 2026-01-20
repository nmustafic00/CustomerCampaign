import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './features/auth/login/login.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { CampaignListComponent } from './features/campaigns/campaign-list/campaign-list.component';
import { CampaignDetailComponent } from './features/campaigns/campaign-detail/campaign-detail.component';
import { CreateCampaignComponent } from './features/campaigns/create-campaign/create-campaign.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { CreateAgentComponent } from './features/admin/create-agent/create-agent.component';
import { CampaignResultsComponent } from './features/admin/campaign-results/campaign-results.component';
import { UploadCsvComponent } from './features/admin/upload-csv/upload-csv.component';
import { CustomerRewardComponent } from './features/agent/customer-reward/customer-reward.component';
import { AgentCampaignResultsComponent } from './features/agent/agent-campaign-results/agent-campaign-results.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    CampaignListComponent,
    CampaignDetailComponent,
    CreateCampaignComponent,
    NavbarComponent,
    CreateAgentComponent,
    CampaignResultsComponent,
    UploadCsvComponent,
    CustomerRewardComponent,
    AgentCampaignResultsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
