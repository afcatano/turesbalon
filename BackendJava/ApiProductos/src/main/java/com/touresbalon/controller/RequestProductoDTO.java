package com.touresbalon.controller;


public class RequestProductoDTO {

	public String codigo;
	public String nombre ;
	public String descripcion ;
	public String fechaInicial ;
	public String fechaFinal ;
	public int pagina;
	public int tamanoPagina;
	public String getCodigo() {
		return codigo;
	}
	public void setCodigo(String codigo) {
		this.codigo = codigo;
	}
	public String getNombre() {
		return nombre;
	}
	public void setNombre(String nombre) {
		this.nombre = nombre;
	}
	public String getDescripcion() {
		return descripcion;
	}
	public void setDescripcion(String descripcion) {
		this.descripcion = descripcion;
	}
	public String getFechaInicial() {
		return fechaInicial;
	}
	public void setFechaInicial(String fechaInicial) {
		this.fechaInicial = fechaInicial;
	}
	public String getFechaFinal() {
		return fechaFinal;
	}
	public void setFechaFinal(String fechaFinal) {
		this.fechaFinal = fechaFinal;
	}
	public int getPagina() {
		return pagina;
	}
	public void setPagina(int pagina) {
		this.pagina = pagina;
	}
	public int getTamanoPagina() {
		return tamanoPagina;
	}
	public void setTamanoPagina(int tamanoPagina) {
		this.tamanoPagina = tamanoPagina;
	}
	
	
}
