import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent {
  orders!: any[];
  newOrder: any = {};
  showAddForm: boolean = false;
  showModal: boolean = false;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getOrders().subscribe((data: any[]) => {
      this.orders = data;
    });
  }

  getOrders(): Observable<any[]> {
    return this.http.get<any[]>('http://your-backend-api-url/orders');
  }

  addClick(): void {
    this.newOrder = {};
    this.showModal = true;
  }
  closeClick(){
    //this.refreshList();
  }
  addOrder(orderForm: any): void {
    this.addOrderRequest(this.newOrder).subscribe((data: any) => {
      this.orders.push(data);
      this.showAddForm = false;
      orderForm.reset();
    });
  }

  addOrderRequest(order: any): Observable<any> {
    return this.http.post<any>('http://your-backend-api-url/orders', order);
}

editOrder(order: any): void {
// TODO: implement edit order functionality
}

deleteOrder(order: any): void {
// TODO: implement delete order functionality
}
}
