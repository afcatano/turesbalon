import { Component, OnInit,Input } from '@angular/core';

@Component({
  selector: 'app-navegador',
  templateUrl: './navegador.component.html',
  styleUrls: ['./navegador.component.css']
})
export class NavegadorComponent implements OnInit {

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
     

  }
}

}