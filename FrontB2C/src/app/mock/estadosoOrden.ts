
export const EstadosOrden: estadoOrden[] = [
    { codigo:'A' ,valor:'Activa'},
    { codigo:'C' ,valor:'Cancelada'},
    { codigo:'PE' ,valor:'Pendiente'},
    { codigo:'PA' ,valor:'Pagada'},
    { codigo:'I' ,valor:'Inactiva'}, 
    { codigo:'R' ,valor:'Rechazada'},
    { codigo:'AP' ,valor:'Aprobada'}
];

export class estadoOrden {
    codigo: string;
    valor: string;
  }