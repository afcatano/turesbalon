package com.touresbalon.controller;


import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

import org.springframework.cache.annotation.Cacheable;

@Entity
@Table(name = "Campañas")
@Cacheable("Campañas")
public class Campañas {
	 
	
	@Id
	@Column(name = "id")
	Integer Id;
	 
	@Column(name = "codigo")
	String codigo;
	
	@Column(name = "descripcion")
	String descripcion;
	
	@Column(name = "paquete")
	String campaña;
	
	@Column(name = "fechacreacion")
	Date fechaInicio;
	
	@Column(name = "id_producto")
	Long id_producto;
	
	
	@Column(name = "precio")
	Long precio;
	
	@Column(name = "cantidad")
	Long cantidad;
	

	@Column(name = "imagen")
	String imagen;
	
	@Column(name = "fechafin")
	Date fechaFin;

	public Integer getId() {
		return Id;
	}

	public void setId(Integer id) {
		Id = id;
	}

	public String getCodigo() {
		return codigo;
	}

	public void setCodigo(String codigo) {
		this.codigo = codigo;
	}

	public String getDescripcion() {
		return descripcion;
	}

	public void setDescripcion(String descripcion) {
		this.descripcion = descripcion;
	}

	public String getCampaña() {
		return campaña;
	}

	public void setCampaña(String campaña) {
		this.campaña = campaña;
	}

	public Date getFechaInicio() {
		return fechaInicio;
	}

	public void setFechaInicio(Date fechaInicio) {
		this.fechaInicio = fechaInicio;
	}

	public Long getId_producto() {
		return id_producto;
	}

	public void setId_producto(Long id_producto) {
		this.id_producto = id_producto;
	}

	public Long getPrecio() {
		return precio;
	}

	public void setPrecio(Long precio) {
		this.precio = precio;
	}

	public Long getCantidad() {
		return cantidad;
	}

	public void setCantidad(Long cantidad) {
		this.cantidad = cantidad;
	}

	public String getImagen() {
		return imagen;
	}

	public void setImagen(String imagen) {
		this.imagen = imagen;
	}

	public Date getFechaFin() {
		return fechaFin;
	}

	public void setFechaFin(Date fechaFin) {
		this.fechaFin = fechaFin;
	}

	
	

	
}
