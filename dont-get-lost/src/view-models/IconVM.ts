export interface Icon {
    X: number;
    Y: number;
    Type: IconType;
    MapId: number;
}

export enum IconType {
    Elevator, 
    Vendor,
    Cloakroom
}