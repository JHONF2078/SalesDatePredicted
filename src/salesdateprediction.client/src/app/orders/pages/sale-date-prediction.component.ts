import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DatePredicionListComponent } from "../components/date-predicion-list/date-predicion-list.component";

@Component({
  selector: 'app-sale-date-prediction',
  standalone: true,
  imports: [CommonModule, DatePredicionListComponent],
  templateUrl: './sale-date-prediction.component.html',
  styleUrls: ['./sale-date-prediction.component.scss']
})
export class SaleDatePredictionComponent {

}
