import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPagination } from '../shared/models/pagination';
import { AppConfiguration } from 'read-appsettings-json';
import { IBrand } from '../shared/models/brand';
import { IProductType } from '../shared/models/producttype';
import { map } from 'rxjs/operators';
import { ProductParameters } from '../shared/models/productParameters';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  base_URL: string = AppConfiguration.Setting().BaseUrl;

  constructor(private http: HttpClient) {}

  getProduct(productParams: ProductParameters) {
    let params = new HttpParams();
    if (productParams.brandIdSelected !== 0) {
      params = params.append(
        'brandId',
        productParams.brandIdSelected.toString()
      );
    }
    if (productParams.productTypeIdSelected !== 0) {
      params = params.append(
        'brandId',
        productParams.productTypeIdSelected.toString()
      );
    }

    if (productParams.search) {
      params = params.append("search",productParams.search);
    }
    params = params.append('sort', productParams.sortItemSelected);
    params = params.append('pageIndex', productParams.pageNumber.toString());
    params = params.append('pageIndex', productParams.pageSize.toString());

    return this.http
      .get<IPagination>(this.base_URL + 'products', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getBrand() {
    return this.http.get<IBrand[]>(this.base_URL + 'products/brands');
  }

  getProductTypes() {
    return this.http.get<IProductType[]>(this.base_URL + 'products/types');
  }
}
