import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IBrand } from '../shared/models/brand';
import { IProductType } from '../shared/models/producttype';
import { ProductParameters } from '../shared/models/productParameters';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild("search",{static:true}) searchTerm : ElementRef;

  products: IProduct[];
  brands: IBrand[];
  productTypes: IProductType[];
  productParams = new ProductParameters();  
  totalCount : number;
  sortOptions = [
    { name: 'Alphabatically', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService
      .getProduct(
       this.productParams
      )
      .subscribe(
        (response) => {
          this.products = response.data;
          this.productParams.pageSize =response.pageSize;
          this.productParams.pageNumber =response.pageIndex;
          this.totalCount = response.count;
        },
        (error) => {
          console.log(error);
        }
      );
  }

  getBrands() {
    this.shopService.getBrand().subscribe(
      (response) => {
        this.brands = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe(
      (response) => {
        this.productTypes = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onBrandSelected(brandId: number) {
    this.productParams.brandIdSelected = brandId;
    this.productParams.pageNumber =1;
    this.getProducts();
  }

  onProductTypeSelected(productTypeId: number) {
    this.productParams.productTypeIdSelected = productTypeId;
    this.productParams.pageNumber =1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.productParams.sortItemSelected = sort;
    this.getProducts();
  }

  onPageChanged(event : any){
    if (this.productParams.pageNumber !==event ) {
      this.productParams.pageNumber = event;
      this.getProducts();
    }   
  }

  onSearch(){
    this.productParams.search = this.searchTerm.nativeElement.value;
    this.productParams.pageNumber =1;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value ="";
    this.productParams = new ProductParameters();
    this.getProducts();
  }

}
