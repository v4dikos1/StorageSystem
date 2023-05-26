using AutoMapper;
using System.Reflection;

namespace Application.Common.Mapping;

/// <summary>
/// Automatic configuration of mappings from an assembly
/// </summary>
internal class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
    {
        ApplyMappingsFromAssembly(assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                //Looking for the types that implement the IMapWith<> interface
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            // For each type, check if there is a method named "Mapping"
            var methodInfo = type.GetMethod("Mapping");
            // And if there is such a method, call it
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}