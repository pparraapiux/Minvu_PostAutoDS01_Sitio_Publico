using Minvu.SectoresMedios.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Minvu.SectoresMedios.Domain.Validators
{
    public static class MinvuValidators
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class RequeridoSegunValor : ValidationAttribute, IClientValidatable
        {
            string nombrePropiedadAComparar;
            string valorRequerido;

            public RequeridoSegunValor(string propiedadAcomparar, string valorRequerido, string errorMessage)
                : base(errorMessage)
            {
                this.nombrePropiedadAComparar = propiedadAcomparar;
                this.valorRequerido = valorRequerido;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var validationResult = ValidationResult.Success;
                try
                {
                    var informacionPropiedad = validationContext.ObjectType.GetProperty(this.nombrePropiedadAComparar);
                    var valorAvalidar = TypeHelper.ConvertObjectToString(value);
                    var valorPropiedad = TypeHelper.ConvertObjectToString(informacionPropiedad.GetValue(validationContext.ObjectInstance, null));

                    if (valorRequerido == valorPropiedad && string.IsNullOrEmpty(valorAvalidar))
                        validationResult = new ValidationResult(ErrorMessageString);

                    //if (informacionPropiedad.PropertyType.Equals(new DateTime().GetType()))
                    //{
                    //    DateTime toValidate = (DateTime)value;
                    //    DateTime referenceProperty = (DateTime)informacionPropiedad.GetValue(validationContext.ObjectInstance, null);
                    //    // if the end date is lower than the start date, than the validationResult will be set to false and return
                    //    // a properly formatted error message
                    //    if (toValidate.CompareTo(referenceProperty) < 1)
                    //    {
                    //        validationResult = new ValidationResult(ErrorMessageString);
                    //    }
                    //}
                    //else
                    //{
                    //    validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return validationResult;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
                string errorMessage = ErrorMessageString;

                // The value we set here are needed by the jQuery adapter
                ModelClientValidationRule regla = new ModelClientValidationRule();
                regla.ErrorMessage = errorMessage;
                regla.ValidationType = "requeridosegunvalor"; // This is the name the jQuery adapter will use
                //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
                regla.ValidationParameters.Add("propiedadacomparar", this.nombrePropiedadAComparar);
                regla.ValidationParameters.Add("valorrequerido", this.valorRequerido);
                yield return regla;
            }
        }

        public class ValidarArchivo : ValidationAttribute, IClientValidatable
        {
            string[] extensiones;
            string ext;

            public ValidarArchivo(string ext)
            {
                this.extensiones = ext.Split(';');
                this.ext = ext;
            }

            public override bool IsValid(object value)
            {
                var file = value as HttpPostedFileBase;

                if (file == null)
                    return true;
                else if (!extensiones.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    return false;
                }
                else
                    return true;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
                string errorMessage = ErrorMessageString;

                // The value we set here are needed by the jQuery adapter
                ModelClientValidationRule regla = new ModelClientValidationRule();
                regla.ErrorMessage = "Solo archivos con extensión: " + string.Join(", ", extensiones);
                regla.ValidationType = "validararchivo"; // This is the name the jQuery adapter will use
                //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
                regla.ValidationParameters.Add("extensiones", this.ext);
                yield return regla;
            }
        }


        public class TamArchivo : ValidationAttribute, IClientValidatable
        {
            int size;

            public TamArchivo(int size)
            {
                this.size = size;
            }

            public override bool IsValid(object value)
            {
                int MaxContentLength = 1024 * 1024 * size; //2 MB

                var file = value as HttpPostedFileBase;

                if (file == null)
                    return true;
                else if (file.ContentLength > MaxContentLength)
                {
                    return false;
                }
                else
                    return true;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
                string errorMessage = "Tamaño máximo del archivo : " + size.ToString() + "MB";

                // The value we set here are needed by the jQuery adapter
                ModelClientValidationRule regla = new ModelClientValidationRule();
                regla.ErrorMessage = errorMessage;
                regla.ValidationType = "tamarchivo"; // This is the name the jQuery adapter will use
                //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
                regla.ValidationParameters.Add("tam", this.size);
                yield return regla;
            }
        }

        public class ValidarRut : ValidationAttribute, IClientValidatable
        {
            public override bool IsValid(object value)
            {
                var rut = value as string;
                var crut = RutHelper.cuerpoRut(rut);
                var dv = RutHelper.dvRut(rut);

                if (RutHelper.validarRut(crut, dv) || rut == null)
                    return true;
                else
                    return false;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                string errorMessage = "El RUT ingresado no es válido";

                // The value we set here are needed by the jQuery adapter
                ModelClientValidationRule regla = new ModelClientValidationRule();
                regla.ErrorMessage = errorMessage;
                regla.ValidationType = "validarrut";
                yield return regla;
            }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class RequeridoArchivoSegunValor : ValidationAttribute, IClientValidatable
        {
            string nombrePropiedadAComparar;
            string valorRequerido;

            public RequeridoArchivoSegunValor(string propiedadAcomparar, string valorRequerido, string errorMessage)
                : base(errorMessage)
            {
                this.nombrePropiedadAComparar = propiedadAcomparar;
                this.valorRequerido = valorRequerido;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var validationResult = ValidationResult.Success;
                try
                {
                    var informacionPropiedad = validationContext.ObjectType.GetProperty(this.nombrePropiedadAComparar);
                    var valorAvalidar = value as HttpPostedFile;
                    var valorPropiedad = TypeHelper.ConvertObjectToString(informacionPropiedad.GetValue(validationContext.ObjectInstance, null));

                    if (valorRequerido == valorPropiedad && valorAvalidar == null)
                        validationResult = new ValidationResult(ErrorMessageString);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return validationResult;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
                string errorMessage = ErrorMessageString;

                // The value we set here are needed by the jQuery adapter
                ModelClientValidationRule regla = new ModelClientValidationRule();
                regla.ErrorMessage = errorMessage;
                regla.ValidationType = "requeridoarchivosegunvalor"; // This is the name the jQuery adapter will use
                //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
                regla.ValidationParameters.Add("propiedadacomparar", this.nombrePropiedadAComparar);
                regla.ValidationParameters.Add("valorrequerido", this.valorRequerido);
                yield return regla;
            }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class ValidaRango : ValidationAttribute, IClientValidatable
        {
            string nombrePropiedadAComparar;
            bool debeSerMayor;

            public ValidaRango(string propiedadAcomparar, bool debeSerMayor, string errorMessage)
                : base(errorMessage)
            {
                this.nombrePropiedadAComparar = propiedadAcomparar;
                this.debeSerMayor = debeSerMayor;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var validationResult = ValidationResult.Success;
                try
                {
                    var informacionPropiedad = validationContext.ObjectType.GetProperty(this.nombrePropiedadAComparar);
                    var valorAvalidar = value as decimal?;
                    decimal? valorPropiedad = null;

                    if (informacionPropiedad.GetValue(validationContext.ObjectInstance, null) != null)
                        valorPropiedad = decimal.Parse(TypeHelper.ConvertObjectToString(informacionPropiedad.GetValue(validationContext.ObjectInstance, null)));


                    if (debeSerMayor)
                    {
                        if(valorAvalidar != null && (valorPropiedad == null || valorAvalidar < valorPropiedad))
                            validationResult = new ValidationResult(ErrorMessageString);

                    }
                    else
                    {
                        if (valorAvalidar != null && (valorPropiedad == null || valorAvalidar > valorPropiedad))
                            validationResult = new ValidationResult(ErrorMessageString);
                    }
                        
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return validationResult;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
                string errorMessage = ErrorMessageString;

                // The value we set here are needed by the jQuery adapter
                ModelClientValidationRule regla = new ModelClientValidationRule();
                regla.ErrorMessage = errorMessage;
                regla.ValidationType = "validarango"; // This is the name the jQuery adapter will use
                //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
                regla.ValidationParameters.Add("propiedadacomparar", this.nombrePropiedadAComparar);
                regla.ValidationParameters.Add("debesermayor", this.debeSerMayor);
                yield return regla;
            }
        }
    }

    
}
