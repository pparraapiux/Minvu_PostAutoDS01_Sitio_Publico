﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Minvu.SectoresMedios.IData.ORM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Workflow_MigraEntities : DbContext
    {
        public Workflow_MigraEntities()
            : base("name=Workflow_MigraEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<estado_evento> estado_evento { get; set; }
        public virtual DbSet<evento> evento { get; set; }
        public virtual DbSet<instancia_objeto_workflow> instancia_objeto_workflow { get; set; }
    
        public virtual int pa_Evento_Gestor_VerificarInstancia(Nullable<int> id_tipo_objeto, Nullable<long> id_objeto, ObjectParameter resultado, ObjectParameter id_instancia_objeto, ObjectParameter id_estado)
        {
            var id_tipo_objetoParameter = id_tipo_objeto.HasValue ?
                new ObjectParameter("id_tipo_objeto", id_tipo_objeto) :
                new ObjectParameter("id_tipo_objeto", typeof(int));
    
            var id_objetoParameter = id_objeto.HasValue ?
                new ObjectParameter("id_objeto", id_objeto) :
                new ObjectParameter("id_objeto", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_Evento_Gestor_VerificarInstancia", id_tipo_objetoParameter, id_objetoParameter, resultado, id_instancia_objeto, id_estado);
        }
    
        public virtual ObjectResult<pa_Malla_Estados_Eventos_Gestor_ObtenerDetalleTipoEvento_Result> pa_Malla_Estados_Eventos_Gestor_ObtenerDetalleTipoEvento(Nullable<int> id_tipo_evento)
        {
            var id_tipo_eventoParameter = id_tipo_evento.HasValue ?
                new ObjectParameter("id_tipo_evento", id_tipo_evento) :
                new ObjectParameter("id_tipo_evento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_Malla_Estados_Eventos_Gestor_ObtenerDetalleTipoEvento_Result>("pa_Malla_Estados_Eventos_Gestor_ObtenerDetalleTipoEvento", id_tipo_eventoParameter);
        }
    
        public virtual ObjectResult<pa_Workflow_Gestor_CambiarEstadoObjeto_Result> pa_Workflow_Gestor_CambiarEstadoObjeto(Nullable<int> id_instancia_objeto, Nullable<int> id_estado)
        {
            var id_instancia_objetoParameter = id_instancia_objeto.HasValue ?
                new ObjectParameter("id_instancia_objeto", id_instancia_objeto) :
                new ObjectParameter("id_instancia_objeto", typeof(int));
    
            var id_estadoParameter = id_estado.HasValue ?
                new ObjectParameter("id_estado", id_estado) :
                new ObjectParameter("id_estado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_Workflow_Gestor_CambiarEstadoObjeto_Result>("pa_Workflow_Gestor_CambiarEstadoObjeto", id_instancia_objetoParameter, id_estadoParameter);
        }
    
        public virtual int pa_Workflow_Gestor_Obtener_id_tipo_evento(string codigo_tipo_evento, ObjectParameter id_tipo_evento)
        {
            var codigo_tipo_eventoParameter = codigo_tipo_evento != null ?
                new ObjectParameter("codigo_tipo_evento", codigo_tipo_evento) :
                new ObjectParameter("codigo_tipo_evento", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_Workflow_Gestor_Obtener_id_tipo_evento", codigo_tipo_eventoParameter, id_tipo_evento);
        }
    
        public virtual int pa_Workflow_Gestor_Obtener_id_tipo_objeto(string codigo_aplicacion, string codigo_tipo_objeto, ObjectParameter id_tipo_objeto)
        {
            var codigo_aplicacionParameter = codigo_aplicacion != null ?
                new ObjectParameter("codigo_aplicacion", codigo_aplicacion) :
                new ObjectParameter("codigo_aplicacion", typeof(string));
    
            var codigo_tipo_objetoParameter = codigo_tipo_objeto != null ?
                new ObjectParameter("codigo_tipo_objeto", codigo_tipo_objeto) :
                new ObjectParameter("codigo_tipo_objeto", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_Workflow_Gestor_Obtener_id_tipo_objeto", codigo_aplicacionParameter, codigo_tipo_objetoParameter, id_tipo_objeto);
        }
    
        public virtual int pa_Workflow_Gestor_RegistrarEvento(Nullable<int> id_instancia_objeto, Nullable<int> id_tipo_evento, Nullable<long> id_operacion, Nullable<int> id_estado, Nullable<System.DateTime> fecha_evento, string comentario_evento, string id_usuario, string codigo_grupo, ObjectParameter id_evento)
        {
            var id_instancia_objetoParameter = id_instancia_objeto.HasValue ?
                new ObjectParameter("id_instancia_objeto", id_instancia_objeto) :
                new ObjectParameter("id_instancia_objeto", typeof(int));
    
            var id_tipo_eventoParameter = id_tipo_evento.HasValue ?
                new ObjectParameter("id_tipo_evento", id_tipo_evento) :
                new ObjectParameter("id_tipo_evento", typeof(int));
    
            var id_operacionParameter = id_operacion.HasValue ?
                new ObjectParameter("id_operacion", id_operacion) :
                new ObjectParameter("id_operacion", typeof(long));
    
            var id_estadoParameter = id_estado.HasValue ?
                new ObjectParameter("id_estado", id_estado) :
                new ObjectParameter("id_estado", typeof(int));
    
            var fecha_eventoParameter = fecha_evento.HasValue ?
                new ObjectParameter("fecha_evento", fecha_evento) :
                new ObjectParameter("fecha_evento", typeof(System.DateTime));
    
            var comentario_eventoParameter = comentario_evento != null ?
                new ObjectParameter("comentario_evento", comentario_evento) :
                new ObjectParameter("comentario_evento", typeof(string));
    
            var id_usuarioParameter = id_usuario != null ?
                new ObjectParameter("id_usuario", id_usuario) :
                new ObjectParameter("id_usuario", typeof(string));
    
            var codigo_grupoParameter = codigo_grupo != null ?
                new ObjectParameter("codigo_grupo", codigo_grupo) :
                new ObjectParameter("codigo_grupo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_Workflow_Gestor_RegistrarEvento", id_instancia_objetoParameter, id_tipo_eventoParameter, id_operacionParameter, id_estadoParameter, fecha_eventoParameter, comentario_eventoParameter, id_usuarioParameter, codigo_grupoParameter, id_evento);
        }
    
        public virtual int pa_Workflow_Gestor_RegistrarInstancia(string codigo_aplicacion, string codigo_tipo_objeto, Nullable<long> id_objeto, Nullable<int> id_objeto_padre, ObjectParameter id_tipo_evento, ObjectParameter id_tipo_objeto)
        {
            var codigo_aplicacionParameter = codigo_aplicacion != null ?
                new ObjectParameter("codigo_aplicacion", codigo_aplicacion) :
                new ObjectParameter("codigo_aplicacion", typeof(string));
    
            var codigo_tipo_objetoParameter = codigo_tipo_objeto != null ?
                new ObjectParameter("codigo_tipo_objeto", codigo_tipo_objeto) :
                new ObjectParameter("codigo_tipo_objeto", typeof(string));
    
            var id_objetoParameter = id_objeto.HasValue ?
                new ObjectParameter("id_objeto", id_objeto) :
                new ObjectParameter("id_objeto", typeof(long));
    
            var id_objeto_padreParameter = id_objeto_padre.HasValue ?
                new ObjectParameter("id_objeto_padre", id_objeto_padre) :
                new ObjectParameter("id_objeto_padre", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_Workflow_Gestor_RegistrarInstancia", codigo_aplicacionParameter, codigo_tipo_objetoParameter, id_objetoParameter, id_objeto_padreParameter, id_tipo_evento, id_tipo_objeto);
        }
    }
}
