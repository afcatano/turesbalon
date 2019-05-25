

import {hotel} from './hotel';
import {Transporte} from './transporte';
import {Evento} from './evento';

export class Producto {
    uuidPaqueteProducto:string;
    codigo: number;
    userid:number
    precio: number;
    titulo: string;
    descripcion: string;
    imagen: string;
    ciudad:string
    tipoDetalle:string;
    
     /**info transporte */
     Evento: Evento;
     inicioEvento:string;
     finEvento:string;
     cantidad:string;
     /**info transporte */
     Transporte: Transporte;
     horaSalida:string;
     cantidadTransporte:string;
     /**info hotel */
     Hotel: hotel;
     reservaInicial:string;
     reservaFinal:string;
     cantidadHotel:string;

     toJSON = () => ({
         IdUsuario: this.userid,
         ValorTotal: this.precio,
            Evento: {
              CodigoEvento: this.Evento.CodigoEvento,
              NombreEvento: this.Evento.NombreEvento,
              DescEvento: this.Evento.DescEvento,
              FechaEvento: this.Evento.FechaEvento,
              ValorEvento: this.Evento.ValorEvento,
              ciudad:this.Evento.ciudad,
              imagen:this.Evento.imagen,
              inicioEvento:this.Evento.inicioEvento,
              finEvento:this.Evento.finEvento,
              Cantidad:this.Evento.cantidadPersonas,
              esInternacional:this.Evento.esInternacional
            },
            Hotel: {
              IdReservaHotel: this.Hotel ? this.Hotel.codigo:null,
              NombreHotel: this.Hotel ? this.Hotel.nombre:null,
              NumeroHabitacion: this.Hotel ? this.Hotel.numeroHabitacion:null,
              Direccion: this.Hotel ? this.Hotel.direccion:null,
              Pais: this.Hotel ? this.Hotel.pais:null,
              Ciudad: this.Hotel ? this.Hotel.ciudad:null,
              FechaEntrada: this.Hotel ? this.Hotel.fechaEntrada:null,
              FechaSalida: this.Hotel ? this.Hotel.fechaSalida:null,
              TipoHotel: this.Hotel ? this.Hotel.tipoHotel:null,
              ValorHotel: this.Hotel ? this.Hotel.valor:null,
              EmpresaHotel: this.Hotel ? this.Hotel.proveedor:null,
            },
            Transporte: {
              IdReservaTransporte: this.Transporte ? this.Transporte.codigo:null,
              PaisOrigen: this.Transporte ? this.Transporte.paisOrigen:null,
              PaisDestino:this.Transporte ? this.Transporte.paisDestino:null,
              CiudadOrigen: this.Transporte ? this.Transporte.ciudadOrigen:null,
              CiudadDestino: this.Transporte ? this.Transporte.ciudadDestino:null,
              NumSillas: this.Transporte ? this.Transporte.numSillas:null,
              FechaIdaDespegue:this.Transporte ? this.Transporte.fechaIda:null,
              FechaIdaAterrizaje: this.Transporte ? this.Transporte.fechaRegreso:null,
              FechaRegresoDespegue: this.Transporte ? this.Transporte.fechaRegresoDespegue:null,
              FechaRegresoAterrizaje: this.Transporte ? this.Transporte.fechaRegresoAterrizaje:null,
              ValorTransporte: this.Transporte ? this.Transporte.valor:null,
              EmpresaTransporte:this.Transporte ? this.Transporte.proveedor:null,
            }
          
        
      })
}