import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, firstValueFrom } from 'rxjs';

import { Create_Product } from 'src/app/contracts/create_product';
import { List_Product } from 'src/app/contracts/list_product';

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
}
