using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace EnKolayCari.Application.ViewMapping
{
    public static class ViewEntityToDtoMapper
    {
        private static readonly IMapper _mapper;

        static ViewEntityToDtoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewEntityToDtoMappingProfile());
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();
        }

    }
}
