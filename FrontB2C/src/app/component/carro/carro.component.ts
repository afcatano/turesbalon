import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { CarritoService } from '../../service/carrito.service';
import { Producto } from '../../Models/producto';

@Component({
  selector: 'app-carro',
  templateUrl: './carro.component.html',
  styleUrls: ['./carro.component.css']
})
export class CarroComponent implements OnInit {

  private carrito: Array<Producto> = [];
  private subscription: Subscription;
  private total: number;

  constructor(private carritoService: CarritoService) { }

  ngOnInit() {
    this.carritoService.getCarrito().subscribe(data => {
      console.log(data);
      this.carrito = data;
     // this.total = this.carritoService.getTotal();
    },
      error => alert(error));
  }

}
