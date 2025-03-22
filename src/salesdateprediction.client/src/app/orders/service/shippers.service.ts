import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, of, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Shipper } from '../interface/shipper';

@Injectable({
  providedIn: 'root'
})
export class ShippersService {

  private apiUrl = `${environment.apiUrl}/Shippers`;

  constructor(private http: HttpClient) {
    this.loadShippers();
  }

  private shippersSubject = new BehaviorSubject<Shipper[]>([]);

  shippers$ = this.shippersSubject.asObservable();


  private loadShippers(): void {
    this.http.get<Shipper[]>(this.apiUrl).subscribe(shippers => {
      this.shippersSubject.next(shippers);
    });
  }

  getShippers(): Observable<Shipper[]> {
    return this.shippers$; // Se retorna directamente sin redundancias
  }

}
