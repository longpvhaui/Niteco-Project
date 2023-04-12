import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { Order } from "../order/order.model";
import { OrderSearch } from "../order/order-search.model";



@Injectable({
  providedIn:'root'
})
export class OrderService {
    constructor(private http:HttpClient){
        
    }
    getCustomer(){
        return this.http.get<any>(environment.apiUrl + '/api/Shared/get-all-customer');
    }
    getProduct(){
      
        return this.http.get<any>(environment.apiUrl + '/api/Shared/get-all-product');
    }
    getCategory(){
      
        return this.http.get<any>(environment.apiUrl + '/api/Shared/get-all-category');
    }
    createOrder(order:Order){
        debugger
        return this.http.post(environment.apiUrl + '/api/Order/create',order);
    }
    getPagging(orderSearch:OrderSearch){
        debugger
        return this.http.post(environment.apiUrl + '/api/Order/get-search',orderSearch);
    }
    getAll(pageIndex:number,pageSize:number){
      
        return this.http.get<Order>(environment.apiUrl + '/api/Order/get-all?'+ `pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
}