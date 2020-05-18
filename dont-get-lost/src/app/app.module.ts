import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OverlayModule } from '@angular/cdk/overlay';

import { AppRoutingModule } from './app-routing.module';
import { MainMapComponent } from '../main-map/main-map.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { OverlayService } from '../services/overlay.service';
import { OverlayComponent } from '../overlay/overlay.component';

@NgModule({
    declarations: [
        MainMapComponent,
        OverlayComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        MatCheckboxModule,
        MatSidenavModule,
        MatIconModule,
        MatExpansionModule,
        MatListModule,
        MatButtonModule,
        MatInputModule,
        FormsModule, 
        ReactiveFormsModule,
        MatSelectModule,
        MatProgressSpinnerModule,
        OverlayModule,
    ],
    providers: [
        OverlayService,
    ],
    bootstrap: [
        MainMapComponent,
    ],
    entryComponents: [
        OverlayComponent,
    ],
})
export class AppModule { }
