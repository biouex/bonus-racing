import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BasicComponent } from '../../components/common/layouts/basic.component';
import { ActiveRoundComponent } from './active-round.component';



const routes: Routes = [
    {
        path: '', component: BasicComponent,
        children: [
            { path: 'active', component: ActiveRoundComponent }
        ]
    }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class ActiveRoundRoutingModule {
}
