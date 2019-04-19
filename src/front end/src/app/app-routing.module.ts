import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { UsersComponent } from './components/users/users.component';
import { WorkspaceComponent } from './components/layout/workspace/workspace.component';


const routes: Routes = [
  { path: '', component: WorkspaceComponent },
  { path: 'index', component: WorkspaceComponent },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UsersComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
