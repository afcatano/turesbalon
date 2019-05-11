import { Component, OnInit } from '@angular/core';
import  {TiposDocumento} from '../../mock/tipoDocumentos';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  
  constructor() { }

  dataTiposDocumento = TiposDocumento;
  
  ngOnInit() {
  }

}
