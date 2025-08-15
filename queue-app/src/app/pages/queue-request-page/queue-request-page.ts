import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { QueueService } from '../../services/queue-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-queue-request-page',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './queue-request-page.html',
  styleUrls: ['./queue-request-page.scss'],
})
export class QueueRequestPage {
  isLoading = false;
  errorMessage: string | null = null;

  constructor(private queueService: QueueService, private router: Router) {}

  getTicket() {
    this.isLoading = true;
    this.errorMessage = null;
    this.queueService.createTicket().subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.is_success && response.data) {
          this.router.navigate(['/ticket/display'], { state: { ticket: response.data } });
        } else {
          this.errorMessage = response.message || 'An unknown error occurred.';
        }
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = 'Failed to connect to the server. Please try again later.';
        console.error(err);
      },
    });
  }
}
