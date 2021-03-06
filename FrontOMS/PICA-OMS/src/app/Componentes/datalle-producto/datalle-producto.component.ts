import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DialogData } from '../../Models/messageDialog';
import { ProductosService} from '../../service/productos.service';

import {FormControl} from '@angular/forms';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';

@Component({
  selector: 'app-datalle-producto',
  templateUrl: './datalle-producto.component.html',
  styleUrls: ['./datalle-producto.component.css']
})
export class DatalleProductoComponent  {

  mensajeError="";
  fechaEvento="";
  v_initialDate = new FormControl(new Date());
  v_endDate = new FormControl(new Date());
  serializedDate = new FormControl((new Date()).toISOString());
  
  public initialDate;//Valor donde se almacena el rango de fecha inicial 
  public endDate;//Valor donde se almacena el rango de fecha final
  public rangeDate:boolean=false; //Bandera para activar o inactivar el rango de fecha

  //Constantes
  v_minInitialDate = new Date(2018, 4, 1);
  v_maxDateFist = new Date(2030, 4, 1);
  v_minEndDate = new Date(2018, 4, 1);
  v_maxEndDate = new Date(2030, 0, 1);
  day:number=24*60*60*1000;
  maxRange:number=31 //Maximo del rango;

  //Variables
  minInitialDate = this.v_minInitialDate;
  maxDateFist = this.v_maxDateFist;
  minEndDate = this.v_minEndDate;
  maxEndDate = this.v_maxEndDate;

  constructor(
    public dialogRef: MatDialogRef<DatalleProductoComponent>,private service:ProductosService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  

  openCrear(data){
    console.log("crear");
    data.FechaEvento=this.fechaEvento;
    data.estado="Activo";
    this.service.crearProductos(data).subscribe(result =>{
      console.log(result);
      if(result.codigo){
        console.log(result.codigo);
      }else{

      }
      this.dialogRef.close(result.mensaje);
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
      this.dialogRef.close(result.mensaje);
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
      this.dialogRef.close(result.mensaje);
    }); 
  }

   //destino que captura el input de los rangos de fecha
   addEvent(type: string, event: MatDatepickerInputEvent<Date>, dateInput: string) {
    var fecha = event.value;
    console.log(fecha);
    if(fecha && fecha!=null){
      if("endDate"==dateInput)
      {
        this.endDate= fecha;//.getFullYear() +"-"+fecha.getMonth()+"-"+fecha.getDate();
        console.log("endDate");
        console.log(fecha.getFullYear()+"-"+fecha.getMonth()+"-"+ fecha.getDate());
        this.fechaEvento=fecha.getFullYear()+"-"+(fecha.getMonth().toString().length==1?"0"+fecha.getMonth():fecha.getMonth())+"-"+(fecha.getDate().toString().length==1?"0"+fecha.getDate():fecha.getDate());
     }
      
    }else{
      if("endDate"==dateInput)
       {
          this.endDate= undefined;
           console.log("entra");
        }
      
    }
   }
}
