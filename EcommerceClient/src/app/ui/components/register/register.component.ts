import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { UserService } from 'src/app/services/common/models/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formbuilder : FormBuilder, private userService : UserService, private toastService : CustomToastrService) { }

  frm: FormGroup;
  ngOnInit(): void {
    this.frm = this.formbuilder.group({
      namesurname : ["", [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      username : ["", [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      email : ["", [Validators.required, Validators.email, Validators.maxLength(250)]],
      Password : ["", [Validators.required]],
      Password2 : ["", [Validators.required]]
    }, { validators : (group : AbstractControl) : ValidationErrors | null =>
        {

          let password = group.get("Password").value;
          let password2 = group.get("Password2").value;
          return password === password2 ? null : {passwordnotmatch : true};
        }
    })
  }

  get component()
  {
    return this.frm.controls;
  }

  submitted : Boolean = false;


  async onSubmit(user : User){
    this.submitted = true;


    if(this.frm.invalid)
      return;

    const result: Create_User = await this.userService.create(user);

    if(result.succeeded)
      this.toastService.message(result.message, "Kayıt Başarılı", {
      messageType : ToastrMessageType.Success,
      position : ToastrPosition.TopRight
      })
    else
      this.toastService.message(result.message, "Kayıt Oluşturulurken beklenmeyen bir hatayla karşılaşıldı!", {
      messageType : ToastrMessageType.Error,
      position : ToastrPosition.TopRight
      })
  }

}
