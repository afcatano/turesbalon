import {Injectable} from "@angular/core";
import { Router } from '@angular/router';

//Clase encargada de cargar en localstorage los parametros basicos
@Injectable()
export class StorageParamsCompraService {
  private localStorageService;
  private ParamsCompraSession : any;

  constructor(private router: Router) {
    this.localStorageService = localStorage;
    this.ParamsCompraSession = this.loadSessionData();
  };
  setParamsCompraSession(session: any): void {
    this.ParamsCompraSession = session;
    this.localStorageService.setItem('ParamsCompra', JSON.stringify(session));
  };
  loadSessionData(): any{
    var sessionStr = this.localStorageService.getItem('ParamsCompra');
    return (sessionStr) ?  JSON.parse(sessionStr) : null;
  };
  getParamsCompraSession(): any {
    return this.ParamsCompraSession;
  };
  removeParamsCompraSession(): void {
    this.localStorageService.removeItem('ParamsCompra');
    this.ParamsCompraSession = null;
  };
 
}