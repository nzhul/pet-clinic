using PetClinic.Data.DomainObjects;
using System;
using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace PetClinic.Data
{
    public class DataMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            // Getting Started with the Fluent Mapping API
            // http://documentation.telerik.com/openaccess-orm/developers-guide/code-only-mapping/fluent-mapping-overview

            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

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
    }
}
