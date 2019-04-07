import { Component, OnInit } from '@angular/core';
import {Hoteles } from '../../mock/hoteles';

import {Session } from '../../Models/session';
import { StorageService } from '../../storage/storage.service';
import {User } from '../../Models/User';
import {ProductsService} from '../../service/products.service'
import {CarritoService} from '../../service/carrito.service';
import {AppComponent} from '../../app.component';
import {Producto}from '../../Models/producto';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-hoteles',
  templateUrl: './hoteles.component.html',
  styleUrls: ['./hoteles.component.css']
})
export class HotelesComponent implements OnInit {
  userInfo :User;
  ordenesCount:number;
  private subscription: Subscription;
 
  infoTable: any[];
  processing:boolean;
  
  constructor(private sesion:StorageService, private productService: ProductsService, private parent: AppComponent, private carritoService:CarritoService) { 

  }

  ngOnInit() {
    this.action();
  }

  action(){
    console.log("Eschua evento");
    this.processing=true;
    //console.log(this.username, this.password);
    var params={fecha:""}
    this.productService.hotels(params).subscribe(
      result => {
            console.log(result);
            this.infoTable = result;
            this.ordenesCount=this.infoTable.length; 
      },
      error => {
          console.log(error);
          this.processing = false;
          this.parent.openDialog( "","Servidor no disponible","Alerta");
          this.infoTable = Hoteles;
      })
     
  }

  private correctLogin(data: User){
    var sessionData= new Session();
    sessionData.user=data;
   // this.storageService.setCurrentSession(sessionData);
    //this.router.navigate(['/']);
  }

  addProducto(item) {
  console.log(item);
  var producto = new Producto();
  producto.hotel=item; 
  this.carritoService.addCarrito(producto);
  }

}
