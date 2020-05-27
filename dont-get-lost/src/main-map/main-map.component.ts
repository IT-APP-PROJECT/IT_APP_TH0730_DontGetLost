import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import * as L from 'leaflet';
import { Subject, Observable } from 'rxjs';
import axios from "axios";

import { MapData, buildings, IconTitles, corridorCoordinates } from '../resources/mapData';
import { Map } from './../view-models/MapVM';
import { Room } from './../view-models/RoomVM';
import { Icon, IconType } from './../view-models/IconVM';
import { Floor } from './../view-models/FloorVM';
import { Building } from './../view-models/BuildingVM';
import { OverlayService } from './../services/overlay.service';
import { OverlayComponentRef } from './../overlay/overlay-component-ref';
import { Point } from 'src/view-models/PointVM';

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
    IsNavigationEnabled = false;
    CorridorCoordinates = corridorCoordinates;
    PointsOnHall: Point[];

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
        this.PointsOnHall = [];
        this.setGeoMap();
    }

    ngAfterViewInit(): void {
        this.OnMenuClosedEvent$ = this.Menu.closedStart;
        this.OnMenuOpenedEvent$ = this.Menu.openedStart;
        this.OnMenuOpenedEvent$.subscribe(() => this.IsMenuOpened = true);
        this.OnMenuClosedEvent$.subscribe(() => setTimeout(() => this.IsMenuOpened = false, 200));
        this.NavigationFormFroup.controls['InitialRoomFormControl'].valueChanges.subscribe((value: string) => {
            this.ClearInitialRoomBtnVisibility = this.stringHasValue(value);
            this.checkNavigationData();
        });
        this.NavigationFormFroup.controls['FinalRoomFormControl'].valueChanges.subscribe((value: string) => {
            this.ClearFinalRoomBtnVisibility = this.stringHasValue(value);
            this.checkNavigationData();
        });
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    toggleMenu(): void {
        if (!this.Menu.opened) {
            this.InitialBuilding = this.AvailableBuildings[1];
            this.FinalBuilding = this.AvailableBuildings[1];
        }
        this.Menu.toggle();
    }

    toggleFloorLayer(floorNumber: string): void {
        this.clearNavigationData();
        let mapId = `${this.ActiveBuilding.Name}-0${floorNumber}`;
        this.ActiveFloor = this.ActiveBuilding.Floors.find((floor: Floor) => floor.Number === floorNumber);
        this.setupLayerData(mapId, true);
    }

    toggleNavigation(): void {
        this.clearNavigationData();
        let floorNumber = this.retriveFloor(this.NavigationFormFroup.controls['InitialRoomFormControl'].value);
        let mapId = `${this.InitialBuilding.Name}-0${floorNumber}`;
        this.ActiveBuilding =  this.InitialBuilding;
        this.InactiveBuildingName = this.FinalBuilding.Name;
        this.ActiveFloor = this.ActiveBuilding.Floors.find((floor: Floor) => floor.Number === floorNumber);
        this.toggleMenu();
        this.setupLayerData(mapId, true);
    }

    switchBuilding(): void {
        let floor;
        if (this.ActiveBuilding === this.InitialBuilding) {
            this.ActiveBuilding = this.FinalBuilding;
            this.InactiveBuildingName = this.InitialBuilding.Name;
            floor = this.retriveFloor(this.NavigationFormFroup.controls['FinalRoomFormControl'].value);
        } else {
            this.ActiveBuilding = this.InitialBuilding;
            this.InactiveBuildingName = this.FinalBuilding.Name
            floor = this.retriveFloor(this.NavigationFormFroup.controls['InitialRoomFormControl'].value);
        }
        this.toggleFloorLayer(floor);
    }

    switchMapMode(): void {
        this.clearNavigationData();
        this.ActiveBuilding = this.AvailableBuildings[1];
        this.InitialBuilding = this.ActiveBuilding;
        this.FinalBuilding = this.AvailableBuildings[0];
        this.InactiveBuildingName = this.FinalBuilding.Name;
        this.ActiveFloor = this.ActiveBuilding.Floors[0];
        if (this.ActiveMapType === ActiveMapType.Geo) {
            this.setupLayerData('C4-00', false)
        } else {
            this.switchToGeoMap();
        }
    }

    async setupLayerData(mapId: string, navigationMode: boolean) {
        this.toggleOverlay();
        await this.getFloorImageURL(mapId);
        await this.getNonGeoMap(mapId);
        await this.getMapRooms(mapId);
        await this.getMapIcons(mapId);
        if (navigationMode) {
            this.findPath(mapId);
        }
        this.closeOverlay();
    }

    getNonGeoMap = async (mapId: string) => {
        let response = await axios({  method: "GET", url: `${this.API_BASE_URL}/pathPoints?mapName=${mapId}`});
        response.data.forEach((element: any) => {
            this.CurrentMap.PathPoints.push({x: element.x, y: element.y})
        });
        this.CurrentMap.MapId =  mapId;
    }

    getMapIcons = async (mapId: string) => {
        let response = await axios({  method: "GET", url: `${this.API_BASE_URL}/icons?mapName=${mapId}`});
        response.data.forEach((icon: any) => {
            this.CurrentMapIcons.push({MapId: icon.mapName, Type: icon.type, X: icon.coordinates.x, Y: icon.coordinates.y});
            this.CurrentMapIcons.forEach((icon: Icon) => {
                this.getCustomIconImage(icon.Type, icon.X, icon.Y);
            });
        });
    }

    getMapRooms = async (mapId: string) => {
        let response = await axios({  method: "GET", url: `${this.API_BASE_URL}/rooms?mapName=${mapId}`});
        response.data.forEach((room: any) => {
            this.CurrentMapRooms.push({MapId: room.mapName, Title: room.name, Description: room.description, X: room.coordinates.x, Y: room.coordinates.y, Link: room.url});
        });
        this.CurrentMapRooms.forEach((room: Room) => {
            this.addRoomIconToMap(room.X, room.Y, room.Title, room.Description, room.Link);
        });
    }

    getFloorImageURL = async (svgName: string) => {
        let response = await axios({  method: "GET", url: `${this.API_BASE_URL}/images?name=${svgName}`});
        this.SVGLink = response.data.url;
        this.setNonGeoMap(this.SVGLink);
    }

    getCustomIconImage = async (iconType: IconType, x: number, y: number) => {
        let response = await axios({  method: "GET", url: `${this.API_BASE_URL}/images?name=${IconType[iconType]}`})
        let popupTitle = this.IconTitles.find((iconTitle: any) => iconTitle.Type === IconType[iconType]).Title;
        var coordinates = L.latLng([ y, x ]);
        var customIcon = L.icon({
            iconUrl: response.data.url,
            className: "custom-icon",
            shadowUrl: '',
        
            iconSize:     [30, 30], 
            shadowSize:   [0, 0],
            iconAnchor:   [15, 15], 
            shadowAnchor: [0, 0], 
            popupAnchor:  [0, -15]
        });
        L.marker(coordinates, {icon: customIcon}).addTo(this.map).bindPopup('<h3>' + popupTitle + '</h3>');
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

    clearNavigationData(): void {
        this.CurrentMap = {MapId: '', PathPoints: []};
        this.CurrentMapIcons = [];
        this.CurrentMapRooms = [];
    }

    checkNavigationData(): void {
        if(this.InitialBuilding && this.FinalBuilding && 
           this.stringHasValue(this.NavigationFormFroup.controls['InitialRoomFormControl'].value) &&
           this.stringHasValue(this.NavigationFormFroup.controls['FinalRoomFormControl'].value)) {
            this.IsNavigationEnabled = true;
        } else {
            this.IsNavigationEnabled = false;
        }
    }

    drawPath(points: Point[]): void {
        let convertedPoints = this.convertToLatLang(points);
        new L.Polyline(convertedPoints, {
            color: 'orange',
            weight: 8,
            opacity: 0.9,
            smoothFactor: 1
        }).addTo(this.map);
    }

    convertToLatLang(points: Point[]): any {
        let latLang = [];
        points.forEach((point: Point) => {
            latLang.push(L.latLng([ point.y, point.x ]));
        });
        return latLang;
    }

    findPath(mapId: string) {
        let nearestExit;
        let initialRoom = this.CurrentMapRooms.find((room: Room) => room.Title === this.NavigationFormFroup.controls['InitialRoomFormControl'].value);
        if (initialRoom) {
            let initialRoomPoint = {x: initialRoom.X, y: initialRoom.Y};
            this.drawPath(this.pointToHall(initialRoomPoint, mapId));
        }
        let finalRoom = this.CurrentMapRooms.find((room: Room) => room.Title === this.NavigationFormFroup.controls['FinalRoomFormControl'].value);
        if (finalRoom) {
            let finalRoomPoint = {x: finalRoom.X, y: finalRoom.Y};
            this.drawPath(this.pointToHall(finalRoomPoint, mapId));
        }
        if (this.InitialBuilding === this.FinalBuilding && !this.isSameFloor()) {
            let exitPoints = [];
            let exitsIcons = this.CurrentMapIcons.filter((icon: Icon) => icon.Type === IconType['Stairs']);
            exitsIcons.forEach((icon: Icon) => {
                let point = {x: icon.X, y: icon.Y};
                exitPoints.push(point);
            });
            if (initialRoom) {
                nearestExit = this.findNearestPoint({x: initialRoom.X, y: initialRoom.Y}, exitPoints, 1000);
                this.drawPath(this.pointToHall(nearestExit, mapId));
            } else if (finalRoom) {
                nearestExit = this.findNearestPoint({x: finalRoom.X, y: finalRoom.Y}, exitPoints, 1000);
                this.drawPath(this.pointToHall(nearestExit, mapId))
            }
        } 
        if (this.InitialBuilding !== this.FinalBuilding) {
            let exitPoints = [];
            let exitsIcons = this.CurrentMapIcons.filter((icon: Icon) => icon.Type === IconType['Stairs']);
            exitsIcons.forEach((icon: Icon) => {
                let point = {x: icon.X, y: icon.Y};
                exitPoints.push(point);
            });
            if (initialRoom) {
                if (this.ActiveBuilding.Name === 'C3') {
                    let maxX = Math.max.apply(Math, this.CurrentMap.PathPoints.map(function(o) { return o.x }));
                    nearestExit = this.CurrentMap.PathPoints.find((point: Point) => point.x === maxX);
                    this.PointsOnHall.push(nearestExit);
                } else {
                    nearestExit = this.findNearestPoint({x: initialRoom.X, y: initialRoom.Y}, exitPoints, 1000);
                    this.drawPath(this.pointToHall(nearestExit, mapId));
                }
            } else if (finalRoom) {
                if (this.ActiveBuilding.Name === 'C3') {
                    let maxX = Math.max.apply(Math, this.CurrentMap.PathPoints.map(function(o) { return o.x }));
                    nearestExit = this.CurrentMap.PathPoints.find((point: Point) => point.x === maxX);
                    this.PointsOnHall.push(nearestExit);
                } else {
                    nearestExit = this.findNearestPoint({x: finalRoom.X, y: finalRoom.Y}, exitPoints, 1000);
                    this.drawPath(this.pointToHall(nearestExit, mapId))
                }
            }
        }
        this.drawPath(this.PointsOnHall);
        this.PointsOnHall = [];
    }

    private pointToHall(point: Point, mapId: string): Point[] {
        let path = [];
        let corridor = this.CorridorCoordinates.get(mapId);
        path.push(point);
        path.push(this.findNearestPoint(point, this.CurrentMap.PathPoints, 150));
        while(path[path.length - 1].y !== corridor) {
            path.push(this.findNearestPointDistance(path[path.length - 1], path, 5));
        }
        this.PointsOnHall.push(path[path.length - 1]);
        return path;
    }

    private findNearestPointDistance(initialPoint: Point, selectedPoints: Point[], distance: number): Point {
        let nearestPoint;
        let points = this.CurrentMap.PathPoints.filter((nextPoint: Point) => {
            return Math.sqrt(Math.pow(Math.abs(initialPoint.x - nextPoint.x),2)  + Math.pow(Math.abs(initialPoint.y - nextPoint.y),2)) === distance;
        });
        points.some((point: Point) => {
            if (point.x !== selectedPoints[selectedPoints.length - 2].x || point.y !== selectedPoints[selectedPoints.length - 2].y) {
                nearestPoint = point;
                return true;
            }
        });
        return nearestPoint;
    }

    private findNearestPoint(initialPoint: Point, points: Point[], distance: number): Point {
        let currentDistance;
        let nearestPoint: Point;
        points.forEach((nextPoint: Point) => {
            currentDistance = Math.sqrt(Math.pow(Math.abs(initialPoint.x - nextPoint.x),2)  + Math.pow(Math.abs(initialPoint.y - nextPoint.y),2));
            if (currentDistance < distance) {
                distance = currentDistance;
                nearestPoint = nextPoint;
            }
        });
        return nearestPoint;
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

    private switchToGeoMap(): void {
        this.ActiveMapType = ActiveMapType.Geo;
        this.clearMap();
        this.map = L.map('map').setView([51.109070, 17.05953], 18);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(this.map);
        this.BuildingsLayerGroup.addTo(this.map);
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

    private isSameFloor(): boolean {
        return this.retriveFloor(this.NavigationFormFroup.controls['InitialRoomFormControl'].value) ===
            this.retriveFloor(this.NavigationFormFroup.controls['FinalRoomFormControl'].value) ? true : false;
    }

    private retriveFloor(room: string): string {
        var numberRegex = /^[0-9]*$/g
        if (room[0] === '0' || room.length === 0) {
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
