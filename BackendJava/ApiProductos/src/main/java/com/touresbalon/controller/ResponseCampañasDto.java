package com.touresbalon.controller;

import java.util.List;

public class ResponseCampañasDto {

	public String codigo;
	public String mensaje ;
	public int cantidadRegistros;
	public int paginaActual;
	public int totalPaginas;
	public int tamanoPagina;
	public List<Campañas> data;
	
	
	public String getCodigo() {
		return codigo;
	}
	public void setCodigo(String codigo) {
		this.codigo = codigo;
	}
	public String getMensaje() {
		return mensaje;
	}
	public void setMensaje(String mensaje) {
		this.mensaje = mensaje;
	}
	public int getCantidadRegistros() {
		return cantidadRegistros;
	}
	public void setCantidadRegistros(int cantidadRegistros) {
		this.cantidadRegistros = cantidadRegistros;
	}
	public int getPaginaActual() {
		return paginaActual;
	}
	public void setPaginaActual(int paginaActual) {
		this.paginaActual = paginaActual;
	}
	public int getTotalPaginas() {
		return totalPaginas;
	}
	public void setTotalPaginas(int totalPaginas) {
		this.totalPaginas = totalPaginas;
	}
	public int getTamanoPagina() {
		return tamanoPagina;
	}
	public void setTamanoPagina(int tamanoPagina) {
		this.tamanoPagina = tamanoPagina;
	}
	public List<Campañas> getData() {
		return data;
	}
	public void setData(List<Campañas> campañas) {
		this.data = campañas;
	}
	
	
	
	
	
}
