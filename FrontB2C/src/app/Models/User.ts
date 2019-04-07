export class User {

  public username: string;
  public password: string;
  public nombre: string;
  public apellido: string;
  public telefono: number;
  public correo: string;
  public ciudad: string;
  public departamento: string;
  public pais: string;
  public confirmarContrasena: string;
  public checkTarjeta:boolean;
  public direccion: string;
  public tipoDocumento: string;
  public documento: number;

  public codigoTarjeta: number;
  public fechaTarjeta: string;
  public numeroTarjeta: number;
  public franquiciaTarjeta: string;

  constructor(
     username,
     password,
     nombre,
     apellido,
     telefono,
     correo,
     ciudad,
     departamento,
     pais,
     confirmarContrasena,
     checkTarjeta,
     direccion,
     tipoDocumento,
     documento,
     codigoTarjeta,
      fechaTarjeta,
      numeroTarjeta,
      franquiciaTarjeta,
  ) {
    this.username = username;
    this.password = password;
    this.nombre=nombre;
    this.apellido= apellido;
    this.telefono=telefono;
    this.correo= correo;
    this.ciudad= ciudad;
    this.departamento= departamento;
    this.pais= pais;
    this.confirmarContrasena= confirmarContrasena;
    this.checkTarjeta=checkTarjeta;

    this.direccion=direccion;
    this.tipoDocumento=tipoDocumento;
    this.documento=documento;
    this.codigoTarjeta=codigoTarjeta;
    this. fechaTarjeta=fechaTarjeta;
    this. numeroTarjeta=numeroTarjeta;
    this. franquiciaTarjeta=franquiciaTarjeta;
  }

  getUsername = () => {
    return this.username;
  }

  setUsername = (username) => {
    this.username = username;
  }

  getPassword = () => {
    return this.username;
  }

  setPassword = (password) => {
    this.password=password;
  }

  getnombre = () => {
    return this.nombre;
  }

  setnombre = (nombre) => {
    this.nombre=nombre;
  }

  getapellido = () => {
    return this.apellido;
  }

  setapellido = (apellido) => {
    this.apellido=apellido;
  }


  gettelefono = () => {
    return this.telefono;
  }

  settelefono = (telefono) => {
    this.telefono=telefono;
  }


  getcorreo = () => {
    return this.correo;
  }

  setcorreo = (correo) => {
    this.correo=correo;
  }

  getciudad = () => {
    return this.ciudad;
  }

  setciudad = (ciudad) => {
    this.ciudad=ciudad;
  }
  getdepartamento = () => {
    return this.departamento;
  }

  setdepartamento = (departamento) => {
    this.departamento=departamento;
  }
  getpais = () => {
    return this.pais;
  }

  setpais = (pais) => {
    this.pais=pais;
  }
  getconfirmarContrasena = () => {
    return this.confirmarContrasena;
  }

  setconfirmarContrasena = (confirmarContrasena) => {
    this.confirmarContrasena=confirmarContrasena;
  }
  getcheckTarjeta = () => {
    return this.checkTarjeta;
  }

  setcheckTarjeta = (checkTarjeta) => {
    this.checkTarjeta=checkTarjeta;
  }

  
  /*
  this.codigoTarjeta=codigoTarjeta;
  this. =fechaTarjeta;
  this. =numeroTarjeta;
  this. =franquiciaTarjeta;*/
  getdireccion = () => {
    return this.direccion;
  }

  setdireccion = (direccion) => {
    this.direccion=direccion;
  }

  gettipoDocumento = () => {
    return this.tipoDocumento;
  }

  settipoDocumento = (tipoDocumento) => {
    this.tipoDocumento=tipoDocumento;
  }


  getdocumento = () => {
    return this.documento;
  }

  setdocumento = (documento) => {
    this.documento=documento;
  }


  getcodigoTarjeta = () => {
    return this.codigoTarjeta;
  }

  setcodigoTarjeta = (codigoTarjeta) => {
    this.codigoTarjeta=codigoTarjeta;
  }


  getfechaTarjeta = () => {
    return this.fechaTarjeta;
  }

  setfechaTarjeta = (fechaTarjeta) => {
    this.fechaTarjeta=fechaTarjeta;
  }

  getfranquiciaTarjeta = () => {
    return this.franquiciaTarjeta;
  }

  setfranquiciaTarjeta = (franquiciaTarjeta) => {
    this.franquiciaTarjeta=franquiciaTarjeta;
  }

  getnumeroTarjeta = () => {
    return this.numeroTarjeta;
  }

  setnumeroTarjeta = (numeroTarjeta) => {
    this.numeroTarjeta=numeroTarjeta;
  }

  toJSON = () => ({
    username: this.username,
    password: this.password,
    nombre: this.nombre,
    apellido :this.apellido,
    telefono :this.telefono,
    correo :this.correo,
    ciudad :this.ciudad,
    departamento :this.departamento,
    pais :this.pais,
    confirmarContrasena  :this.confirmarContrasena,
    checkTarjeta :this.checkTarjeta,
    direccion :this.direccion,
    tipoDocumento :this.tipoDocumento,
    documento :this.documento,
    codigoTarjeta: this.codigoTarjeta,
    fechaTarjeta :this. fechaTarjeta,
    numeroTarjeta: this. numeroTarjeta,
    franquiciaTarjeta :this. franquiciaTarjeta,
    
  })

}
