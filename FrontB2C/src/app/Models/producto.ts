

import {hotel} from './hotel';
import {transporte} from './transporte';


export class Producto {
    uuidPaqueteProducto:string;
    codigo: number;
    titulo: string;
    precio: number;
    descripcion: string;
     /**info transporte */
     evento: string;
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