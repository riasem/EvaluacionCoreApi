
using Xunit;

using System.Runtime.Serialization;
using AutoMapper;
using Utils.Application.Common.Mappings;
using Utils.Domain.Entities;
using Utils.Application.Features.Notificacion.Commands.SendEmailCommand;
using Utils.Application.Features.Notificacion.Commands.ReenviarOtp;

namespace EnrolApp.Application.UnitTests.Common.Mappings
{
    public class MappingProfileXUnitTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingProfileXUnitTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }
        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(SendEmailRequest), typeof(Notificaciones))]
        [InlineData(typeof(ReenviarOtpRequest), typeof(Notificaciones))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(origin);

            _mapper.Map(instance, origin, destination);
        }
    }
}