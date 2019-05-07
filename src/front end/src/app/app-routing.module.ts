import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { WorkspaceComponent } from './components/layout/workspace/workspace.component';



const routes: Routes = [
  { path: '', component: WorkspaceComponent },
  // { path: 'index', loadChildren: './components/layout/workspace/workspace.module#WorkspaceModule', canLoad: [] },
  { path: 'index', component: WorkspaceComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
