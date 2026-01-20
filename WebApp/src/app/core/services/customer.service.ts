import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CustomerPreview } from '../../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = '/api/customers'; // Use relative URL to go through proxy in development

  constructor(private http: HttpClient) {}

  getCustomerPreview(customerId: string): Observable<CustomerPreview> {
    return this.http.get<CustomerPreview>(`${this.apiUrl}/${customerId}/preview`);
  }
}
