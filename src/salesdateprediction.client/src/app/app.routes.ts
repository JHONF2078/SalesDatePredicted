import { Routes } from '@angular/router';


export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./navigation/navigation.routes') // standalone route file
        .then(m => m.NAVIGATION_ROUTES)
  }
];
