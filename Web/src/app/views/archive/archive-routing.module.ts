import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BasicComponent } from '../../components/common/layouts/basic.component';
import { ArchiveComponent } from './archive.component';
import { ArchiveViewComponent } from './archive-view/archive-view.component';



const routes: Routes = [
    {
        path: '', component: BasicComponent,
        children: [
            { path: 'archive/:id', component: ArchiveViewComponent },
            { path: 'archive', component: ArchiveComponent }
        ]
    }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class ArchiveRoutingModule {
}
