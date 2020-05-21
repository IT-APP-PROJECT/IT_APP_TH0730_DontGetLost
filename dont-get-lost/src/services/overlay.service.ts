import { Injectable, Inject, OnInit, Injector, ComponentRef } from '@angular/core';
import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal, PortalInjector } from '@angular/cdk/portal';

import { OverlayComponent } from './../overlay/overlay.component';
import { OverlayComponentRef } from './../overlay/overlay-component-ref';

interface DialogConfig {
  panelClass?: string;
  hasBackdrop?: boolean;
  backdropClass?: string;
}

const DEFAULT_CONFIG: DialogConfig = {
  hasBackdrop: true,
  backdropClass: 'dark-backdrop',
  panelClass: 'tm-dialog-panel',
}

@Injectable()
export class OverlayService {

  constructor(
    private injector: Injector,
    private overlay: Overlay) { }

  open(config: DialogConfig = {}) {

    const dialogConfig = { ...DEFAULT_CONFIG, ...config };
    const overlayRef = this.createOverlay(dialogConfig);
    const dialogRef = new OverlayComponentRef(overlayRef);
    const overlayComponent = this.attachDialogContainer(overlayRef, dialogConfig, dialogRef);
    return dialogRef;
  }

  private createOverlay(config: DialogConfig) {
    const overlayConfig = this.getOverlayConfig(config);
    return this.overlay.create(overlayConfig);
  }

  private attachDialogContainer(overlayRef: OverlayRef, config: DialogConfig, dialogRef: OverlayComponentRef) {
    const injector = this.createInjector(config, dialogRef);

    const containerPortal = new ComponentPortal(OverlayComponent, null, injector);
    const containerRef: ComponentRef<OverlayComponent> = overlayRef.attach(containerPortal);

    return containerRef.instance;
  }

  private createInjector(config: DialogConfig, dialogRef: OverlayComponentRef): PortalInjector {
    const injectionTokens = new WeakMap();

    injectionTokens.set(OverlayComponentRef, dialogRef);

    return new PortalInjector(this.injector, injectionTokens);
  }

  private getOverlayConfig(config: DialogConfig): OverlayConfig {
    const positionStrategy = this.overlay.position()
      .global()
      .centerHorizontally()
      .centerVertically();

    const overlayConfig = new OverlayConfig({
      hasBackdrop: config.hasBackdrop,
      backdropClass: config.backdropClass,
      panelClass: config.panelClass,
      scrollStrategy: this.overlay.scrollStrategies.block(),
      positionStrategy
    });

    return overlayConfig;
  }
}