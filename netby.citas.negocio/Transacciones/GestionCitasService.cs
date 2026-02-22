using Microsoft.EntityFrameworkCore;
using netby.citas.negocio.Dto;
using netby.citas.negocio.Modelos;
using netby.citas.negocio.Utilitarios;
using netby.persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netby.citas.negocio.Transacciones
{
    public class GestionCitasService : ICita, IVehiculo
    {

        private readonly CitasDbContext _context;

        public GestionCitasService(CitasDbContext context)
        {
            _context = context;
        }

        public ResultadoDto CrearVehiculo(VehiculoDto vehiculo)
        {

            var resultado = new ResultadoDto();
            var vt = new Vehiculo();
            vt.Placa = vehiculo.Placa;
            vt.Detalle = vehiculo.Detalle;
            vt.FechaRegistro = DateTime.Now;
            vt.UsuarioRegistra = vehiculo.UsuarioRegistra;
            _context.Vehiculos.Add(vt);
            resultado.Correcto = true;
            return resultado;

        }

        public ResultadoDto CrearCita(CitaDto cita)
        {

            var resultado = new ResultadoDto();

            if (_context.Cita.Any(t => t.FechaCita == cita.FechaCita))
            {

                throw new CitasException("Ya existe una cita para esta fecha en este horario", "CTA0003");
            }

            if (cita.FechaCita.DayOfWeek >= DayOfWeek.Monday && cita.FechaCita.DayOfWeek <= DayOfWeek.Friday)
            {

                var hora8 = new TimeSpan(8, 0, 0);
                var hora14 = new TimeSpan(14, 0, 0);
                if (cita.FechaCita.TimeOfDay >= hora8
                    && cita.FechaCita.TimeOfDay <= hora14
                    && cita.FechaCita.Minute % 30 == 0
                    && cita.FechaCita.Second == 0
                    && cita.FechaCita.Millisecond == 0)
                {

                    var vt = new Vehiculo();
                    var vehiculoP = _context.Vehiculos.FirstOrDefault(t => t.Placa == cita.Vehiculo.Placa);
                    if (vehiculoP != null)
                    {

                        vt = vehiculoP;
                        vt.FechaActualiza = DateTime.Now;
                        vt.UsuarioActualiza = cita.UsuarioRegistra;
                    }

                    vt.Placa = cita.Vehiculo.Placa;
                    vt.Detalle = cita.Vehiculo.Detalle;
                    vt.FechaRegistro = DateTime.Now;
                    vt.UsuarioRegistra = cita.UsuarioRegistra;
                    vt.Activo = true;

                    var ct = new Citum();
                    ct.FechaCita = cita.FechaCita;
                    ct.Descripcion = cita.Descripcion;
                    ct.UsuarioRegistra = cita.UsuarioRegistra;
                    ct.Activo = true;
                    vt.Cita.Add(ct);
                    if (vehiculoP == null)
                        _context.Vehiculos.Add(vt);
                }
                else
                {

                    throw new CitasException("Hora fuera de rago, se puede agendar entre las 8am a 2pm cada 30 min.", "CTA0002");
                }
            }
            else
            {

                throw new CitasException("Dia de la semana fuera de rango, debe ser de lunes a Viernes", "CTA0001");
            }

            resultado.Correcto = true;
            return resultado;
        }

        public ResultadoDto<List<CitaDto>> ObtenerCitas(string placa)
        {

            var ctaLst = new List<CitaDto>();
            var citas = _context.Cita.Where(t => t.SecuencialVehiculoNavigation.Placa == placa && t.Activo == true && t.FechaCita.Date >= DateTime.Now.Date).ToList();

            foreach (var cita in citas)
            {

                var cdto = new CitaDto();
                cdto.FechaCita = cita.FechaCita;
                cdto.UsuarioRegistra = cita.UsuarioRegistra;
                cdto.Descripcion = cita.Descripcion;
                cdto.SecuencialVehiculo = cita.SecuencialVehiculo;
                cdto.Secuencial = cita.Secuencial;
                cdto.Vehiculo = new VehiculoDto
                {
                    Placa = cita.SecuencialVehiculoNavigation.Placa,
                    Secuencial = cita.SecuencialVehiculoNavigation.Secuencial

                };

                ctaLst.Add(cdto);
            }

            var resultado = new ResultadoDto<List<CitaDto>>();
            resultado.Correcto = true;
            resultado.Anexo = new List<CitaDto>();
            resultado.Anexo.AddRange(ctaLst);
            return resultado;
        }

        public ResultadoDto<VehiculoDto> ObtenerVehiculo(string placa)
        {
            throw new NotImplementedException();
        }
    }
}
