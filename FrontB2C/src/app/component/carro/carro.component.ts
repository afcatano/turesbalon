import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { CarritoService } from '../../service/carrito.service';
import { Producto } from '../../Models/producto';
import {AppComponent} from '../../app.component';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import {StorageService} from '../../storage/storage.service'
import { Router } from '@angular/router';
@Component({
  selector: 'app-carro',
  templateUrl: './carro.component.html',
  styleUrls: ['./carro.component.css']
})
export class CarroComponent implements OnInit {

  public carrito: Array<Producto> = [];
  public subscription: Subscription;
  public total: number;
  progressBar=false
  params:any;
  countOrders=0;
  optionActual="C";
  session:any;
  totalOrders=0;
  constructor(private router:Router,
    private parent: AppComponent, private carritoService:CarritoService, private storageCompra:StorageParamsCompraService, private storageUser:StorageService) { }

  ngOnInit() {
    this.cargarCarrito();
  }

  cargarCarrito(){
    this.session = this.storageCompra.getParamsCompraSession();
    this.params={};
    if(this.session)
    {
      var valorTotal=0;
      this.params.opionPaquete=this.session.optionPaquete
      
      if(this.session.orden.Evento!= undefined )
       valorTotal= (this.session.orden.Evento.ValorEvento !=null? this.session.orden.Evento.ValorEvento:0)*(this.session.orden.Evento.Cantidad);
       if(this.session.orden.Transporte!= undefined)
        valorTotal=valorTotal+ ((this.session.orden.Transporte.valor !=null? this.session.orden.Transporte.valor:0)*(this.session.orden.Transporte.numSillas));
      if(this.session.orden.Hotel!= undefined)//&& this.session.orden.Transporte.IdReservaHotel!= undefined)
        valorTotal= valorTotal + ((this.session.orden.Hotel.valor !=null? this.session.orden.Hotel.valor:0)*(this.session.orden.Hotel.cantidadPersonas));

      this.session.orden.precio =valorTotal;
      this.totalOrders=valorTotal;
      this.carrito=[];//TODO
      this.carrito.push(this.session.orden);
      this.countOrders=this.carrito.length;
    }
    this.params.nombrePaso= this.optionActual;//esta en el paso de consultar eventos
    
      console.log(this.carrito);
  }

  eliminarItem(item){
    this.session = null;
    this.carrito= null;
    this.params ={};
    this.storageCompra.setParamsCompraSession(this.session);
    this.countOrders= 0;
    this.totalOrders=0;
    console.log("Elimina orden");
  }


  //Metodo que agrega la cantidad a un hotel, transporte o evento
  removerProducto(producto, type){
    this.storageCompra.setModItemsParamsCompraSession(producto,type);
    this.cargarCarrito();
  }

  //Metodo que elimina  la cantidad a un hotel, transporte o evento
  addProducto(producto,type) {
    this.storageCompra.setAddItemsParamsCompraSession(producto,type);
    this.cargarCarrito();
  }

  //Metodo que elimina  el elemento sea  un hotel, transporte o evento
  delteItemProducto(producto,type){
    this.storageCompra.setDeleteItemsParamsCompraSession(producto,type);
    this.cargarCarrito();
  }

  pagar(){
    console.log("pagar");

    var user= this.storageUser.getCurrentUser();
    if(user){
      
      this.session.orden.userid=user.userid;
      this.session.orden.precio=this.totalOrders;
      this.storageCompra.setParamsCompraSession(this.session);
      this.router.navigate(['crearOrden']);

    }else{
      this.parent.openDialog( "","Debe primero iniciar sesion !!.","Alerta");
    }

  }
}
