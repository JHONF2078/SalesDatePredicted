import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { Order } from '../../interface/order';
import { OrdersService } from '../../service/orders.service';
import { MATERIAL_IMPORTS } from '../../../material/material.component';
import { CommonModule } from '@angular/common';
import { EmployeesService } from '../../service/employees.service';
import { ShippersService } from '../../service/shippers.service';
import { ProductsService } from '../../service/products.service';
import { SalesDatePrediction } from '../../interface/orders-date-prediction';

@Component({
  selector: 'app-order-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ...MATERIAL_IMPORTS],
  templateUrl: './order-create.component.html',
  styleUrl: './order-create.component.scss'
})
export class OrderCreateComponent {
  orderForm: FormGroup;
  dateFormat: string = 'dd/MM/yyyy';

  customerName: string = '';
  employees: { value: number; viewValue: string }[] = [];
  shippers: { value: number; viewValue: string }[] = [];
  products: { value: number; viewValue: string }[] = [];

  constructor(
    public dialogRef: MatDialogRef<OrderCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { order: Order; salesDatePrediction: SalesDatePrediction },
    private fb: FormBuilder,
    private dateAdapter: DateAdapter<any>,
    private orderService: OrdersService,
    private employeesService: EmployeesService,
    private shippersService: ShippersService,
    private productsService: ProductsService
  ) {

    this.customerName = data?.salesDatePrediction?.companyName;

    this.orderForm = this.fb.group({
      empId: [data?.order?.empId || '', Validators.required],
      shipperId: [data?.order?.shipperId || '', Validators.required],
      shipName: [data?.order?.shipName || ''],
      shipAddress: [data?.order?.shipAddress || ''],
      shipCity: [data?.order?.shipCity || ''],
      shipCountry: [data?.order?.shipCountry || ''],
      orderDate: [data?.order?.orderDate || '', Validators.required],
      requiredDate: [data?.order?.requiredDate || '', Validators.required],
      shippedDate: [data?.order?.shippedDate || '', Validators.required],
      freight: [data?.order?.freight || 0, Validators.required],
      product: [data?.order?.orderDetail?.productId || '', Validators.required],
      unitPrice: [data?.order?.orderDetail?.unitPrice || 0, Validators.required],
      qty: [data?.order?.orderDetail?.qty || 0, Validators.required],
      discount: [data?.order?.orderDetail?.discount || 0, Validators.required],
    });

  }


  ngOnInit() {
    this.employeesService.getEmployees().subscribe(employees => {
      this.employees = employees.map(employee => {
        return { value: employee.empId, viewValue: `${employee.fullName}` };
      });
    });

    this.shippersService.getShippers().subscribe(shippers => {
      this.shippers = shippers.map(shipper => {
        return { value: shipper.shipperId, viewValue: `${shipper.companyName}` };
      });
    });
    this.productsService.getProducts().subscribe(products => {
      this.products = products.map(product => {
        return { value: product.productId, viewValue: `${product.productName}` };
      });
    });
  }

  save(): void {
    if (this.orderForm.valid) {
      const formValue = this.orderForm.value;

      // Transformar los datos al formato esperado por la API
      const transformedOrder = {
        custId: this.data.salesDatePrediction?.custId || 0, // Obtener el ID del cliente si está disponible
        empId: formValue.empId,
        orderDate: formValue.orderDate,
        requiredDate: formValue.requiredDate,
        shippedDate: formValue.shippedDate,
        shipperId: formValue.shipperId,
        freight: formValue.freight,
        shipName: formValue.shipName,
        shipAddress: formValue.shipAddress,
        shipCity: formValue.shipCity,
        shipCountry: formValue.shipCountry,
        orderDetail: {
          productId: formValue.product,
          unitPrice: formValue.unitPrice,
          qty: formValue.qty,
          discount: formValue.discount
        }
      };

      // Cerrar el diálogo y devolver los datos transformados
      this.dialogRef.close(transformedOrder);
    } else {
      console.error('Formulario inválido:', this.orderForm.errors);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
    // Método 'onCancel' que cierra el diálogo sin retornar valores.
  }
}
