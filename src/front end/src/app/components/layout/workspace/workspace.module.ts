import { NgModule } from '@angular/core';
import { TranslateModule, TranslateService, TranslateStore } from '@ngx-translate/core';
// import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';

import { FooterComponent } from '../footer/footer.component';
import { WorkspaceComponent } from './workspace.component';
// import { DropdownDirective } from '../../shared/dropdown-directive/dropdown.directive';
// import { DropdownTriggerDirective } from '../../shared/dropdown-directive/dropdown-trigger.directive';
// import { NavItemDirective } from '../../shared/navitem-directive/dropdown.directive';
import { NavBarComponent } from "../nav-bar/nav-bar.component";
import { LeftSidebarComponent } from '../left-sidebar/left-sidebar.component';
import { RightSidebarComponent } from '../right-sidebar/right-sidebar.component';
import { WorkspaceRoutingModule } from './workspace-routing.module';
import { UsersComponent } from '../../users/users.component';


@NgModule({
  declarations: [
    FooterComponent,
    NavBarComponent,
    LeftSidebarComponent,
    RightSidebarComponent,
    WorkspaceComponent,
    UsersComponent,
    // DropdownDirective,
    // DropdownTriggerDirective,
    // NavItemDirective
  ],
  imports: [
    CommonModule,
    // SharedModule, 
    WorkspaceRoutingModule,
    TranslateModule.forRoot(),
  ],
})
export class WorkspaceModule { }
