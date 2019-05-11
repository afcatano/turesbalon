import { Component, OnInit , Input } from '@angular/core';

@Component({
  selector: 'app-pasos-compra',
  templateUrl: './pasos-compra.component.html',
  styleUrls: ['./pasos-compra.component.css']
})
export class PasosCompraComponent implements OnInit {
  @Input() params: any;
  
  pasos:any[];

  constructor() { }

  ngOnInit() {
    console.log("inicia");
    console.log(this.params);
    if(this.params){
      this.pasos = [];
      console.log("Pasos");
      console.log(this.params);
      var numPasos=0;
      switch(this.params.opionPaquete){
  
         case 'FULL':
                    this.pasos.push({paso:1,nombre:"Evento",estado:this.params.nombrePaso=='E'?true:false});
                    this.pasos.push({paso:0});
                    this.pasos.push({paso:2,nombre:"Hotel",estado:this.params.nombrePaso=='H'?true:false});
                    this.pasos.push({paso:0});
                    this.pasos.push({paso:3,nombre:"Transporte",estado:this.params.nombrePaso=='T'?true:false});
                    numPasos=4;
                    break;
         case 'ET':
                     this.pasos.push({paso:1,nombre:"Evento",estado:this.params.nombrePaso=='E'?true:false});
                    this.pasos.push({paso:0});
                    this.pasos.push({paso:2,nombre:"Transporte",estado:this.params.nombrePaso=='T'?true:false});
                    numPasos=3;
                    break;
         case 'EH':
                    this.pasos.push({paso:1,nombre:"Evento",estado:this.params.nombrePaso=='E'?true:false});
                    this.pasos.push({paso:0});
                    this.pasos.push({paso:2,nombre:"Hotel",estado:this.params.nombrePaso=='H'?true:false});
                    numPasos=3;
                    break;
         default:
                    this.pasos.push({paso:1,nombre:"Evento",estado:this.params.nombrePaso=='E'?true:false});
                    numPasos=2;
                    break;
  
      }

      this.pasos.push({paso:0});
      this.pasos.push({paso:numPasos,nombre:"Checkint",estado:this.params.nombrePaso=='C'?true:false});
     

    }

  }

}
