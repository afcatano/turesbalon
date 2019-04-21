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
    this.session = this.storageCompra.getParamsCompraSession();
    this.params={};
    if(this.session)
    {
      var valorTotal=0;
      this.params.opionPaquete=this.session.optionPaquete
      
      if(this.session.orden.Evento!= undefined )
       { 
         valorTotal= (this.session.orden.Evento.ValorEvento !=null? this.session.orden.Evento.ValorEvento:0)*(this.session.orden.Evento.cantidadPersonas);
       }
      if(this.session.orden.Transporte!= undefined )
        valorTotal=valorTotal+ ((this.session.orden.Transporte.valor !=null? this.session.orden.Transporte.valor:0)*(this.session.orden.Transporte.numSillas));
      if(this.session.orden.Hotel!= undefined )
        valorTotal= valorTotal + ((this.session.orden.Hotel.valor !=null? this.session.orden.Hotel.valor:0)*(this.session.orden.Evento.cantidadPersonas));

      this.session.orden.precio =valorTotal;
      this.totalOrders=valorTotal;
      this.carrito.push(this.session.orden);
      this.countOrders=this.carrito.length;
    }
    this.params.nombrePaso= this.optionActual;//esta en el paso de consultar eventos
    
    /*this.carritoService.getCarrito().subscribe(data => {
      console.log(data);
      this.carrito = data;
     // this.total = this.carritoService.getTotal();
    },
      error => alert(error));*/

    
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
