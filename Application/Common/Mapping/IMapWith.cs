using AutoMapper;

namespace Application.Common.Mapping;

/// <summary>
/// The interface that all models that need to be mapped into other models must implement
/// </summary>
/// <typeparam name="T">Type of the model</typeparam>
public interface IMapWith<T>
{
    /// <summary>
    /// Mapping one model to another
    /// </summary>
    /// <param name="profile"></param>
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}