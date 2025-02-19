import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner'
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { HubUrls } from 'src/app/constants/hub-urls';
import { ReceiveFuntions } from 'src/app/constants/receive-funtions';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { SignalRService } from 'src/app/services/common/signalr.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit 
{
  constructor(private alertify : AlertifyService,spinner : NgxSpinnerService, private signalRService : SignalRService)
  {
    super(spinner);
    signalRService.start(HubUrls.ProductHub);
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallPulseSync);

    this.signalRService.on(ReceiveFuntions.ProductAddedMessageReceiveFuntion, message => 
    {
      this.alertify.message(message, 
        {
          messageType : MessageType.Notify,
          position : Position.TopCenter
        })
    })
  }
}


