using System;
using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace Application.Data
{
    public class DataMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            // Getting Started with the Fluent Mapping API
            // http://documentation.telerik.com/openaccess-orm/developers-guide/code-only-mapping/fluent-mapping-overview

            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            //// Explicit Mapping example
            //MappingConfiguration<Product> productConfiguration = new MappingConfiguration<Product>();
            //productConfiguration.MapType(x => new
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Discontinued = x.Discontinued,
            //    CategoryId = x.CategoryId
            //}).ToTable("Products");
            //productConfiguration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);

            //MappingConfiguration<Category> categoryConfiguration = new MappingConfiguration<Category>();
            //categoryConfiguration.MapType(x => new
            //{
            //    CategoryId = x.Id,
            //    CategoryName = x.Name,
            //}).ToTable("Categories");
            //categoryConfiguration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);

            //configurations.Add(productConfiguration);
            //configurations.Add(categoryConfiguration);

            MappingConfiguration<Product> productConfiguration = new MappingConfiguration<Product>();
            productConfiguration.MapType().ToTable("Products");
            productConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            productConfiguration.HasAssociation(p => p.Category)
                .WithOpposite(c => c.Products)
                .HasConstraint((p, c) => p.CategoryId == c.Id);
            configurations.Add(productConfiguration);

            MappingConfiguration<Category> categoryConfiguration = new MappingConfiguration<Category>();
            categoryConfiguration.MapType().ToTable("Categories");
            categoryConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            configurations.Add(categoryConfiguration);

            return configurations;
        }

        // nzhul: this override is used to allow creation of foreigh keys on schema update
        protected override MetadataContainer CreateModel()
        {
            MetadataContainer model = base.CreateModel();
            model.DefaultMapping.NullForeignKey = true;
            return model;
        }

    }
}
