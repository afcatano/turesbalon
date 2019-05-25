import {Injectable} from "@angular/core";
import { Router } from '@angular/router';

//Clase encargada de cargar en localstorage los parametros basicos
@Injectable()
export class StorageParamsCompraService {
  private localStorageService;
  private ParamsCompraSession : any;

  constructor(private router: Router) {
    this.localStorageService = localStorage;
    this.ParamsCompraSession = this.loadSessionData();
  };
  setParamsCompraSession(session: any): void {
    this.ParamsCompraSession = session;
    console.log(session);
    this.localStorageService.setItem('ParamsCompra', JSON.stringify(session));
  };

  setAddItemsParamsCompraSession(items, type): void {
      var listCarrito = this.getParamsCompraSession();
      console.log("Agregar item al carrito");
      var carrito=[];
      if(listCarrito){
        if(listCarrito.orden){
          switch(type){
  
            case "E":
                    if(listCarrito.orden.Evento)
                      listCarrito.orden.Evento.Cantidad = parseInt(listCarrito.orden.Evento.Cantidad) +1;
                  break;
            case "T":
                      if(listCarrito.orden.Transporte)
                      listCarrito.orden.Transporte.numSillas = parseInt(listCarrito.orden.Transporte.numSillas) +1;
                  break;
            case "H":
                      if(listCarrito.orden.Hotel)
                      listCarrito.orden.Hotel.cantidadPersonas = parseInt(listCarrito.orden.Hotel.cantidadPersonas) +1;
                  break;
          }
          this.setParamsCompraSession(listCarrito);
        }
       
    };
  }


  setDeleteItemsParamsCompraSession(items, type): void {
    console.log("Elimina item al carrito");
    var listCarrito = this.getParamsCompraSession();
    var carrito=[];
    if(listCarrito){
     
      if(listCarrito.orden){
        switch(type){

          case "E":
                    listCarrito.orden.Evento=null;
                break;
          case "T":
                    listCarrito.orden.Transporte= null;
                break;
          case "H":
                    listCarrito.orden.Hotel=null;
                break;
        }
        this.setParamsCompraSession(listCarrito);
      }
     
  };
}

  setModItemsParamsCompraSession(items, type): void {
    console.log("Elimina item al carrito");
    var listCarrito = this.getParamsCompraSession();
    var carrito=[];
    if(listCarrito){
    
      if(listCarrito.orden){
        switch(type){

          case "E":
                  if(listCarrito.orden.Evento)
                    if(listCarrito.orden.Evento.Cantidad!=0)
                      listCarrito.orden.Evento.Cantidad = parseInt(listCarrito.orden.Evento.Cantidad) -1;
                break;
          case "T":
                    if(listCarrito.orden.Transporte)
                    if(listCarrito.orden.Transporte.numSillas!=0)
                    listCarrito.orden.Transporte.numSillas = parseInt(listCarrito.orden.Transporte.numSillas) -1;
                break;
          case "H":
                    if(listCarrito.orden.Hotel)
                    if(listCarrito.orden.Hotel.cantidadPersonas!=0)
                    listCarrito.orden.Hotel.cantidadPersonas = parseInt(listCarrito.orden.Hotel.cantidadPersonas) -1;
                break;
        }
        this.setParamsCompraSession(listCarrito);
      }
    
  };
  }



  loadSessionData(): any{
    var sessionStr = this.localStorageService.getItem('ParamsCompra');
    return (sessionStr) ?  JSON.parse(sessionStr) : null;
  };
  getParamsCompraSession(): any {
    return this.ParamsCompraSession;
  };
  removeParamsCompraSession(): void {
    this.localStorageService.removeItem('ParamsCompra');
    this.ParamsCompraSession = null;
  };
 
}