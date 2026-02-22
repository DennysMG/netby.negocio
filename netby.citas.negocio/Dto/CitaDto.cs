using Microsoft.EntityFrameworkCore;
using netby.persistencia.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netby.citas.negocio.Dto
{
    public class CitaDto
    {

        [JsonProperty("secuencial")]
        public int Secuencial { get; set; }

        [JsonProperty("fechaCita")]
        public DateTime FechaCita { get; set; }

        [JsonProperty("descripcion")]
        [MaxLength(500, ErrorMessage = "Longitud fuera de rango")]
        public string? Descripcion { get; set; }

        [JsonProperty("secuencialVehiculo")]
        public int SecuencialVehiculo { get; set; }

        [JsonProperty("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [JsonProperty("fechaActualiza")]

        public DateTime FechaActualiza { get; set; }

        [JsonProperty("activo")]
        public bool Activo { get; set; }

        [JsonProperty("usuarioRegistra")]
        [MaxLength(10,ErrorMessage = "Longitud fuera de rango")]
        public string UsuarioRegistra { get; set; } = null!;

        [JsonProperty("usuarioActualiza")]
        [MaxLength(10, ErrorMessage = "Longitud fuera de rango")]
        public string UsuarioActualiza { get; set; } = null!;

        [JsonProperty("Vehiculo")]
        public VehiculoDto Vehiculo { get; set; } = null!;
    }
}
