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
            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            MappingConfiguration<Pet> petConfiguration = new MappingConfiguration<Pet>();
            petConfiguration.MapType().ToTable("Pets");
            petConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            petConfiguration.HasProperty(p => p.Name).IsUnicode();
            petConfiguration.HasProperty(p => p.Breed).IsUnicode();
            petConfiguration.HasAssociation(p => p.Owner)
                .WithOpposite(c => c.Pets)
                .HasConstraint((p, c) => p.OwnerId == c.Id);
            configurations.Add(petConfiguration);

            MappingConfiguration<Cat> catConfiguration = new MappingConfiguration<Cat>();
            catConfiguration.MapType(x => new
            {
                NumberOfHoursSpentSleeping = x.NumberOfHoursSpentSleeping,
                FavouriteFood = x.FavouriteFood
            }).Inheritance(Telerik.OpenAccess.InheritanceStrategy.Vertical).ToTable("Cats");
            catConfiguration.HasProperty(p => p.FavouriteFood).IsUnicode();
            configurations.Add(catConfiguration);

            MappingConfiguration<Dog> dogConfiguration = new MappingConfiguration<Dog>();
            dogConfiguration.MapType(x => new
            {
                FavouriteFood = x.FavouriteFood,
                FavouriteGame = x.FavouriteGame,
                isAgresive = x.IsAggressiveTowardsOtherPeople
            }).Inheritance(Telerik.OpenAccess.InheritanceStrategy.Vertical).ToTable("Dogs");
            dogConfiguration.HasProperty(p => p.Name).IsUnicode();
            dogConfiguration.HasProperty(p => p.Breed).IsUnicode();
            dogConfiguration.HasProperty(p => p.FavouriteFood).IsUnicode();
            dogConfiguration.HasProperty(p => p.FavouriteGame).IsUnicode();
            configurations.Add(dogConfiguration);

            MappingConfiguration<Bird> birdConfiguration = new MappingConfiguration<Bird>();
            birdConfiguration.MapType(x => new
            {
                PertnerId = x.PartnerId,
                Partner = x.Partner
            }).Inheritance(Telerik.OpenAccess.InheritanceStrategy.Vertical).ToTable("Birds");

            birdConfiguration.HasAssociation(p => p.Partner).ToColumn("PartnerId");

            configurations.Add(birdConfiguration);

            MappingConfiguration<Owner> ownerConfiguration = new MappingConfiguration<Owner>();
            ownerConfiguration.MapType().ToTable("Owners");
            ownerConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            ownerConfiguration.HasProperty(p => p.Name).IsUnicode();
            configurations.Add(ownerConfiguration);

            MappingConfiguration<User> userConfiguration = new MappingConfiguration<User>();
            userConfiguration.MapType().ToTable("Users");
            userConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            configurations.Add(userConfiguration);

            MappingConfiguration<Examination> examinationConfiguration = new MappingConfiguration<Examination>();
            examinationConfiguration.MapType().ToTable("Examinations");
            examinationConfiguration.HasProperty(p => p.Id).IsIdentity(KeyGenerator.Autoinc);
            examinationConfiguration.HasProperty(p => p.Diagnosis).IsUnicode();
            examinationConfiguration.HasAssociation(p => p.ExaminedPet)
                .WithOpposite(c => c.Examinations)
                .HasConstraint((p, c) => p.ExaminedPetId == c.Id);
            configurations.Add(examinationConfiguration);

            return configurations;
        }
    }
}
