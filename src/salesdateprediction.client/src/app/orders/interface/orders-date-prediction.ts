export interface SalesDatePrediction {
  custId: Number;
  companyName: string;
  lastOrderDate?: Date;
  possibleNextOrderDate?: Date;
}
