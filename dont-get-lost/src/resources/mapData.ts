import { Floor } from './../view-models/FloorVM';
import { Building } from './../view-models/BuildingVM';
import { IconType } from './../view-models/IconVM';

export let MapData = {
    'buildings': [{
        'type': 'FeatureCollection',
        'features': [
            {
                'type': 'Feature',
                'properties': {
                    'name': 'Budynek C4',
                    'popupContent': 'Budynek Politechniki Wrocławskiej C4',
                },
                'geometry': {
                    'type': 'LineString',
                    'coordinates': [
                        [
                            17.059358954429626,
                            51.109232711333135,
                        ],
                        [
                            17.059246301651,
                            51.10909293980368,
                        ],
                        [
                            17.0597505569458,
                            51.10893295982344,
                        ],
                        [
                            17.05987125635147,
                            51.10907104783907,
                        ],
                        [
                            17.059358954429626,
                            51.109232711333135,
                        ],
                    ],
                },
            },
            {
                'type': 'Feature',
                'properties': {
                    'name': 'Budynek C3',
                    'amenity': 'Baseball Stadium',
                    'popupContent': 'Budynek Politechniki Wrocławskiej C3',
                },
                'geometry': {
                    'type': 'LineString',
                    'coordinates': [
                        [
                            17.059873938560486,
                            51.109072731836726,
                        ],
                        [
                            17.0606330037117,
                            51.10883528756324,
                        ],
                        [
                            17.060517668724057,
                            51.10869383082047,
                        ],
                        [
                            17.0597505569458,
                            51.10893295982344,
                        ],
                        [
                            17.059873938560486,
                            51.109072731836726,
                        ],
                    ],
                },
            },],
    }]
}

let floors = [
    {Number: '0', Display: 'Piwnica'}, 
    {Number: '1', Display: 'Parter'}, 
    {Number: '2', Display: 'P1'},
    {Number: '3', Display: 'P2'},
    {Number: '4', Display: 'P3'},
]

let C3 = {Name: 'C3', Floors: floors};
let C4 = {Name: 'C4', Floor: floors};

export let buildings = [C3, C4];

export let IconTitles = [
    {Type: IconType[0], Title: 'Winda'},
    {Type: IconType[1], Title: 'Automat'},
    {Type: IconType[2], Title: 'Szatnia'},
    {Type: IconType[3], Title: 'Łazienka'},
    {Type: IconType[4], Title: 'Klatka schodowa'},
]