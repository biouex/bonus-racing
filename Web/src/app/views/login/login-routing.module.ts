import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login.component';
import { BlankComponent } from '../../components/common/layouts/blank.component';



const routes: Routes = [
  {
    path: '', component: BlankComponent,
    children: [
      { path: 'login', component: LoginComponent }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class LoginRoutingModule {
}
