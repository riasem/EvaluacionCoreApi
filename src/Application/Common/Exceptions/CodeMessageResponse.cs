using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Common.Exceptions
{
    public static class CodeMessageResponse
    {
        public static string GetMessageByCode(string code, string value = "")
        {
            IDictionary<string, string> codes = new Dictionary<string, string>();

            codes.Add("000", "Consulta generada exitosamente");
            codes.Add("001", "La consulta no retorna datos");
            codes.Add("002", "Ocurrió un error durante la consulta");

            codes.Add("100", $"{value} guardado exitosamente");
            codes.Add("101", $"No se pudo registrar {value}");
            codes.Add("102", "Ocurrió un error durante el registro");

            codes.Add("200", $"{value} actualizado exitosamente");
            codes.Add("201", $"No se pudo actualizar {value}");

            codes.Add("300", $"{value} eliminado exitosamente");
            codes.Add("301", $"No se pudo eliminar {value}");

            codes.Add("400", "Petición Incorrecta");
            codes.Add("401", "Usted no está autorizado");
            codes.Add("402", "Error de validación");
            codes.Add("403", "Usted no tiene permisos sobre este recurso");
            codes.Add("404", "Recurso no encontrado");

            codes.Add("500", $"Error interno del servidor");
            codes.Add("501", $"Error en conexión con {value}");

            return codes[code];
        }
    }
}
