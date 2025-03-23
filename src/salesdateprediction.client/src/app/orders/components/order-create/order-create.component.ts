import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { Order } from '../../interface/order';
import { OrdersService } from '../../service/orders.service';
import { MATERIAL_IMPORTS } from '../../../material/material.component';
import { CommonModule } from '@angular/common';
import { EmployeesService } from '../../service/employees.service';
import { ShippersService } from '../../service/shippers.service';
import { ProductsService } from '../../service/products.service';
import { SalesDatePrediction } from '../../interface/orders-date-prediction';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    private productsService: ProductsService,
    private snackBar: MatSnackBar
  ) {

    this.customerName = data?.salesDatePrediction?.customerName;

    this.orderForm = this.fb.group({
      empId: [data?.order?.empId || '', [Validators.required]],
      shipperId: [data?.order?.shipperId || '', [Validators.required]],
      shipName: [data?.order?.shipName || '', [Validators.required]],
      shipAddress: [data?.order?.shipAddress || '', [Validators.required]],
      shipCity: [data?.order?.shipCity || '', [Validators.required]],
      shipCountry: [data?.order?.shipCountry || '', [Validators.required]],
      orderDate: [data?.order?.orderDate || '', [Validators.required]],
      requiredDate: [data?.order?.requiredDate || '', [Validators.required]],
      shippedDate: [data?.order?.shippedDate || '', [Validators.required]],
      freight: [
        data?.order?.freight,
        [Validators.required, Validators.min(0)]
      ],
      product: [data?.order?.orderDetail?.productId || '', [Validators.required]],
      unitPrice: [
        data?.order?.orderDetail?.unitPrice,
        [Validators.required, Validators.min(0.01)]
      ],
      qty: [
        data?.order?.orderDetail?.qty,
        [Validators.required, Validators.min(1)]
      ],
      discount: [
        data?.order?.orderDetail?.discount,
        [Validators.required, discountRangeValidator]
      ],
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

      this.dialogRef.close(transformedOrder);


      this.snackBar.open('Order saved successfully!', 'Close', {
        duration: 3000,
        verticalPosition: 'top',
        horizontalPosition: 'center'
      });

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

function discountRangeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  return (value >= 0 && value <= 1) ? null : { discountRange: true };
}
