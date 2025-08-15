import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { CurrentTicket, Ticket } from '../models/ticket';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class QueueService {
  private apiUrl = environment.apiURL + '/queue';

  constructor(private http: HttpClient) {}

  createTicket(): Observable<ApiResponse<Ticket>> {
    return this.http.post<ApiResponse<Ticket>>(`${this.apiUrl}/tickets`, {});
  }

  currentTicket(): Observable<ApiResponse<CurrentTicket>> {
    return this.http.post<ApiResponse<CurrentTicket>>(
      `${this.apiUrl}/tickets/current`,
      {}
    );
  }

  resetQueue(): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/reset`, {});
  }
}
