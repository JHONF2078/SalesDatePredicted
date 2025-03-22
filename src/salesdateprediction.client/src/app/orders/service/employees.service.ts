import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, of, tap } from 'rxjs';
import { Employee } from '../interface/employee';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  private apiUrl = `${environment.apiUrl}/Employees`;

  constructor(private http: HttpClient) {
    this.loadEmployees();
  }

  private employeesSubject = new BehaviorSubject<Employee[]>([]);

  employees$ = this.employeesSubject.asObservable();


  private loadEmployees(): void {
    this.http.get<Employee[]>(this.apiUrl).subscribe(employees => {
      this.employeesSubject.next(employees);
    });
  }

  getEmployees(): Observable<Employee[]> {
    return this.employees$; // Se retorna directamente sin redundancias
  }

}
