

import {hotel} from './hotel';
import {transporte} from './transporte';
import {Evento} from './evento';

export class Producto {
    uuidPaqueteProducto:string;
    codigo: number;
    titulo: string;
    precio: number;
    descripcion: string;
    imagen: string;
    ciudad:string
     /**info transporte */
     evento: Evento;
     inicioEvento:string;
     finEvento:string;
     cantidad:string;
     /**info transporte */
     transporte: transporte;
     horaSalida:string;
     cantidadTransporte:string;
     /**info hotel */
     hotel: hotel;
     reservaInicial:string;
     reservaFinal:string;
     cantidadHotel:string;
}