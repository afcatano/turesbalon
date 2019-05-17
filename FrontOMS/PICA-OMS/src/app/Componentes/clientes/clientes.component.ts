import { Component, OnInit } from '@angular/core';
import {TiposDocumento} from '../../mock/tipoDocumentos';
import {Categorias} from '../../mock/categorias';
import {buscadorPaginacion} from '../../Models/busquedaPaginacion';
import {consulta} from '../../Models/Customer';
import { AppComponent } from '../../app.component';
import {ClientesService} from '../../service/clientes.service';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  data= {tipoDocumento:""};
  datacat= {categoria:""};

  dataTiposDocumento = TiposDocumento;
  dataCategorias = Categorias; 
  
  progressBar= false;
  params = {
    tmIni: null,
    tmFin: null,
    cliente: null,
    cantidad: 0,
    page: 0,//Variable para almacenar la pagina actual
    pageSize: 21,  // Variable para almacenar la cantidad de resultados por pagina
    categoria: null,

 
    accion:""
  }
  dataCount: number=0;//tamaño para el paginador
  dataClientes:consulta[];

  constructor(private parent: AppComponent ,private serviceCustomer :ClientesService) { } 
  ngOnInit() {
  }
  
  //metodo que consulta clientes
  onConsultar(){
      this.progressBar=true;
      let parametros = this.params;
      console.log("getDataSource()", parametros);
      var data=new buscadorPaginacion();
      data.pagina=this.params.page;
      data.tamanoPagina=this.params.pageSize;
      data.nombre=this.params.cliente;
      data.fechaInicial="2017-09-09";
      data.fechaFinal="2022-09-09";//TODO
      data.codigo =this.params.categoria; //TODO - Categoria
      this.serviceCustomer.GetCustomer(data,result =>{
           
        if(result.codigo=='0'){
           this.dataCount=result.cantidadRegistros;// ==0?1:result.cantidadRegistros;//TODO
           this.params.page = result.paginaActual;
           this.params.pageSize = result.tamanoPagina;
           console.log(this.dataEventos);
           if(result.eventos){
                this.dataEventos=result.eventos;
          }
          }else{
            if(result.mensaje)
              this.parent.openDialog( "",result.mensaje,"Alerta");
            else
              this.parent.openDialog( "","Servidor no disponible","Alerta");
         }
          this.progressBar=false;
      });
  }
}
