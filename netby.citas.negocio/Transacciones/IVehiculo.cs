using netby.citas.negocio.Dto;
using netby.citas.negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netby.citas.negocio.Transacciones
{
    public interface IVehiculo
    {

        ResultadoDto CrearVehiculo(VehiculoDto vehiculo);
        ResultadoDto<VehiculoDto> ObtenerVehiculo(string placa);
    }
}
