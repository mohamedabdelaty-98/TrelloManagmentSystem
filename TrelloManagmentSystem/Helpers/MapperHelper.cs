using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TrelloManagmentSystem.Helpers
{
    public static class MapperHelper 
    {
        public static IMapper mapper { get; set; }
        public static IEnumerable<TResult> Map<TResult>(this IQueryable source)
            => source.ProjectTo<TResult>(mapper.ConfigurationProvider);

        public static TResult MapOne<TResult>(this object source)
            =>mapper.Map<TResult>(source);

        public static TDestination MapOne<TSource,TDestination>(this TSource source, TDestination destination)
            => mapper.Map(source, destination);

    }
}
