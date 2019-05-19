import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DialogData } from '../../Models/messageDialog';
import { ProductosService} from '../../service/productos.service';


@Component({
  selector: 'app-datalle-producto',
  templateUrl: './datalle-producto.component.html',
  styleUrls: ['./datalle-producto.component.css']
})
export class DatalleProductoComponent  {

  constructor(
    public dialogRef: MatDialogRef<DatalleProductoComponent>,private service:ProductosService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  

  openCrear(data){
    console.log("crear");
    this.service.crearProductos(data).subscribe(result =>{
      console.log(result);
      if(result.codigo){
        console.log(result.codigo);
      }else{

      }
      this.dialogRef.close();
    }); 
  }

  openAlmacenar(data){
    console.log("almacenar");
    console.log(data.estado);
    this.service.actualizarProductos(data).subscribe(result =>{
      console.log(result);
      if(result.codigo){
        console.log(result.codigo);
      }else{

      }
      this.dialogRef.close();
    }); 
  }

  openEliminar(data){
    console.log("eliminar");
    data.estado="Inactivo";
    this.service.actualizarProductos(data).subscribe(result =>{
      console.log(result);
      if(result.codigo){
        console.log(result.codigo);
      }else{

      }
      this.dialogRef.close();
    }); 
  }
}
