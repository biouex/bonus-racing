import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BasicComponent } from '../../components/common/layouts/basic.component';
import { RoundsComponent } from './rounds.component';
import { RoundEditComponent } from './round-edit/round-edit.component';



const routes: Routes = [
    {
        path: '', component: BasicComponent,
        children: [
            { path: '', pathMatch: 'full', redirectTo: 'rounds' },
            { path: 'rounds/:id', component: RoundEditComponent },
            { path: 'rounds', component: RoundsComponent }
        ]
    }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class RoundsRoutingModule {
}
