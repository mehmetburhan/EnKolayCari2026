using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace EnKolayCari2026.Application.FunctionMapping
{
    public static class FunctionEntityToDtoMapper
    {
        private static readonly IMapper _mapper;

        static FunctionEntityToDtoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
               cfg.AddProfile(new FunctionEntityToDtoMappingProfile());
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();
        }

    }
}
