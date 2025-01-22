import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, first, firstValueFrom } from 'rxjs';

import { Create_Product } from 'src/app/contracts/create_product';
import { List_Product } from 'src/app/contracts/list_product';
import { List_Product_Image } from 'src/app/contracts/list_product_image';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService : HttpClientService) { }

  create(createProduct : Create_Product, successCallBack? : () => void, errorCallBack? : (errorMessage : string) => void)
  {
    this.httpClientService.post(
      {
        controller : "Test"
      }, createProduct).subscribe(result => 
        {
          successCallBack();
        }, (errorResponse : HttpErrorResponse) =>
        {
          const _error : Array<{ key : string, value : Array<string> }> = errorResponse.error;

          let message = "";

          _error.forEach((v, index) => 
          {
            v.value.forEach((_v, index) => 
            {
              message += `${_v}<br>`;
            });
          });
          errorCallBack(message)
        });
  }

  async read(page : number = 0, size : number = 5,  successCallBack? : () => void, errorCallBack? : (errorMessage : string) => void) : Promise<{ totalCount : number, products : List_Product[] }>
  {
   const promiseData : Promise<{ totalCount : number, products : List_Product[] }> = this.httpClientService.get<{ totalCount : number, products : List_Product[]}>(
    {
      controller : "Test",
      queryString : `page=${page}&size=${size}`
    }).toPromise();

    promiseData.then(d => successCallBack())
    .catch((errorResponse : HttpErrorResponse) => errorCallBack(errorResponse.message))

    return await promiseData;
  }

  async delete(id : string)
  {
    const deleteObservable : Observable<any> =  this.httpClientService.delete<any>(
      {
        controller : "Test"
      }, id);

    await firstValueFrom(deleteObservable)
  }

  async readImages(id : string, successCallBack? : () => void) : Promise<List_Product_Image[]>
  {
    const getObservable : Observable<List_Product_Image[]> =  this.httpClientService.get<List_Product_Image[]>(
      {
        action : "GetProductImages",
        controller : "Test"
      }, id);


      const images : List_Product_Image[] = await firstValueFrom(getObservable);
      successCallBack();

    return images;
  }

  async deleteImage(id : string, imageId : string, successCallBack? : () => void)
  {
    
    const deleteObservable = this.httpClientService.delete(
      {
       action : "DeleteProductImage",
        controller : "Test",
        queryString : `imageId=${imageId}`
      }, id)

    await firstValueFrom(deleteObservable);
    successCallBack();
    
  }

}
