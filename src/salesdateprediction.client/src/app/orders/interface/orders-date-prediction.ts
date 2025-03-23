export interface SalesDatePrediction {
  custId: number;
  companyName: string;
  lastOrderDate?: Date;
  possibleNextOrderDate?: Date;
}
