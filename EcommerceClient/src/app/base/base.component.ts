import { NgxSpinnerService } from 'ngx-spinner';


export class BaseComponent {

  constructor(private spinner : NgxSpinnerService) { }
  
    showSpinner(spinnerNameType : SpinnerType)
    {
      this.spinner.show(spinnerNameType);

      setTimeout(() => this.hideSpinner(spinnerNameType), 1000);
    }
    
    hideSpinner(spinnerNameType : SpinnerType)
    {
      this.spinner.hide(spinnerNameType);
    }
  
}

export enum SpinnerType
{
  BallPulseSync = "s1",
  BallRunningDots = "s2",
  Pacman = "s3",
  Timer = "s4"
}
