import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { CampaignListComponent } from './features/campaigns/campaign-list/campaign-list.component';
import { CampaignDetailComponent } from './features/campaigns/campaign-detail/campaign-detail.component';
import { CreateCampaignComponent } from './features/campaigns/create-campaign/create-campaign.component';
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';
import { CreateAgentComponent } from './features/admin/create-agent/create-agent.component';
import { CampaignResultsComponent } from './features/admin/campaign-results/campaign-results.component';
import { UploadCsvComponent } from './features/admin/upload-csv/upload-csv.component';
import { CustomerRewardComponent } from './features/agent/customer-reward/customer-reward.component';
import { AgentCampaignResultsComponent } from './features/agent/agent-campaign-results/agent-campaign-results.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'campaigns',
    component: CampaignListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'campaigns/:id',
    component: CampaignDetailComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'campaigns/create',
    component: CreateCampaignComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'campaigns/:id/results',
    component: CampaignResultsComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'campaigns/:id/upload-csv',
    component: UploadCsvComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'admin/create-agent',
    component: CreateAgentComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'agent/reward-customer',
    component: CustomerRewardComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Agent'] }
  },
  {
    path: 'campaigns/:id/agent-results',
    component: AgentCampaignResultsComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Agent'] }
  },
  { path: '**', redirectTo: '/dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
