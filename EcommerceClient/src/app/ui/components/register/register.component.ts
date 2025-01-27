import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { user } from 'src/app/entities/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formbuilder : FormBuilder) { }

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


  onSubmit(data : user){
    this.submitted = true;

    var f = this.frm;
    var c = this.component;
    var d = this.frm.hasError("passwordnotmatch");

    if(this.frm.invalid)
      return;
  }

}
