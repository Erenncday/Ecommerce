import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { ProductService } from 'src/app/services/common/models/product.service';
import { List_Product } from 'src/app/contracts/list_product';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { DialogService } from 'src/app/services/common/dialog/dialog.service';
import { SelectProductImageDialogComponent } from 'src/app/dialogs/select-product-image-dialog/select-product-image-dialog.component';

declare var $ : any;

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})

export class ListComponent extends BaseComponent implements OnInit{

  constructor(spinner : NgxSpinnerService, private productService : ProductService, private alertifyService : AlertifyService, private dialogService : DialogService) 
  { 
    super(spinner)
  }

  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate', 'updatedDate', 'photos', 'edit', 'delete'];

  dataSource : MatTableDataSource<List_Product> = null;

  @Output() fileUploadOptions : Partial<FileUploadOptions> = 
  {
    action : "upload",
    controller : "Test",
    explanation : "Resimleri sürükleyin veya seçin...",
    isAdminPage : true,
    accept : ".png, jpg, .jpeg, .pdf"
  };

  @ViewChild(MatPaginator) paginator: MatPaginator;

  async getProducts()
  {
    this.showSpinner(SpinnerType.Timer);

    const allProducts : { totalCount : number, products : List_Product[] } = await this.productService.read(this.paginator ? this.paginator.pageIndex : 0,this.paginator ? this.paginator.pageSize : 5, () => this.hideSpinner(SpinnerType.Timer), errorMessage => this.alertifyService.message(errorMessage, 
    {
      dismissOthers : true,
      messageType : MessageType.Error,
      position : Position.TopRight
    }))
    
    this.dataSource = new MatTableDataSource<List_Product>(allProducts.products);

    this.paginator.length = allProducts.totalCount;
  }

  // delete(id, event)
  // {
  //   alert(id)
  //   const img : HTMLImageElement = event.srcElement;
  //   $(img.parentElement.parentElement).fadeOut(2000);
  // }

  addProductImages(id: string)
  {
    this.dialogService.openDialog(
      {
        componentType : SelectProductImageDialogComponent,
        data : id,
        options : 
        {
          width : "1400px"
        }
      }
    )
  }

  async pageChanged()
  {
    await this.getProducts();
  }

  async ngOnInit() 
  {
   await this.getProducts();
  }
}