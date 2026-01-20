import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { CreateAgentRequest } from '../../../models/agent.model';

@Component({
  selector: 'app-create-agent',
  templateUrl: './create-agent.component.html',
  styleUrls: ['./create-agent.component.css']
})
export class CreateAgentComponent {
  agentForm: FormGroup;
  loading = false;
  error: string = '';
  success: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    public router: Router
  ) {
    this.agentForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.agentForm.valid) {
      this.loading = true;
      this.error = '';
      this.success = '';

      const agentData: CreateAgentRequest = this.agentForm.value;

      this.authService.createAgent(agentData).subscribe({
        next: () => {
          this.success = 'Agent created successfully!';
          this.agentForm.reset();
          this.loading = false;
        },
        error: (error) => {
          this.error = error.error?.message || 'Failed to create agent';
          this.loading = false;
        }
      });
    }
  }
}
