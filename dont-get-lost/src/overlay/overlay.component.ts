import { Component, Input, Inject } from '@angular/core';

import { OverlayComponentRef } from './overlay-component-ref';

@Component({
    selector: 'overlay',
    templateUrl: './overlay.component.html',
    styleUrls: ['./overlay.component.scss']
})
export class OverlayComponent {

  constructor(public dialogRef: OverlayComponentRef) { }
}
