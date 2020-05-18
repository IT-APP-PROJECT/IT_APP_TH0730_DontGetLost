import { Point } from './Point.VM'

export interface Map {
    Floor: number;
    Building: string;
    FloorNumber: number;
    PathPoints: Point[];
    MapId: number;
}