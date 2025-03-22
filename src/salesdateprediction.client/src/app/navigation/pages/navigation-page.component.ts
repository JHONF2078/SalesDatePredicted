import { Component, HostListener, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from '../components/header/header.component';
import { MenuComponent } from '../components/menu/menu.component';
import { FooterComponent } from '../components/footer/footer.component';
import { MATERIAL_IMPORTS } from '../../material/material.component';
import { MatSidenav } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-navigation-page',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HeaderComponent,
    MenuComponent,
    FooterComponent,
    ...MATERIAL_IMPORTS

  ],
  templateUrl: './navigation-page.component.html',
  styleUrls: ['./navigation-page.component.scss']
})
export class NavigationPageComponent {
  @ViewChild('sidenav') sidenav!: MatSidenav;
  isMobile = window.innerWidth < 768; // Detecta si es móvil al cargar

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.isMobile = window.innerWidth < 768;
  }

  closeSidenav() {
    if (this.isMobile) {
      this.sidenav.close();
    }
  }

}
