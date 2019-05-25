export class RequestJsonClientes {
    tipoDocumento:string;
    Pagina:number;
    TotalRegistros:number;
    RegistroXPagina:number;
    documento:number;
    estadoCliente:string;
    TipoEvento:string;
    FechaIniFact:string;
    FechaFinFact:string;

toJSON = () => ({
    Pagina: this.Pagina,
    TotalRegistros: this.TotalRegistros,
    RegistroXPagina: this.RegistroXPagina,
    tipoDocumento: this.tipoDocumento,
    documento: this.documento,
    estadoCliente: this.estadoCliente,
    TipoEvento:this.TipoEvento,
    FechaIniFact:this.FechaIniFact,
    FechaFinFact:this.FechaFinFact
  });
}