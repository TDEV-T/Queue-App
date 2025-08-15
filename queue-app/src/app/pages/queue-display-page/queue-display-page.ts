import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Ticket } from '../../models/ticket';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-queue-display-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './queue-display-page.html',
  styleUrls: ['./queue-display-page.scss'],
})
export class QueueDisplayPage implements OnInit {
  ticket: Ticket | null = null;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state && navigation.extras.state['ticket']) {
      this.ticket = navigation.extras.state['ticket'] as Ticket;
    }
  }

  ngOnInit(): void {
    if (!this.ticket) {
      this.router.navigate(['/']);
    }
  }
}
