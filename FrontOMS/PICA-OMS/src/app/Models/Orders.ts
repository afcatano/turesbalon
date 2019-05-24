export class Orders {
    public CodigoOrden: string;
    public TipoConsulta: string;
    public FechaOrden: string;
    public EstadoOrden: string;
    public ValorOrden: number;
    public IdUsuario: string;
    public IdTipo: string;
    public IdNumero: string;
    public TotalVendido: number;
    public TotalRegistros: number;
    public NombreEstadoOrden:string;
    public Pagina:number;
    public RegsPorPagina:number;
    public ConDetalle:boolean;
    public CodigoEvento:string;

  toJSON = () => ({
    CodigoOrden: this.CodigoOrden,
    TipoConsulta: this.TipoConsulta,
    FechaOrden: this.FechaOrden,
    EstadoOrden: this.EstadoOrden,
    ValorOrden: this.ValorOrden,
    IdTipo: this.IdTipo,
    IdNumero: this.IdNumero,
    TotalVendido: this.TotalVendido,
    TotalRegistros: this.TotalRegistros,
    Pagina: this.Pagina,
    RegsPorPagina: this.RegsPorPagina,
    ConDetalle: this.ConDetalle,
    CodigoEvento: this.CodigoEvento
  });
}

