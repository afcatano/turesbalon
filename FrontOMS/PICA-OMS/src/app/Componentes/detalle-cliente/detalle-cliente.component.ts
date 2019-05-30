import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DialogData } from '../../Models/messageDialog';
import { AppComponent } from '../../app.component';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
import {ClientesService} from '../../service/clientes.service';
import {Categorias} from '../../mock/categorias';
import {TiposDocumento} from '../../mock/tipoDocumentos';
import {Cliente} from '../../Models/Customer';

@Component({
  selector: 'app-detalle-cliente',
  templateUrl: './detalle-cliente.component.html',
  styleUrls: ['./detalle-cliente.component.css']
})
export class DetalleClienteComponent {

  datadoc = {tipoDocumento:""};
  datacat= {categoria:""};

  dataTiposDocumento = TiposDocumento;
  dataCategorias = Categorias; 
  dataClientes = new Cliente();

  constructor(public dialogRef: MatDialogRef<DetalleClienteComponent>,private service :ClientesService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    }
 
  onNoClick(): void {
      this.dialogRef.close();
    }

    
    onGuardar(data){
      this.dataClientes.apellido = data.apellido;
      this.dataClientes.userid = data.userid;
      this.dataClientes.nombre = data.nombre;
      this.dataClientes.ciudad = data.ciudad;
      this.dataClientes.telefono = data.telefono;
      this.dataClientes.correo = data.correo;
      this.dataClientes.pais = data.pais;
      this.dataClientes.direccion = data.direccion;
      this.dataClientes.tipoDocumento = data.IDtipoDocumento;
      this.dataClientes.documento = data.documento;
      this.dataClientes.estadoCliente = data.IDEstadoCliente;

      console.log("Actualiza");
      console.log(data.IDEstadoCliente);
      this.service.updateUser(this.dataClientes).subscribe(result =>{
        console.log(result);
        if(result.codigo){
          console.log(result.codigo);
        }else{
  
        }
        this.dialogRef.close(result.mensaje);
      }); 
    }
  
    onCrear(data){
      this.dataClientes.apellido = data.apellido;
      this.dataClientes.userid = data.userid;
      this.dataClientes.nombre = data.nombre;
      this.dataClientes.ciudad = data.ciudad;
      this.dataClientes.telefono = data.telefono;
      this.dataClientes.correo = data.correo;
      this.dataClientes.pais = data.pais;
      this.dataClientes.direccion = data.direccion;
      this.dataClientes.tipoDocumento = data.IDtipoDocumento;
      this.dataClientes.documento = data.documento;
      this.dataClientes.estadoCliente = data.IDEstadoCliente;
      this.dataClientes.checkTarjeta = false;
      this.dataClientes.username =  data.nombre + "123";
      this.dataClientes.password =  data.nombre + "123";
      this.dataClientes.confirmarContrasena =  data.nombre + "123";

      console.log("crear");
      this.service.registerUser(this.dataClientes).subscribe(result =>{
        console.log(result);
        if(result.codigo){
          console.log(result.codigo);
        }else{
  
        }
        this.dialogRef.close(result.mensaje);
      }); 
    }

    
    onSalir(){
      this.dialogRef.close();
    }
}
