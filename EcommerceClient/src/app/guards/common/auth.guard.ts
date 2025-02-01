import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { _isAuthenticated, AuthService } from 'src/app/services/common/auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private jwtHelper : JwtHelperService, private router: Router, private toastrService : CustomToastrService, private spinner: NgxSpinnerService ) {
    
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
  {
    this.spinner.show(SpinnerType.BallPulseSync);

    // const token : string = localStorage.getItem("accessToken");

    // const decodeToken = this.jwtHelper.decodeToken(token);

    // const expirationDate: Date = this.jwtHelper.getTokenExpirationDate(token);

    // const expired : boolean = this.jwtHelper.isTokenExpired(token);

    // let expired: boolean;

    // try
    // {
    //   expired = this.jwtHelper.isTokenExpired(token);
    // }
    // catch
    // {
    //   expired = true;
    // }

    if(!_isAuthenticated)
    {
      this.router.navigate(["login"], {queryParams: {returnUrl: state.url}})

      this.toastrService.message("Oturum açmanız gerekiyor!", "Yetkisiz Erişim!", {
        messageType: ToastrMessageType.Warning,
        position : ToastrPosition.TopRight
      })
    }
    
    this.spinner.hide(SpinnerType.BallPulseSync);

    return true;
  }
    
}

