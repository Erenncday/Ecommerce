import { NgModule} from '@angular/core';
import { CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HttpClientModule } from '@angular/common/http';




@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AdminModule, UiModule, 
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    HttpClientModule
    
  ],
  providers: [
    {provide : "baseUrl", useValue: "https://localhost:7048/api", multi: true}
  ],
  bootstrap: [AppComponent],
  schemas : [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class AppModule { }
