import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MatSidenavModule } from '@angular/material/sidenav';

const routes: Routes = [];

@NgModule({
    imports: [
        RouterModule.forRoot(routes),
        MatSidenavModule,
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
