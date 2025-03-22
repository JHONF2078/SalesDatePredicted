import { Component, EventEmitter, HostListener, Output } from '@angular/core';
import { MATERIAL_IMPORTS } from '../../../material/material.component';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [...MATERIAL_IMPORTS, RouterModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent {
  @Output() closeMenu = new EventEmitter<void>();

  isMobile = window.innerWidth < 768;

  constructor(private router: Router) { }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.isMobile = window.innerWidth < 768;
  }


  handleClick() {
    if (this.isMobile) {
      this.closeMenu.emit();
    }
  }

  isActive(route: string): boolean {
    return this.router.url === route;
  }

}
