import { Component, Inject, OnInit, Output } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { List_Product } from 'src/app/contracts/list_product';
import { ProductService } from 'src/app/services/common/models/product.service';
import { List_Product_Image } from 'src/app/contracts/list_product_image';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { DialogService } from 'src/app/services/common/dialog/dialog.service';
import { DeleteDialogComponent, DeleteState } from '../delete-dialog/delete-dialog.component';

declare var $ : any;

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss']
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit {

  constructor(dialogRef: MatDialogRef<SelectProductImageDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string, private productService : ProductService, private spinner : NgxSpinnerService, private dialogService : DialogService ) { 
    super(dialogRef); 
  }

  @Output() options : Partial<FileUploadOptions> =
  {
    accept : ".png, jpg, .jpeg",
    action : "upload",
    controller : "Test",
    explanation : "Resimleri sürükleyin veya seçin...",
    isAdminPage : true,
    queryString : `id=${this.data}`
  };


  images : List_Product_Image[];

  async ngOnInit(){
    this.spinner.show(SpinnerType.Pacman);
    this.images = await this.productService.readImages(this.data as string, () => this.spinner.hide(SpinnerType.Pacman));
  }

  async deleteImage(imageId : string, event : any){


    this.dialogService.openDialog({
      componentType : DeleteDialogComponent,
      data : DeleteState.Yes,
      afterClosed : async () => 
      {
        this.spinner.show(SpinnerType.Timer);

        await this.productService.deleteImage(this.data as string, imageId, () => 
        {
          this.spinner.hide(SpinnerType.Timer);
    
          var card = $(event.srcElement).parent().parent().parent();
          card.fadeOut(500);
        });
      }
    })


  }
}

export enum SelectProductImageState
{
  Close
}
