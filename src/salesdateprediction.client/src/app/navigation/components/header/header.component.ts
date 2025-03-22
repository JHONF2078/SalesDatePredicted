import { Component, EventEmitter, Output } from '@angular/core';
import { MATERIAL_IMPORTS } from '../../../material/material.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, ...MATERIAL_IMPORTS],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Output() toggleMenu = new EventEmitter<void>();

}
