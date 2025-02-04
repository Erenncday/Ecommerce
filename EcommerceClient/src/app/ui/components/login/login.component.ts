import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AuthService } from 'src/app/services/common/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {


  constructor(private userAuthService : UserAuthService, spinner : NgxSpinnerService, private authService : AuthService, private activatedRoute : ActivatedRoute, private router: Router ) {
    super(spinner);
    
  }
  ngOnInit(): void {
 
  }


async login(UsernameOrEmail:string, Password:string)
{
  this.showSpinner(SpinnerType.Timer);
  await this.userAuthService.login(UsernameOrEmail, Password, () => {

    this.authService.identityCheck();

    this.activatedRoute.queryParams.subscribe(params =>
    {
      const returnUrl: string = params["returnUrl"];
      
      if(returnUrl)
        this.router.navigate([returnUrl]);
    });

    this.hideSpinner(SpinnerType.Timer);


  });
}

}
