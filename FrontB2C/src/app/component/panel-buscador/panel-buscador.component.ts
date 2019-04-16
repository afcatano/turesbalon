import { Component, OnInit ,Output,EventEmitter} from '@angular/core';
import {paqueteInfo} from '../../Models/paqueteInfo';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-panel-buscador',
  templateUrl: './panel-buscador.component.html',
  styleUrls: ['./panel-buscador.component.css']
})
export class PanelBuscadorComponent implements OnInit {

  //Evento que envia la consulta realizada
  @Output() action = new EventEmitter<paqueteInfo>()

  paquete={ categoria:"",nombre:"", fechaInicial:"", fechaFinal:"", cantidad:"" }
  constructor() { }

  ngOnInit() {
  }


  onSubmit() {
    console.log("Event emitter");
    var paquete = new paqueteInfo(this.paquete.categoria, this.paquete.nombre,this.paquete.fechaInicial,this.paquete.fechaFinal, this.paquete.cantidad);
    this.action.emit(paquete);
   }
}