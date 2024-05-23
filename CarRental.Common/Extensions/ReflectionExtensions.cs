using Microsoft.VisualBasic;
using System.Reflection;

namespace CarRental.Common.Extensions
{
    public static class ReflectionExtensions
    {
        // Returns all non-public instance variables in a type (class/struct)
        // or an empty array
        public static FieldInfo[] GetVaraibles(this Type type)
            => type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        // Get the first collection matching the type T that is instantiated
        // through a construcor or at initialization. Throw an exception if
        // no collection of type T exists.
        public static FieldInfo? FindCollection<T>(this FieldInfo[] fields) where T : class
            => fields.FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
            ?? throw new InvalidOperationException("Unsupported type");

        // Get the collection data or throw an exception the collection is null.
        public static object? GetData(this FieldInfo? field, object container)
            => field?.GetValue(container) ?? throw new InvalidDataException();

        public static IQueryable<T> ToQueryable<T>(this object? data) where T : class
            => data is not null && data is List<T>  // is data a List<T>
            ? ((List<T>)data).AsQueryable()         // convert to queryable data
            : throw new InvalidDataException();     // thgrow exception if fail

        // Filter the result and return it from the collection
        public static List<T> Filter<T>(this IQueryable<T> collection, Func<T, bool>? lambda) where T : class
            => lambda is null ? collection.ToList() : collection.Where(lambda).ToList();

        public static T? FilterSingle<T>(this IQueryable<T> collection, Func<T, bool>? lambda) where T : class
        => lambda is null ? default : collection.Single(lambda);
    }
}
