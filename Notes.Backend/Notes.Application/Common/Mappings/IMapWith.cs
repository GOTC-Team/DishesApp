using AutoMapper;

namespace Notes.Application.Common.Mappings;

public interface IMapWith<TSource>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(TSource), GetType());
}
