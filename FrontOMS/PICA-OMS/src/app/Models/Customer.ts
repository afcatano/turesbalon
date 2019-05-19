export class Cliente {
    public nombre: string;
    public apellido: string;
    public telefono: number;
    public correo: string;
    public ciudad: string;
    public departamento: string;
    public pais: string;
    public direccion: string;
    public tipoDocumento: string;
    public documento: number;
    public TotalFacturado: number;
    public TotalRegistros: number;

  toJSON = () => ({
    nombre: this.nombre,
    apellido: this.apellido,
    telefono: this.telefono,
    correo: this.correo,
    ciudad: this.ciudad,
    departamento: this.departamento,
    pais: this.pais,
    direccion: this.direccion,
    tipoDocumento: this.tipoDocumento,
    documento: this.documento,
    TotalFacturado: this.TotalFacturado,
    TotalRegistros: this.TotalRegistros
  });
}

