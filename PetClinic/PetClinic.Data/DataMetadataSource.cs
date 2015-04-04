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


            //TODO: Add the Users in the MetaDataSource

            MappingConfiguration<Pet> petConfiguration = new MappingConfiguration<Pet>();
            petConfiguration.MapType().ToTable("Pets");
            petConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            petConfiguration.HasAssociation(p => p.Owner)
                .WithOpposite(c => c.Pets)
                .HasConstraint((p, c) => p.OwnerId == c.Id);
            configurations.Add(petConfiguration);

            MappingConfiguration<Owner> ownerConfiguration = new MappingConfiguration<Owner>();
            ownerConfiguration.MapType().ToTable("Owners");
            ownerConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            configurations.Add(ownerConfiguration);

            MappingConfiguration<User> userConfiguration = new MappingConfiguration<User>();
            userConfiguration.MapType().ToTable("Users");
            userConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            configurations.Add(userConfiguration);

            return configurations;
        }
    }
}
