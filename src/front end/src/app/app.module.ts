import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { NgZorroAntdModule, NZ_I18N, zh_CN } from 'ng-zorro-antd';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { registerLocaleData } from '@angular/common';
import { TranslateService, TranslateStore } from '@ngx-translate/core';
import zh from '@angular/common/locales/zh';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { WorkspaceComponent } from './components/layout/workspace/workspace.component';
import { HeaderNavUserComponent } from './components/layout/header-nav-user/header-nav-user.component';

registerLocaleData(zh);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    WorkspaceComponent,
    HeaderNavUserComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule, ReactiveFormsModule,
    HttpClientModule,
    NgZorroAntdModule,
    BrowserAnimationsModule,
  ],
  providers: [{ provide: NZ_I18N, useValue: zh_CN }, TranslateService, TranslateStore],
  bootstrap: [AppComponent]
})
export class AppModule { }
