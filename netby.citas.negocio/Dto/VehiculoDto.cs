using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netby.citas.negocio.Dto
{
    public class VehiculoDto
    {

        [JsonProperty("secuencial")]
        public int Secuencial { get; set; }

        [JsonProperty("placa")]
        [MaxLength(10, ErrorMessage = "Longitud fuera de rango")]
        public string Placa { get; set; } = null!;

        [JsonProperty("detalle")]
        [MaxLength(50, ErrorMessage = "Longitud fuera de rango")]

        public string Detalle { get; set; } = null!;

        [JsonProperty("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [JsonProperty("fechaActualiza")]
        public DateTime FechaActualiza { get; set; }

        [JsonProperty("activo")]
        public bool Activo { get; set; }

        [JsonProperty("usuarioRegistra")]
        [MaxLength(10, ErrorMessage = "Longitud fuera de rango")]

        public string UsuarioRegistra { get; set; } = null!;

        [JsonProperty("usuarioActualiza")]
        [MaxLength(10, ErrorMessage = "Longitud fuera de rango")]

        public string UsuarioActualiza { get; set; } = null!;

        [JsonProperty("citas")]
        public List<CitaDto> Citas { get; } = new List<CitaDto>();
    }

}
