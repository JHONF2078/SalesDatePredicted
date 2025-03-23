export interface SalesDatePrediction {
  custId: number;
  customerName: string;
  lastOrderDate?: Date;
  possibleNextOrderDate?: Date;
}
