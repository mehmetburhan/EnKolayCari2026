using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace EnKolayCari.Application.ProcedureMapping
{
    public static class ProcedureEntityToDtoMapper
    {
        private static readonly IMapper _mapper;

        static ProcedureEntityToDtoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
               cfg.AddProfile(new ProcedureEntityToDtoMappingProfile());
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();
        }

    }
}
