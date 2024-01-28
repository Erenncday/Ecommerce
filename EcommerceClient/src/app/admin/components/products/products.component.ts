import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Product } from 'src/app/contracts/product';
import { HttpClientService } from 'src/app/services/common/http-client.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit 
{
  constructor(spinner : NgxSpinnerService, private httpClientService : HttpClientService)
  {
    super(spinner);
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallPulseSync);
    
    this.httpClientService.get<Product[]>(
      {
        controller : "Test"
      }
    ).subscribe(data => console.log(data));

    // this.httpClientService.post(
    //   {
    //     controller : "Test"
    //   },
    //   {
    //     name : "Akıllı Kalem",
    //     stock : 199,
    //     price : 75
    //   }
    // ).subscribe();


    // this.httpClientService.put(
    //   {
    //     controller : "Test"
    //   },
    //   {
    //     id : "81797259-0381-4483-a7d1-c7131b53169a",
    //     name : "Dolma Kalem",
    //     stock : 100,
    //     price : 100.75
    //   }
    // ).subscribe();

    // this.httpClientService.delete(
    //   {
    //     controller : "Test"
    //   }, "0ec56085-5dca-402d-9df2-e730f2fff40e" 
    // ).subscribe();

    // this.httpClientService.get(
    //   {
    //     baseUrl : "https://jsonplaceholder.typicode.com",
    //     controller : "posts"
    //   }
    // ).subscribe(data => console.log(data))

    this.httpClientService.get(
      {
        fullEndPoint : "https://jsonplaceholder.typicode.com/posts"
      }
    ).subscribe(data => console.log(data))


  }
}

