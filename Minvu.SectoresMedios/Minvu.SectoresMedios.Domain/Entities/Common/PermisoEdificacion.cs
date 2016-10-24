using Minvu.SectoresMedios.Domain.Controls.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Minvu.SectoresMedios.Domain.Validators;

namespace Minvu.SectoresMedios.Domain.Entities.Common
{
    public class PermisoEdificacion
    {
        public int? peh_id { get; set; }
        public int? sol_id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "N° del documento (*)")]
        [Range(1, 99999999, ErrorMessage = "Números mayores que 0")]
        public string peh_num { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'-'MM'-'yy}")]
        [Display(Name = "Fecha de ingreso del documento (*)")]
        public DateTime? peh_fec { get; set; }

        public DateTime? peh_fec_sol { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Range(1, 99999999, ErrorMessage = "Números mayores que 0")]
        [Display(Name = "N° de viviendas indicadas en el documento (*)")]
        public int? peh_num_viv { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Tipo de documento (*)")]
        public int? peh_tip_doc { get; set; }

        public DateTime? peh_fec_pry_aprb { get; set; }

        public int? tempId { get; set; }

        public List<SelectListItem> DDLtipoDocumento { get; set; }

        [MinvuValidators.RequeridoSegunValor("peh_tip_doc", "4", "Requerido")]
        [Display(Name = "Descripción de las modificaciones al proyecto")]
        public string descripcionModificaciones { get; set; }

        public PermisoEdificacion()
        {
            this.DDLtipoDocumento = DropDownListCommon.ObtenerDDLPorDominio("TIPO_DOC_PROY_DS19");
        }
    }
}
