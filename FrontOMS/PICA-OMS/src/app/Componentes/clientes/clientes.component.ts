import { Component, OnInit } from '@angular/core';
import {TiposDocumento} from '../../mock/tipoDocumentos';
import {Categorias} from '../../mock/categorias';
import {RequestJsonClientes} from '../../Models/RequestJsonClientes';
import {Cliente} from '../../Models/Customer';
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
  dataClientes:Cliente[];

  dataTiposDocumento = TiposDocumento;
  dataCategorias = Categorias; 
  
  progressBar= false;
  params = {
    Pagina: 1,//Variable para almacenar la pagina actual
    pageSize: 5,  // Variable para almacenar la cantidad de resultados por pagina
    TotalRegistros: 0,
    categoria: null,
    tipoDocumento: "1"
  }
  private dataCount: number=0;//tamaño para el paginador

  constructor(private parent: AppComponent ,private serviceCustomer :ClientesService) { } 
  ngOnInit() {
  }
  
  //metodo que consulta clientes
  onConsultar(){
      this.progressBar=true;
      let parametros = this.params;
      console.log("onConsultar()", parametros);
      var data=new RequestJsonClientes();
      data.Pagina =this.params.Pagina;
      data.RegistroXPagina =this.params.pageSize;
      data.TotalRegistros = this.dataCount;
      data.tipoDocumento = this.params.tipoDocumento;
      //data.nombre=this.params.cliente;
      //data.fechaInicial="2017-09-09";
      //data.fechaFinal="2022-09-09";//TODO
      //data.codigo =this.params.categoria; //TODO - Categoria
      this.serviceCustomer.getClientes(data,result =>{
           
        if(result.codigo=='0'){
           this.dataCount=result.cantidadRegistros;// ==0?1:result.cantidadRegistros;//TODO
           this.params.Pagina = result.paginaActual;
           this.params.pageSize = result.tamanoPagina;
           this.params.TotalRegistros = result.cantidadRegistros;
           console.log(this.dataClientes);
           if(result.clientes){
                this.dataClientes=result.clientes;
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

//Metodo que se ejecuta cuando cambia la pagina
onPaginateChange(event) {
    this.params.Pagina = event.pageIndex + 1;
    this.params.pageSize = event.pageSize;
   //event
    //this.params
    this.onConsultar();
  }

}
