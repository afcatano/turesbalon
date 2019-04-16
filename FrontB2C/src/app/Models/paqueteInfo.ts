export class paqueteInfo{
   
  public categoria: string;
  public nombre: string;
  public fechaFinal: string;
  public fechaInicial: string;
  public cantidad: number;

  constructor(
    categoria,
    nombre,
    fechaFinal,
    fechaInicial,
    cantidad
 ) {
   this.categoria = categoria;
   this.nombre = nombre;
   this.fechaFinal = fechaFinal;
   this.fechaInicial = fechaInicial; 
   this.cantidad = cantidad; 
 }

 getCategoria = () => {
   return this.categoria;
 }

 setCategoria = (categoria) => {
   this.categoria = categoria;
 }

}