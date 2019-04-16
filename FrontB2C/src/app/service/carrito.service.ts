
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import * as Rx from 'rxjs/Rx';
import { Producto } from '../Models/producto';
import { Observable } from 'rxjs';
import { ReplaySubject } from 'rxjs';
import { BehaviorSubject ,} from 'rxjs';
import { v4 as uuid } from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {

  private subject: BehaviorSubject<Producto[]> = new BehaviorSubject([]);
  private itemsCarrito: Producto[] = [];

  constructor() {
    this.subject.subscribe(data => this.itemsCarrito = data);
  }

  /**
   * addCarrito
   * @param producto
   */
  addCarrito(producto: Producto) {
    producto.uuidPaqueteProducto= uuid();//Agrega id unico para el producto
    this.subject.next([...this.itemsCarrito, producto]);
    console.log("articulo agregado");
  }

  /**
   * clearCarrito
   */
  clearCarrito() {
    this.subject.next(null);
  }

  /**
   * getCarrito
   */
  getCarrito(): Observable<Producto[]> {
    return this.subject;
  }

  /**
   * deteleItem
   */
  deteleItem(producto: Producto): Observable<Producto[]> {

   /* this.subject.forEach( function(valor, indice, array) {
      console.log("En el índice " + indice + " hay este valor: " + valor);
  });
    */
    return this.subject;
  }


 

  /**
   * getTotal
   */
  getTotal() {
    return this.itemsCarrito.reduce((total, producto: Producto) => { return total + producto.precio; }, 0);
  }

  


}