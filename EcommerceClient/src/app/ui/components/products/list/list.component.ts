import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { List_Product } from 'src/app/contracts/list_product';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit
{
    
  constructor(private productService : ProductService, private activatedRoute : ActivatedRoute) {}

  currentPageNo : number; // Sayfa Numarası /ürünler/3 gibi

  totalProductCount : number; // Toplam ürün sayısı

  totalPageCount : number; // Toplam ürün sayısına göre kaç adet sayfa olacağı

  pageSize: number = 12; // Bir sayfada kaç adet veri olacağı

  pageList : number[] = []; // Sayfa numaralarımızı tutacağımız dizi


  products: List_Product[];

  ngOnInit()
  {
   
    this.activatedRoute.params.subscribe(async params =>
    {
      this.currentPageNo = parseInt(params["pageNo"] ?? 1);

      const data : { totalProductCount : number, products : List_Product[] } = await this.productService.read(this.currentPageNo - 1, this.pageSize,
        () =>
        {
  
        },
        errorMessage =>
        {
  
        });
  
        this.products = data.products;
        this.totalProductCount = data.totalProductCount;
        this.totalPageCount = Math.ceil(this.totalProductCount / this.pageSize); // Toplam ürün sayısını, bir sayfada kaç adet görüneğine bölüyoruz ve toplamda kaç adet sayfa olacağını buluyoruz. Math.ceil fonksiyonu ile 12'den fazla olması durumunda yeni bir sayfaya atıyoruz.

        this.pageList = []; // Her sayfa değiştirdiğimizde pageList'teki verilerin sıfırlanması sağlıyoruz.

        if(this.currentPageNo - 3 <= 0) // Sayfa Numaramdan 3 çıkardığımda sonuç 0 veya 0'dan küçük ise
          for(let i = 1; i <= 7; i++) // sayfa sayısını 1'den başlat ve 7 veya 7'den küçük sayfa sayısına kadar 1 arttır.
            this.pageList.push(i);

        else if(this.currentPageNo + 3 >= this.totalPageCount) // Sayfa sayısına 3 eklediğimde toplam sayfa sayısına eşit veya büyükse
          for(let i = this.totalPageCount - 6; i <= this.totalPageCount; i++) // toplam sayfa sayısından 6 çıkart ve toplam sayfa sayısına kadar dön. Toplam sayfa sayısı 100 ise 94'den başlatacak ve 100'e kadar sonuç dönecek.
            this.pageList.push(i);

        else
          for(let i = this.currentPageNo - 3; i <= this.currentPageNo + 3; i++) // Hangi sayfadaysam bulunduğum sayfa sayısının 3 eksiğinden 3 fazlasına kadar değer döndürüyorum. 6. Sayfadaysam 3. sayfadan 9. sayfaya kadar olan sayfa sayısı sonucunu döndürüyorum
            this.pageList.push(i);
    });


  }


  // Tüm dataları çağırmak için kullanılacak method

  // async ngOnInit(){
  //   // İlk çağrı: toplam ürün sayısını almak için
  //   const initialData : { totalProductCount : number, products : List_Product[] } = await this.productService.read(0, 1,
  //     () =>
  //     {

  //     },
  //     errorMessage =>
  //     {

  //     });

  //   const totalProductCount = initialData.totalProductCount;

  //   // İkinci çağrı: toplam ürün sayısını kullanarak
  //   const data : { totalProductCount : number, products : List_Product[] } = await this.productService.read(0, totalProductCount,
  //     () =>
  //     {

  //     },
  //     errorMessage =>
  //     {

  //     });

  //   this.products = data.products;
  // }
}
