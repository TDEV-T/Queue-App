import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { QueueService } from '../../services/queue-service';

@Component({
  selector: 'app-queue-manage-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './queue-manage-page.html',
  styleUrls: ['./queue-manage-page.scss'],
})
export class QueueManagePage implements OnInit {
  currentTicketNumber: string | null = null;
  isLoading = true; // Start in loading state

  constructor(
    private queueService: QueueService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.getCurrentTicketNumber();
  }

  getCurrentTicketNumber() {
    this.isLoading = true;
    this.queueService.currentTicket().subscribe({
      next: (response) => {
        if (response.is_success) {
          this.currentTicketNumber = response.data.current_ticket_number;
          console.log('Current ticket number:', this.currentTicketNumber);
        } else {
          console.error('Failed to fetch current ticket:', response.message);
          this.currentTicketNumber = 'N/A';
        }
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching current ticket:', err);
        this.currentTicketNumber = 'N/A';
        this.isLoading = false;
      },
    });
  }

  clearQueue() {
    this.queueService.resetQueue().subscribe({
      next: (response) => {
        if (response.is_success) {
          this.currentTicketNumber = '00';
          this.cdr.detectChanges();
        } else {
          console.error('Failed to clear the queue:', response.message);
        }
      },
      error: (err) => {
        console.error('Error clearing the queue:', err);
      },
    });
  }
}
