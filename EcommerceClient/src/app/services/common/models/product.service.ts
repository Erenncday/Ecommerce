import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from 'src/app/contracts/create_product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService : HttpClientService) { }

  create(createProduct : Create_Product, successCallBack? : any)
  {
    this.httpClientService.post(
      {
        controller : "Test"
      }, createProduct).subscribe(result => 
        {
          successCallBack();
          // alert("Başarılı!");
        });
  }
}
