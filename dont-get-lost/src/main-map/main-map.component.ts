import { Component, ViewChild } from '@angular/core';
import * as L from 'leaflet';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject, Observable } from 'rxjs';

import { MapData, floors } from '../resources/mapData';

@Component({
    selector: 'main-map',
    templateUrl: './main-map.component.html',
    styleUrls: ['./main-map.component.scss']
})
export class MainMapComponent {
    private destroy$ = new Subject<void>();
    private map: any;

    OnMenuClosedEvent$: Observable<void>;
    OnMenuOpenedEvent$: Observable<void>;
    IsMenuOpened: boolean;
    Floors = floors;
    BuildingsLayerGroup = new L.LayerGroup();
    FloorsLayerGroup = new L.LayerGroup();
    CurrentBuilding: string;

    @ViewChild(MatSidenav)
    Menu: MatSidenav;

    ngOnInit(): void {
        this.IsMenuOpened = false;
        this.map = L.map('map').setView([51.109070, 17.05953], 18);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(this.map);

        this.BuildingsLayerGroup.addTo(this.map);
        this.FloorsLayerGroup.addTo(this.map);

        var layerBuildings = new L.GeoJSON(MapData.buildings, {
            style: function () {
                return {
                    color: 'purple',
                    fill: 'purple',
                    opacity: 0.8,
                };
            },
            onEachFeature: function (feature, layer) {
                layer.bindPopup('<h1>' + feature.properties.name + '</h1><p> ' + feature.properties.popupContent + '</p>');
            }
        });

        this.BuildingsLayerGroup.addLayer(layerBuildings);
    }

    ngAfterViewInit(): void {
        this.OnMenuClosedEvent$ = this.Menu.closedStart;
        this.OnMenuOpenedEvent$ = this.Menu.openedStart;
        this.OnMenuOpenedEvent$.subscribe(() => this.IsMenuOpened = true);
        this.OnMenuClosedEvent$.subscribe(() => setTimeout(() => this.IsMenuOpened = false, 200));
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    toggleMenu(): void {
        this.Menu.toggle();
    }

    toggleFloorLayer(floorNumber: string): void {
        this.FloorsLayerGroup.clearLayers();
        MapData.floors.forEach((floor: any) => {
            if (floor.floorNumber === floorNumber) {
                this.FloorsLayerGroup.addLayer(new L.GeoJSON(floor, {
                    style: function () {
                        return {
                            color: 'green',
                            fill: 'green',
                            opacity: 0.8,
                        };
                    },
                }));
            }
        });
    }
}
