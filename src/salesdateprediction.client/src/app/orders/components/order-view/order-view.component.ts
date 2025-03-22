import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MATERIAL_IMPORTS } from '../../../material/material.component';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { OrdersService } from '../../service/orders.service';
import { ICustomerClients } from '../../interface/customer';

@Component({
  selector: 'app-order-view',
  standalone: true,
  imports: [CommonModule, ...MATERIAL_IMPORTS],
  templateUrl: './order-view.component.html',
  styleUrl: './order-view.component.scss'
})
export class OrderViewComponent {
  private subscriptions: Subscription = new Subscription();


  public dataSource = new MatTableDataSource<ICustomerClients>();

  @ViewChild(MatSort) order!: MatSort;
  @ViewChild(MatPaginator) pagination!: MatPaginator;

  displayedColumns: string[] = ['orderId', 'requiredDate', 'shippedDate', 'shipName', 'shipAddress', 'shipCity']; // Columnas mostradas en la tabla

  constructor(
    private ordersService: OrdersService) { }

  ngOnInit(): void {
    this.loadOrdersByCustomer();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  applyFilter(value: string) {
    this.dataSource.filter = value.trim().toLowerCase();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.order;
    this.dataSource.paginator = this.pagination;
  }

  loadOrdersByCustomer(): void {
    const CustomerOrdersSubscription = this.ordersService.getOrdersByCustomer(79).subscribe(ICustomerClients => {
      this.dataSource.data = ICustomerClients;
    });

    this.subscriptions.add(CustomerOrdersSubscription);
  }

}
