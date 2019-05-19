package com.touresbalon.controller;


import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

import org.springframework.cache.annotation.Cacheable;

@Entity
@Table(name = "Producto")
public class Producto {
	 
	@Id
	@Column(name = "id")
	Integer Id;
	 
	@Column(name = "espectaculo")
	String NombreEvento;
	
	
	@Column(name = "codigo")
	String CodigoEvento;
	
	@Column(name = "descripcion")
	String DescEvento;
	
	@Column(name = "ciudad")
	String ciudad;
	
	@Column(name = "fecha")
	Date FechaEvento;
	
	@Column(name = "horainicio")
	String horaInicio;
	
	@Column(name = "horafin")
	String horaFin;
	
	@Column(name = "precio")
	Long ValorEvento;
	
	@Column(name = "cantidad")
	Long cantidad;
	

	@Column(name = "imagen")
	String imagen;
	
	@Column(name = "usuario")
	String usuario;

	public Integer getId() {
		return Id;
	}

	public void setId(Integer id) {
		Id = id;
	}

	public String getNombreEvento() {
		return NombreEvento;
	}

	public void setNombreEvento(String nombreEvento) {
		NombreEvento = nombreEvento;
	}

	public String getCodigoEvento() {
		return CodigoEvento;
	}

	public void setCodigoEvento(String codigoEvento) {
		CodigoEvento = codigoEvento;
	}

	public String getDescEvento() {
		return DescEvento;
	}

	public void setDescEvento(String descEvento) {
		DescEvento = descEvento;
	}

	public String getCiudad() {
		return ciudad;
	}

	public void setCiudad(String ciudad) {
		this.ciudad = ciudad;
	}

	public Date getFechaEvento() {
		return FechaEvento;
	}

	public void setFechaEvento(Date fechaEvento) {
		FechaEvento = fechaEvento;
	}

	public String getHoraInicio() {
		return horaInicio;
	}

	public void setHoraInicio(String horaInicio) {
		this.horaInicio = horaInicio;
	}

	public String getHoraFin() {
		return horaFin;
	}

	public void setHoraFin(String horaFin) {
		this.horaFin = horaFin;
	}

	public Long getValorEvento() {
		return ValorEvento;
	}

	public void setValorEvento(Long valorEvento) {
		ValorEvento = valorEvento;
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

	public String getUsuario() {
		return usuario;
	}

	public void setUsuario(String usuario) {
		this.usuario = usuario;
	}

	

	
}
