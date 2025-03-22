import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, of, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Order } from '../interface/order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  private apiUrl = `${environment.apiUrl}/Orders`; // URL base obtenida de environment

  constructor(private http: HttpClient) {
    this.loadOrders();
  }

  private ordersSubject = new BehaviorSubject<Order[]>([]);

  orders$ = this.ordersSubject.asObservable();


  private loadOrders(): void {
    this.http.get<Order[]>(this.apiUrl).subscribe(books => {
      this.ordersSubject.next(books);
    });
  }

  getOrders(): Observable<Order[]> {
    return this.orders$; // Se retorna directamente sin redundancias
  }


  addOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, order).pipe(
      tap(newBook => {
        const updatedOrder = [...this.ordersSubject.value, newBook];
        this.ordersSubject.next(updatedOrder);
      })
    );
  }
}
