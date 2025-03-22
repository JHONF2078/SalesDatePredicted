import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Order } from '../interface/order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  private apiUrl = `${environment.apiUrl}/Orders`; // URL base obtenida de environment

  constructor(private http: HttpClient) { }

  private ordersSubject = new BehaviorSubject<Order[]>([]);

  orders$ = this.ordersSubject.asObservable();

  /**
   * Cargar todas las órdenes desde la API.
   */
  loadOrders(): void {
    this.http.get<Order[]>(this.apiUrl).subscribe(orders => {
      this.ordersSubject.next(orders);
    });
  }

  /**
   * Obtener todas las órdenes como un observable.
   */
  getOrders(): Observable<Order[]> {
    return this.orders$;
  }

  /**
   * Obtener órdenes por cliente.
   * @param customerId ID del cliente.
   * @returns Observable con las órdenes del cliente.
   */
  getOrdersByCustomer(customerId: number): Observable<Order[]> {
    const url = `${this.apiUrl}/ByCustomer/${customerId}`;
    return this.http.get<Order[]>(url);
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
