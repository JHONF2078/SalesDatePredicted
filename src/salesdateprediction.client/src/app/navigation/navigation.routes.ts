import { Routes } from '@angular/router';

export const NAVIGATION_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/navigation-page.component')
        .then(m => m.NavigationPageComponent),
    children: [
      {
        path: '', // Redirigir a 'home' por defecto
        redirectTo: 'home',
        pathMatch: 'full',
      },
      {
        path: 'home', // Asegurar que el path sea accesible desde /
        loadComponent: () =>
          import('../home/pages/home/home.component')
            .then(m => m.HomeComponent),
      },
      {
        path: 'orders', // Asegurar que el path sea accesible desde /
        loadComponent: () =>
          import('../orders/pages/sale-date-prediction.component')
            .then(m => m.SaleDatePredictionComponent),
      }
    ]
  }
];
