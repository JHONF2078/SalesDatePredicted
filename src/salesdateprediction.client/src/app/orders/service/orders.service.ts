import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Order } from '../interface/order';
import { ICustomerOrders } from '../interface/customer';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  private apiUrl = `${environment.apiUrl}/Orders`;

  constructor(private http: HttpClient) { }

  private ordersSubject = new BehaviorSubject<Order[]>([]);

  orders$ = this.ordersSubject.asObservable();

  /**
   * Obtener órdenes por cliente.
   * @param customerId ID del cliente.
   * @returns Observable con las órdenes del cliente.
   */
  getOrdersByCustomer(customerId: number): Observable<ICustomerOrders[]> {
    const url = `${this.apiUrl}/ByCustomer/${customerId}`;
    return this.http.get<ICustomerOrders[]>(url);
  }


  /**
   * Agregar una nueva orden.
   * @param order Orden a agregar.
   * @returns Observable con la orden creada.
   */
  addOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, order).pipe(
      tap(newOrder => {
        const updatedOrders = [...this.ordersSubject.value, newOrder];
        this.ordersSubject.next(updatedOrders);
      })
    );
  }
}
