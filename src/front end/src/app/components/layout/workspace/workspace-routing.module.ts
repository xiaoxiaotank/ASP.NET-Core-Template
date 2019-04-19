import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from '../../users/users.component';
import { WorkspaceComponent } from './workspace.component';


const routes: Routes = [
  { path: '', component: WorkspaceComponent , pathMatch: 'full' },
  { path: 'users', component: UsersComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WorkspaceRoutingModule { }
