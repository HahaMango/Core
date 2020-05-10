using AutoMapper;
using Mango.Core.AutoMapper.Config;
using System;
using System.Security.Cryptography;

namespace Mango.Core.AutoMapper
{
    /// <summary>
    /// 映射执行类
    /// </summary>
    public class MangoMapper : IMapper
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object Sync = new object();

        private static IConfigurationProvider _mapperConfig;

        private static Profile _profile;

        public MangoMapper()
        {
            _profile = new MangoMapperConfig();
            _mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(_profile));
        }

        /// <summary>
        /// 对给定源和目标对象映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination) => MapTo(source, destination);

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TDestination MapTo<TDestination>(object source) where TDestination : new() => MapTo(source, new TDestination());

        /// <summary>
        /// 对任意源对象和目标对象执行映射
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static TDestination MapTo<TDestination>(object source, TDestination destination)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if(destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            var sourceType = source.GetType();
            var destinationType = destination.GetType();
            var typeMap = GetTypeMap(sourceType, destinationType);
            if(typeMap == null)
            {
                InitMap(sourceType, destinationType);
            }
            var mapper = new Mapper(_mapperConfig);
            return mapper.Map(source, destination);
        }

        /// <summary>
        /// 创建映射配置
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static void InitMap(Type source,Type destination)
        {
            _profile.CreateMap(source,destination);
            _mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(_profile));
        }

        /// <summary>
        /// 获取对应源对象和目标对象的映射TypeMap
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static TypeMap GetTypeMap(Type source,Type destination)
        {
            return _mapperConfig.FindTypeMapFor(source, destination);
        }
    }
}
