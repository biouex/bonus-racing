import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BasicComponent } from '../../components/common/layouts/basic.component';
import { UsersComponent } from './users.component';
import { UserEditComponent } from './user-edit/user-edit.component';



const routes: Routes = [
    {
        path: '', component: BasicComponent,
        children: [
            { path: 'users/:id', component: UserEditComponent },
            { path: 'users', component: UsersComponent }
        ]
    }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class UsersRoutingModule {
}
