import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import * as L from 'leaflet';
import { Subject, Observable } from 'rxjs';
import axios from "axios";

import { MapData, floors } from '../resources/mapData';
import { Map } from './../view-models/MapVM';
import { Room } from './../view-models/RoomVM';
import { Icon, IconType } from './../view-models/IconVM';
import { Floor } from './../view-models/FloorVM';

export enum ActiveMapType {
    Geo,
    NonGeo,
}


@Component({
    selector: 'main-map',
    templateUrl: './main-map.component.html',
    styleUrls: ['./main-map.component.scss']
})
export class MainMapComponent {
    private destroy$ = new Subject<void>();
    private map: any;
    private API_BASE_URL = 'https://localhost:44351';

    OnMenuClosedEvent$: Observable<void>;
    OnMenuOpenedEvent$: Observable<void>;
    IsMenuOpened: boolean;
    IsRequesting: boolean;
    //TODO mock data delete later
    Floors = floors;
    BuildingsLayerGroup = new L.LayerGroup();
    FloorsLayerGroup = new L.LayerGroup();
    NavigationFormFroup: FormGroup;
    ClearInitialRoomBtnVisibility: boolean;
    ClearFinalRoomBtnVisibility: boolean;
    AvailableBuildings: string[];
    InitialBuilding: string;
    FinalBuilding: string;
    CurrentMapIcons: Icon[];
    CurrentMap: Map;
    CurrentMapRooms: Room[];
    ActiveMapType: ActiveMapType;

    get IsNonGeoMapActive(): boolean {
        return this.ActiveMapType && this.ActiveMapType === ActiveMapType.NonGeo;
    }


    @ViewChild(MatSidenav)
    Menu: MatSidenav;

    constructor(private formBuilder: FormBuilder) {
        this.NavigationFormFroup = this.formBuilder.group({
            InitialRoomFormControl: new FormControl(''),
            FinalRoomFormControl: new FormControl(''),
        });
        //TODO mock data remove later
        this.AvailableBuildings = ['C3', 'C4'];
    }

    ngOnInit(): void {
        this.IsRequesting = false;
        this.IsMenuOpened = false;
        this.setGeoMap();
    }

    ngAfterViewInit(): void {
        this.OnMenuClosedEvent$ = this.Menu.closedStart;
        this.OnMenuOpenedEvent$ = this.Menu.openedStart;
        this.OnMenuOpenedEvent$.subscribe(() => this.IsMenuOpened = true);
        this.OnMenuClosedEvent$.subscribe(() => setTimeout(() => this.IsMenuOpened = false, 200));
        this.NavigationFormFroup.controls['InitialRoomFormControl'].valueChanges.subscribe((value: string) => {
            this.ClearInitialRoomBtnVisibility = this.stringHasValue(value);
        });
        this.NavigationFormFroup.controls['FinalRoomFormControl'].valueChanges.subscribe((value: string) => {
            this.ClearFinalRoomBtnVisibility = this.stringHasValue(value);
        });
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    toggleMenu(): void {
        this.Menu.toggle();
    }

    toggleFloorLayer(floorNumber: number): void {
        this.IsRequesting = true;
    }

    toggleNavigation(): void {
        //this.getGeoJsonMap(this.InitialBuilding, this.NavigationFormFroup.controls['InitialRoomFormControl'].value);
        //let svgFileName = `./../assets/${this.CurrentMap.Building}_p${this.CurrentMap.Floor}.svg`;
        let svgFileName = './../assets/C-3_p0.svg'
        this.setNonGeoMap(svgFileName);
        this.toggleMenu();
        this.addDefaultIconToMap(600,800);
    }

    getNonGeoMap = async (building: string, floor: string) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/geoJsonMaps`, data: {
            building: building,
            floor: floor
        }})
        .then( (response) => {
            this.CurrentMap =  response.data;
            })
        .catch(function (error) {
            console.log(error);
        });
    }

    getMapIcons = async (mapId: number) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/icons`, data: {mapId: mapId}})
        .then( (response) => {
            this.CurrentMapIcons =  response.data;
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    getMapRooms = async (mapId: number) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/rooms`, data: {mapId: mapId}})
        .then( (response) => {
            this.CurrentMapRooms =  response.data;
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    getFloors = async (building: string) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/floors`, data: {building: building}})
        .then( (response) => {
            return response.data;
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    getAvailableBuildings = async () => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/buildings`})
        .then( (response) => {
            this.AvailableBuildings = response.data;
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    clearInitialRoom():void {
        this.NavigationFormFroup.controls['InitialRoomFormControl'].setValue('');
    }

    clearFinalRoom():void {
        this.NavigationFormFroup.controls['FinalRoomFormControl'].setValue('');
    }

    stringHasValue(str: string): boolean {
        return !!str && str.trim().length !== 0;
    }

    private setGeoMap(): void {
        this.ActiveMapType = ActiveMapType.Geo;
        this.clearMap();
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

    private setNonGeoMap(fileName: string): void {
        this.ActiveMapType = ActiveMapType.NonGeo;
        this.clearMap();
        this.map = L.map('map', {
            crs: L.CRS.Simple,
            maxZoom: 2,
        });

        var bounds = [[0,0], [1200,1200]];
        L.imageOverlay(fileName, bounds).addTo(this.map);
        this.map.fitBounds(bounds);
    }

    private clearMap(): void {
        if (this.map != undefined) {
            this.map.off();
            this.map.remove();
        }
    }

    private addDefaultIconToMap(x: number, y: number): void {
        var icon = L.latLng([ y, x ]);
        L.marker(icon).addTo(this.map);
    }
}
