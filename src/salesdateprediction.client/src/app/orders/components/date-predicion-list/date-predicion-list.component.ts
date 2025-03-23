import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MATERIAL_IMPORTS } from '../../../material/material.component';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { SalesDatePredictedService } from '../../service/sales_date_predicted.service';
import { SalesDatePrediction } from '../../interface/orders-date-prediction';
import { Order } from '../../interface/order';
import { OrdersService } from '../../service/orders.service';
import { OrderCreateComponent } from '../order-create/order-create.component';
import { Router } from '@angular/router';
import { ICustomerOrders } from '../../interface/customer';
import { OrderViewComponent } from '../order-view/order-view.component';

export class MyCustomPaginatorIntl extends MatPaginatorIntl {
  override itemsPerPageLabel = 'Rows per page';
}

@Component({
  selector: 'app-date-predicion-list',
  standalone: true,
  imports: [CommonModule, ...MATERIAL_IMPORTS],
  templateUrl: './date-predicion-list.component.html',
  styleUrl: './date-predicion-list.component.scss',
  providers: [
    { provide: MatPaginatorIntl, useClass: MyCustomPaginatorIntl }
  ]
})
export class DatePredicionListComponent implements OnInit, OnDestroy, AfterViewInit {
  private subscriptions: Subscription = new Subscription();


  public dataSource = new MatTableDataSource<SalesDatePrediction>();

  @ViewChild(MatSort) order!: MatSort;
  @ViewChild(MatPaginator) pagination!: MatPaginator;

  displayedColumns: string[] = ['customerName', 'lastOrderDate', 'possibleNextOrderDate', 'actions']; // Columnas mostradas en la tabla

  constructor(private salesDatePredictedService: SalesDatePredictedService,
    private ordersService: OrdersService, private router: Router, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadSalesDatePredictions();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  applyFilter(value: string): void {
    if (value.trim() === '') {
      this.loadSalesDatePredictions(); // Si el campo está vacío, recarga todos los datos
      return;
    }

    const searchValue = value.trim().toLocaleUpperCase();
    const searchSubscription = this.salesDatePredictedService.searchSalesDatePredictions(searchValue)
      .subscribe(filteredPredictions => {
        this.dataSource.data = filteredPredictions; // Actualiza la tabla con los resultados filtrados
      });

    this.subscriptions.add(searchSubscription);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.order;
    this.dataSource.paginator = this.pagination;
  }

  loadSalesDatePredictions(): void {
    const SalesDatePredictionsSubscription = this.salesDatePredictedService.getSalesDatePredictions().subscribe(SalesDatePredictions => {
      this.dataSource.data = SalesDatePredictions;
    });

    this.subscriptions.add(SalesDatePredictionsSubscription);
  }

  openDialog(salesDatePrediction?: SalesDatePrediction, order?: Order): void {
    const dialogRef = this.dialog.open(OrderCreateComponent, {
      width: '700px',
      data: {
        order: order || {},
        salesDatePrediction: salesDatePrediction || {}
      },
      autoFocus: true,
      restoreFocus: true
    });

    const dialogSubscription = dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        console.log('Datos transformados enviados al servicio:', result);

        // Llamar al servicio para guardar la orden
        const addSubscription = this.ordersService.addOrder(result).subscribe(() => {
          this.loadSalesDatePredictions(); // Recargar la lista después de guardar
        });

        this.subscriptions.add(addSubscription);
      }
    });

    this.subscriptions.add(dialogSubscription);
  }


  openDialogView(salesDatePrediction?: SalesDatePrediction, customerOrders?: ICustomerOrders): void {
    const dialogRef = this.dialog.open(OrderViewComponent, {
      width: '900px',
      maxWidth: 'none', // <- agrega esto
      data: {
        customerOrders: customerOrders || {},
        salesDatePrediction: salesDatePrediction || {}
      },
      autoFocus: true,
      restoreFocus: true
    });

    const dialogSubscription = dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        console.log('Datos transformados enviados al servicio:', result);

        // Llamar al servicio para guardar la orden
        const addSubscription = this.ordersService.addOrder(result).subscribe(() => {
          this.loadSalesDatePredictions(); // Recargar la lista después de guardar
        });

        this.subscriptions.add(addSubscription);
      }
    });

    this.subscriptions.add(dialogSubscription);
  }


}
