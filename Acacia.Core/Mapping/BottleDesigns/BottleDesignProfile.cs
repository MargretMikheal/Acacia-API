using AutoMapper;

namespace Acacia.Core.Mapping.BottleDesigns;

public partial class BottleDesignProfile : Profile
{
    public BottleDesignProfile()
    {
        CreateBottleDesignCommandMapping();
        BottleDesignResponseMapping();
    }
}
