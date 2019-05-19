export class RequestJsonClientes {
    tipoDocumento:string;
    Pagina:number;
    TotalRegistros:number;
    RegistroXPagina:number;

toJSON = () => ({
    Pagina: this.Pagina,
    TotalRegistros: this.TotalRegistros,
    RegistroXPagina: this.RegistroXPagina,
    tipoDocumento: this.tipoDocumento
  });
}