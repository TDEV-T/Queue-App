import { Routes } from '@angular/router';
import { QueueRequestPage } from './pages/queue-request-page/queue-request-page';
import { QueueDisplayPage } from './pages/queue-display-page/queue-display-page';
import { QueueManagePage } from './pages/queue-manage-page/queue-manage-page';

export const routes: Routes = [
  { path: '', component: QueueRequestPage },

  {
    path: 'ticket',
    children: [
      {
        path: 'display',
        component: QueueDisplayPage,
      },
      {
        path: 'manage',
        component: QueueManagePage,
      },
    ],
  },

  { path: '**', redirectTo: '', pathMatch: 'full' },
];
