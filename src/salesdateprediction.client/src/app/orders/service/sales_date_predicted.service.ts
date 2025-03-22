import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, of, tap } from 'rxjs';
import { SalesDatePrediction } from '../interface/orders-date-prediction';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class SalesDatePredictedService {

  private apiUrl = `${environment.apiUrl}/Customers/WithOrderInfo`;

  constructor(private http: HttpClient) {
    this.loadSalesDatePredictionss();
  }

  private booksSubject = new BehaviorSubject<SalesDatePrediction[]>([]);

  ListSalesDatePredictions$ = this.booksSubject.asObservable();


  private loadSalesDatePredictionss(): void {
    this.http.get<SalesDatePrediction[]>(this.apiUrl).subscribe(books => {
      this.booksSubject.next(books);
    });
  }

  getSalesDatePredictions(): Observable<SalesDatePrediction[]> {
    return this.ListSalesDatePredictions$; // Se retorna directamente sin redundancias
  }
}
