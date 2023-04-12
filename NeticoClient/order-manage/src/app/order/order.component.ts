import { HttpClient } from '@angular/common/http';
import { Component, } from '@angular/core';
import { OrderService } from '../service/order.service';
import { Order } from './order.model';
import { ToastrService } from 'ngx-toastr';
import { OrderSearch } from './order-search.model';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent {
  orders!: any[];
  newOrder: any = {};
  showAddForm: boolean = false;
  customers!:any[];
  customer! : string;
  products!:any[];
  product!:string;
  categories!:any[];
  categoryName!:string;
  amount!:number;
  orderDate!:Date;
  searchText!:string;
  orderSearch!:OrderSearch;
  currentPage!:number;
  page:number = 1
  pageSize:number = 10;
  pageSizeOptions = [5, 10, 20];
  totalItems:number =0;
  startIndex!:number;
  isSearch:boolean = false;
  constructor(private http: HttpClient, private orderService : OrderService,private toastr: ToastrService) { }

  ngOnInit(): void {
  
    this.startIndex = (this.page - 1) * this.pageSize;
   this.orderService.getCustomer().subscribe((data)=>{
    this.customers = data;
   })
   this.orderService.getCategory().subscribe((data)=>{
    this.categories = data;

   })
   this.orderService.getProduct().subscribe((data)=>{
    this.products = data;

   })
   this.refreshList();
  }
  getCustomerName(customerId: number): string {
    const customerName = this.customers?.find(c => c.id === customerId);
    return customerName ? customerName.name : '';
  }
  getProductName(productId: number): string {
    const productName = this.products?.find(c => c.id === productId);
    return productName ? productName.name : '';
  }
  getCategoryName(categoryId: number): string {
    const categoryName = this.categories?.find(c => c.id === categoryId);
    return categoryName ? categoryName.name : '';
  }
  clearFilter(form:any){
    this.isSearch = false;
    form.resetForm();
    this.refreshList();
  }
  onSearch(form:any){
    this.isSearch = true;
    this.orderSearch = new OrderSearch();
      this.orderSearch.searchText = form.value.searchText;
      this.orderSearch.pageIndex = this.page;
      this.orderSearch.pageSize = this.pageSize;
      this.orderService.getPagging(this.orderSearch).subscribe((data:any)=>{
        this.orders = data.orders;
      })
  }
  onSubmit(form:any){
    let newOrder = new Order();
  
    newOrder.customerId = parseInt(this.customer);
    newOrder.productId = parseInt(this.product);
    newOrder.amount = this.amount;
    newOrder.orderDate = this.orderDate;
   
   
      this.orderService.createOrder(newOrder).subscribe((data:any) => {
          this.refreshList();
          this.toastr.success('Success', 'Create order success')
          form.resetForm();

        }
      , (err) => {
        this.toastr.error('Fail', 'Create order fail');
        form.resetForm();
      })
  }

  refreshList(){
    debugger
    if(this.isSearch){
      this.orderService.getPagging(this.orderSearch).subscribe((data:any)=>{
        this.orders = data.orders;
      })
    }else{
    this.orderService.getAll(this.page,this.pageSize).subscribe((data:any)=>{
      this.orders = data.orders;
       this.totalItems = data.totalItems;
    })
  }
  }
  addClick(): void {
    this.newOrder = {};
  }
  closeClick(){
    this.refreshList();
  }
  changePage(e:any){

    this.page = e;
    this.currentPage = e;
    this.startIndex = (this.currentPage - 1) * this.pageSize;
    this.refreshList();
  }
  onPageSizeChange(value:any){

    this.pageSize = value;
    this.refreshList();
  }
}
