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
    public estadoCliente: any;
    public userid: number;
    public IDEstadoCliente: number
    public IDtipoDocumento: number
    public checkTarjeta: boolean
    public username: string
    public password: string
    public confirmarContrasena: string

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
    TotalRegistros: this.TotalRegistros,
    estadoCliente: this.estadoCliente,
    userid: this.userid,
    checkTarjeta: this.checkTarjeta,
    username: this.username,
    password: this.password,
    confirmarContrasena: this.confirmarContrasena
  });
}

