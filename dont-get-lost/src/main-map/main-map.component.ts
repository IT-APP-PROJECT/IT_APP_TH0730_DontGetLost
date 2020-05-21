import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import * as L from 'leaflet';
import { Subject, Observable } from 'rxjs';
import axios from "axios";

import { MapData, buildings, IconTitles } from '../resources/mapData';
import { Map } from './../view-models/MapVM';
import { Room } from './../view-models/RoomVM';
import { Icon, IconType } from './../view-models/IconVM';
import { Floor } from './../view-models/FloorVM';
import { Building } from './../view-models/BuildingVM';
import { OverlayService } from './../services/overlay.service';
import { OverlayComponentRef } from './../overlay/overlay-component-ref';

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
    private API_BASE_URL = 'https://localhost:5001';

    OnMenuClosedEvent$: Observable<void>;
    OnMenuOpenedEvent$: Observable<void>;
    IsMenuOpened: boolean;
    BuildingsLayerGroup = new L.LayerGroup();
    FloorsLayerGroup = new L.LayerGroup();
    NavigationFormFroup: FormGroup;
    ClearInitialRoomBtnVisibility: boolean;
    ClearFinalRoomBtnVisibility: boolean;
    AvailableBuildings = buildings;
    InitialBuilding: Building;
    FinalBuilding: Building;
    CurrentMapIcons: Icon[];
    CurrentMap: Map;
    CurrentMapRooms: Room[];
    ActiveMapType: ActiveMapType;
    ActiveBuilding: Building;
    ActiveFloor: Floor;
    IconType: IconType;
    OverlayRef: OverlayComponentRef;
    SVGLink: string;
    CustomIconLink: string;
    IconTitles = IconTitles;
    InactiveBuildingName: string;

    get IsNonGeoMapActive(): boolean {
        return this.ActiveMapType && this.ActiveMapType === ActiveMapType.NonGeo;
    }

    @ViewChild(MatSidenav)
    Menu: MatSidenav;

    constructor(private formBuilder: FormBuilder, private overlayService: OverlayService) {
        this.NavigationFormFroup = this.formBuilder.group({
            InitialRoomFormControl: new FormControl(''),
            FinalRoomFormControl: new FormControl(''),
        });
    }

    ngOnInit(): void {
        this.IsMenuOpened = false;
        this.CurrentMap = {MapId: '', PathPoints: []};
        this.CurrentMapIcons = [];
        this.CurrentMapRooms = [];
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

    toggleFloorLayer(floorNumber: string): void {
        let mapId = `${this.ActiveBuilding.Name}-0${floorNumber}`;
        this.ActiveFloor = this.ActiveBuilding.Floors.find((floor: Floor) => floor.Number === floorNumber);
        this.setupLayerData(mapId);
    }

    toggleNavigation(): void {
        let floorNumber = this.retriveFloor(this.NavigationFormFroup.controls['InitialRoomFormControl'].value);
        let mapId = `${this.InitialBuilding.Name}-0${floorNumber}`;
        this.ActiveBuilding =  this.InitialBuilding;
        this.InactiveBuildingName = this.FinalBuilding.Name;
        this.ActiveFloor = this.ActiveBuilding.Floors.find((floor: Floor) => floor.Number === floorNumber);
        this.toggleMenu();
        this.setupLayerData(mapId);
    }

    switchBuilding(): void {
        this.ActiveBuilding = this.ActiveBuilding === this.InitialBuilding ? this.FinalBuilding : this.InitialBuilding;
        this.InactiveBuildingName = this.ActiveBuilding === this.InitialBuilding ? this.FinalBuilding.Name : this.InitialBuilding.Name;
        let floor = this.retriveFloor(this.NavigationFormFroup.controls['FinalRoomFormControl'].value);
        this.toggleFloorLayer(floor);
    }

    setupLayerData(mapId: string): void {
        this.toggleOverlay();
        let floorImagePromise = this.getFloorImageURL(mapId);
        let nonGeoMapPromise = this.getNonGeoMap(mapId);
        let mapPromise = this.getMapRooms(mapId)
        let iconPromise = this.getMapIcons(mapId)
        Promise.all([floorImagePromise, nonGeoMapPromise, mapPromise, iconPromise].map(p =>p.catch(e => e)))
        .finally(() => {
            // this.CurrentMapIcons.forEach((icon: Icon) => {
            //     this.getCustomIconImage(icon.Type, icon.X, icon.Y).finally(() => {this.closeOverlay()});
            // });
            this.closeOverlay();
        });
    }

    getNonGeoMap = async (mapId: string) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/pathPoints?mapName=${mapId}`})
        .then( (response) => {
            response.data.forEach((element: any) => {
                this.CurrentMap.PathPoints.push({x: element.x, y: element.y})
            });
            this.CurrentMap.MapId =  mapId;
            })
        .catch(function (error) {
            console.log(error);
        });
    }

    getMapIcons = async (mapId: string) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/icons?mapName=${mapId}`})
        .then( (response) => {
            response.data.forEach((icon: any) => {
                this.CurrentMapIcons.push({MapId: icon.mapName, Type: icon.type, X: icon.coordinates.x, Y: icon.coordinates.y});
            });
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    getMapRooms = async (mapId: string) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/rooms?mapName=${mapId}`})
        .then( (response) => {
            response.data.forEach((room: any) => {
                this.CurrentMapRooms.push({MapId: room.mapName, Title: room.name, Description: room.description, X: room.coordinates.x, Y: room.coordinates.y, Link: ''});
            });
            this.CurrentMapRooms.forEach((room: Room) => {
                this.addRoomIconToMap(room.X, room.Y, room.Title, room.Description, room.Link);
            });
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    getFloorImageURL = async (svgName: string) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/images?name=${svgName}`})
        .then( (response) => {
            this.SVGLink = response.data.url;
            this.setNonGeoMap(this.SVGLink);
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    getCustomIconImage = async (iconType: IconType, x: number, y: number) => {
        axios({  method: "GET", url: `${this.API_BASE_URL}/images?name=${iconType}`})
        .then( (response) => {
            let popupTitle = this.IconTitles.find((iconTitle: any) => iconTitle.Type === IconType[iconType]);
            var coordinates = L.latLng([ y, x ]);
            var customIcon = L.icon({
                iconUrl: response.data.url,
                shadowUrl: '',
            
                iconSize:     [30, 30], 
                shadowSize:   [0, 0],
                iconAnchor:   [15, 15], 
                shadowAnchor: [0, 0], 
                popupAnchor:  [0, -15]
            });
            L.marker(coordinates, {icon: customIcon}).addTo(this.map).bindPopup('<h3>' + popupTitle + '</h3>');
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

    toggleOverlay(): void {
        this.OverlayRef = this.overlayService.open();
    }

    closeOverlay():void {
        this.OverlayRef.close();
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

    private setNonGeoMap(imageURL: string): void {
        this.ActiveMapType = ActiveMapType.NonGeo;

        this.clearMap();
        this.map = L.map('map', {
            crs: L.CRS.Simple,
            maxZoom: 2,
        });
        var bounds = [[0,0], [1200,1200]];
        L.imageOverlay(imageURL, bounds).addTo(this.map);
        this.map.fitBounds(bounds);
    }

    private clearMap(): void {
        if (this.map != undefined) {
            this.map.off();
            this.map.remove();
        }
    }

    private addRoomIconToMap(x: number, y: number, popupTitle: string, popupBody: string, popupLink): void {
        var icon = L.latLng([ y, x ]);
        L.marker(icon).addTo(this.map).bindPopup(`<div class="popup-container"><h3>${popupTitle}</h3><p>${popupBody}</br><a href="${popupLink}" target="_blank">Wyszukiwarka prowadzących</a></p></div>`);
    }

    private retriveFloor(room: string): string {
        var numberRegex = /^[0-9]*$/g
        if (room[0] === '0') {
            return '0';
        } else {
            if (room.length > 2 && numberRegex.test(room[2])) {
                switch(room[0]) {
                    case '1': {
                        return '2';
                    }
                    case '2': {
                        return '3';
                    }
                    case '3': {
                        return '4';
                    }
                    default: {
                        return 'err';
                    }
                }
            } else {
                return '1'
            }
        }
        
    }
}
