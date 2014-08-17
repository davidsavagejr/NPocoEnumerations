using System;
using System.Reflection;
using Domain.Models;
using Headspring;
using NPoco;
using NPoco.FluentMappings;

namespace Domain
{
    public static class DatabaseFactory
    {
        public const string ConnectionStringName = "database";

        private static readonly Lazy<NPoco.DatabaseFactory> Factory = new Lazy<NPoco.DatabaseFactory>(InitializeFactory);

        private static NPoco.DatabaseFactory InitializeFactory()
        {
            var conventions = FluentMappingConfiguration.Scan(scanner =>
            {
                scanner.Assembly(typeof(BlogPost).Assembly);
                scanner.IncludeTypes(x => x.Namespace.StartsWith(typeof(BlogPost).Namespace));
                scanner.Columns.IgnoreWhere(y =>
                {
                    // Don't ignore HStore
                    if (y.GetMemberInfoType().IsEnumeration())
                        return false;

                    if (!y.GetMemberInfoType().IsValueType && !(y.GetMemberInfoType() == typeof(string)))
                        return !(y.GetMemberInfoType() == typeof(byte[]));

                    if (!y.IsField() && ((PropertyInfo)y).GetSetMethodOnDeclaringType() == null)
                        return false;

                    return false;
                });
                //scanner.OverrideMappingsWith(new OverrideMappings());
            });

            return NPoco.DatabaseFactory.Config(x =>
                {
                    x.WithFluentConfig(conventions);
                    x.UsingDatabase(() => new Database(ConnectionStringName));
                    x.WithMapper(new EnumerationMapper());
                });
        }

        public static Database GetDatabase()
        {
            return Factory.Value.GetDatabase();
        }
    }
}