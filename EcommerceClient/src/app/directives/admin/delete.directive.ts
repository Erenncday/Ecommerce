import { HttpErrorResponse } from '@angular/common/http';
import { Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2 } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { DeleteDialogComponent, DeleteState } from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { DialogService } from 'src/app/services/common/dialog/dialog.service';
import { HttpClientService } from 'src/app/services/common/http-client.service';

declare var $: any;

@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(private spinner: NgxSpinnerService, private element: ElementRef, private _renderer: Renderer2, private httpClientService: HttpClientService, public dialog: MatDialog, private alertifyService: AlertifyService, private dialogService: DialogService) {
    const img = _renderer.createElement("img");

    img.setAttribute("src", "../../../../../assets/Icons/close-square.png");
    img.setAttribute("style", "cursor : pointer;");
    img.width = 22;
    img.height = 22;

    _renderer.appendChild(element.nativeElement, img);
  }

  @Input() id: string;
  @Input() controller: string;
  @Output() callback: EventEmitter<any> = new EventEmitter();

  @HostListener("click")
  async onclick() {
    this.dialogService.openDialog(
      {
        componentType : DeleteDialogComponent,
        data : DeleteState.Yes,
        afterClosed : async () =>
        {
    
          this.spinner.show(SpinnerType.Timer)
          
          const td : HTMLTableCellElement = this.element.nativeElement;
    
          await this.httpClientService.delete(
            {
              controller : this.controller
            }, this.id).subscribe(data => 
              {
                $(td.parentElement).fadeOut(2000, () => 
                {
                  this.callback.emit();
                  this.alertifyService.message("Ürün başarıyla silindi.", {
                    dismissOthers : true,
                    messageType : MessageType.Success,
                    position : Position.TopRight
                  })
                });
              }, (errorResponse : HttpErrorResponse) =>
              {
    
                  this.spinner.hide(SpinnerType.Timer)
    
                  this.alertifyService.message("Silme işlemi gerçekleştirilirken beklenmeyen bir hayatla karşılaşıldı!", {
                  dismissOthers : true,
                  messageType : MessageType.Error,
                  position : Position.TopRight
                })
              });
        }
      }
    );
  }

  // openDialog(afterClosed : any): void {
  //   const dialogRef = this.dialog.open(DeleteDialogComponent, {
  //     data: DeleteState.Yes,
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     if(result == DeleteState.Yes)
  //     {
  //      afterClosed();
  //     }
  //   });
  // }
}
