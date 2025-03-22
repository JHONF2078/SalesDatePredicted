import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, of, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from '../interface/product';


@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  private apiUrl = `${environment.apiUrl}/Products`;

  constructor(private http: HttpClient) {
    this.loadProducts();
  }

  private shippersSubject = new BehaviorSubject<Product[]>([]);

  products$ = this.shippersSubject.asObservable();


  private loadProducts(): void {
    this.http.get<Product[]>(this.apiUrl).subscribe(products => {
      this.shippersSubject.next(products);
    });
  }

  getProducts(): Observable<Product[]> {
    return this.products$; // Se retorna directamente sin redundancias
  }
}
