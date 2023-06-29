using AutoMapper;
using EvaluacionCore.Application.Features.Localidads.Commands.AsignarLocalidadCliente;
using EvaluacionCore.Application.Features.Localidads.Commands.CreateLocalidad;
using EvaluacionCore.Application.Features.Localidads.Commands.UpdateLocalidad;
using EvaluacionCore.Application.Features.Localidads.Dto;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoSubTurno;
using EvaluacionCore.Application.Features.Turnos.Commands.UpdateTurnoColaborador;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Marcaciones;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EvaluacionCore.Application.Common.Mappings;

public class MappingProfile : Profile
{
    //private readonly IConfiguration _config;
    public MappingProfile()
    {
        
        CreateMap<CreateTurnoCommand, Turno>(MemberList.None).ReverseMap();
        CreateMap<CreateTurnoRequest, Turno>(MemberList.None).ReverseMap();
        CreateMap<CreateTurnoColaboradorRequest, TurnoColaborador>(MemberList.None).ReverseMap();
        CreateMap<UpdateTurnoColaboradorRequest, TurnoColaborador>(MemberList.None).ReverseMap();
        CreateMap<Turno, CreateTurnoCommand>().ReverseMap();
        CreateMap<Turno, CreateTurnoSubTurnoRequest>().ReverseMap();
        CreateMap<Localidad, CreateLocalidadRequest>().ReverseMap();
        CreateMap<Localidad, UpdateLocalidadRequest>().ReverseMap();
        CreateMap<Localidad, LocalidadType>().ReverseMap();
        CreateMap<ClaseTurno, ClaseTurnoType>().ReverseMap();
        CreateMap<SubclaseTurno, SubclaseTurnoType>().ReverseMap();
        CreateMap<TipoTurno, TipoTurnoType>().ReverseMap();
        CreateMap<TurnoType, Turno>().ReverseMap();
        CreateMap<TurnoType, TurnoResponseType>().ReverseMap();
        CreateMap<LocalidadColaborador, AsignarLocalidadClienteRequest>().ReverseMap();
        CreateMap<ColaboradorByLocalidadResponseType, Cliente>().ReverseMap();
        CreateMap<ColaboradorByLocalidadResponseType, Localidad>().ReverseMap();
        CreateMap<ColaboradorByLocalidadResponseType, Adjunto>().ReverseMap();
        CreateMap<AccLogMarcacionOffline, CreateCabeceraLogRequest>().ReverseMap();
        CreateMap<DispositivosMarcacionResponse, DispositivoMarcacion>().ReverseMap();
        

        CreateMap<MarcacionOffline, NovedadesMarcacionOfflineResponse>().ReverseMap();

        CreateMap<ColaboradorByLocalidadResponseType, LocalidadColaborador>().ReverseMap()
            .ForMember(des => des.Identificacion,opt => opt.MapFrom(src => src.Colaborador.Identificacion))
            .ForMember(des => des.Empleado, opt => opt.MapFrom(src => (src.Colaborador.Nombres + " " +src.Colaborador.Apellidos)))
            .ForMember(des => des.RutaImagen, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Colaborador.ImagenPerfil.RutaAcceso) ? "" : "https://imagenes.enrolapp.ec/" + src.Colaborador.ImagenPerfil.RutaAcceso  ))
            .ForMember(des => des.NombreUdn, opt => opt.MapFrom(src => src.Localidad.Empresa.RazonSocial))
            .ForMember(des => des.CodigoUdn, opt => opt.MapFrom(src => src.Localidad.Empresa.Codigo))
            .ForMember(des => des.CodigoBiometrico, opt => opt.MapFrom(src => src.Colaborador.CodigoConvivencia));

        //CreateMap<SendEmailVerificacionRequest, NotificacionDto>();
        //ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}