import { Component, OnInit } from '@angular/core';
import { PasarelaService} from '../../service/pasarela.service';
import {AppComponent} from '../../app.component';
import { Router } from '@angular/router';
import { StorageService } from '../../storage/storage.service';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import  {anos} from '../../mock/anos';
import  {meses} from '../../mock/mes';
import {franquicias} from '../../mock/franquicia';
@Component({
  selector: 'app-pasarela',
  templateUrl: './pasarela.component.html',
  styleUrls: ['./pasarela.component.css']
})
export class PasarelaComponent implements OnInit {

  progressBar=false;
  verificar=false;
  tieneTarjetaRegistrar=false;
  sessionUser:any;
  session:any;
  valorTotal:number;
  dataMeses = meses;
  dataAnos = anos;
  dataFranquicias= franquicias;
  constructor(private storageCompra:StorageParamsCompraService,private sesion:StorageService,private pasarelaService:PasarelaService, private parent:AppComponent,private router:Router) { }

  ngOnInit() {
    this.sessionUser = this.sesion.getCurrentUser();
    this.session = this.storageCompra.getParamsCompraSession();

   this.tieneTarjetaRegistrar = this.sessionUser.numeroTarjeta ?true:false;
   //Total de ocmpra
   this.valorTotal=this.session.orden.ValorTotal;
  
  }

  veryfy(){

   var params={
                Tipo: "2",
                CodigoSeguridad: "000",
                FechaVencimiento: "202305",
                Numero: "4050123366775500"
               }

    this.progressBar= true;
    this.pasarelaService.pasarelaVerificar(params).subscribe(
      result =>{
        console.log("Entra a verificar pasarela");
        if(result.Codigo=="0"){
          this.parent.openDialog( "",result.Mensaje,"Alerta");
          this.verificar= true;
        }else{
          this.parent.openDialog( "",result.Mensaje,"Alerta");
        }
        this.progressBar=false;
      }
      ,
      error => {
        console.log("Error al verificar pasarela:" +error);
        console.log(error);
        this.parent.openDialog( "","Servidor no disponible","Alerta");
        this.progressBar=false;
      }
    );

  }


  pay(){
     var params={
        Tipo: "2",
        CodigoSeguridad: "000",
        FechaVencimiento: "202305",
        Numero: "4050123366775500",
        Valor: "200"
      }

      this.progressBar= true;
      this.pasarelaService.pasarelaPagar(params).subscribe(
      result =>{
          console.log("Entra a pagar pasarela");
          if(result.Codigo=="0"){
            this.parent.openDialog( "",result.Mensaje,"Alerta");
            this.verificar= true;
            this.router.navigate(['estadocompra']);
          }else{
            this.parent.openDialog( "",result.Mensaje,"Alerta");
          }
            this.progressBar=false;
      }
      ,
      error => {
          console.log("Error al pagar pasarela:" +error);
          console.log(error);
          this.parent.openDialog( "","Servidor no disponible","Alerta");
          this.progressBar=false;
      }
      );
   }

}
