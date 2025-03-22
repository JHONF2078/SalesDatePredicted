import { Component } from '@angular/core';
import { D3GrapicComponent } from '../components/d3-grapic/d3-grapic.component';

@Component({
  selector: 'app-graphic-page',
  standalone: true,
  imports: [D3GrapicComponent],
  templateUrl: './graphic-page.component.html',
  styleUrl: './graphic-page.component.css'
})
export class GraphicPageComponent {

}
