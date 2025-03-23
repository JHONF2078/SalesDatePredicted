import { AfterViewInit, Component, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MATERIAL_IMPORTS } from '../../../material/material.component';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { OrdersService } from '../../service/orders.service';
import { ICustomerOrders } from '../../interface/customer';
import { Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SalesDatePrediction } from '../../interface/orders-date-prediction';

export class MyCustomPaginatorIntl extends MatPaginatorIntl {
  override itemsPerPageLabel = 'Rows per page';
}

@Component({
  selector: 'app-order-view',
  standalone: true,
  imports: [CommonModule, ...MATERIAL_IMPORTS],
  templateUrl: './order-view.component.html',
  styleUrl: './order-view.component.scss',
  providers: [
    { provide: MatPaginatorIntl, useClass: MyCustomPaginatorIntl }
  ]
})
export class OrderViewComponent {
  private subscriptions: Subscription = new Subscription();
  public dataSource = new MatTableDataSource<ICustomerOrders>();
  customerName: string = '';
  custId: number = 0;

  @ViewChild(MatSort) order!: MatSort;
  @ViewChild(MatPaginator) pagination!: MatPaginator;

  displayedColumns: string[] = ['orderId', 'requiredDate', 'shippedDate', 'shipName', 'shipAddress', 'shipCity']; // Columnas mostradas en la tabla

  constructor(
    public dialogRef: MatDialogRef<OrderViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { customerOrders: ICustomerOrders; salesDatePrediction: SalesDatePrediction },
    private ordersService: OrdersService,
    private router: Router) {
    this.customerName = data?.salesDatePrediction?.customerName;
    this.custId = data?.salesDatePrediction?.custId;
  }

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
    const CustomerOrdersSubscription = this.ordersService.getOrdersByCustomer(this.custId).subscribe(ICustomerClients => {
      this.dataSource.data = ICustomerClients;
    });

    this.subscriptions.add(CustomerOrdersSubscription);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

}
