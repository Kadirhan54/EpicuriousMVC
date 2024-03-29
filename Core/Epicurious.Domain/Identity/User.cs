﻿using Epicurious.Domain.Common;
using Epicurious.Domain.Entities;
using Epicurious.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Epicurious.Domain.Identity
{
    public class User : IdentityUser<Guid>, IEntityBase<Guid>, ICreatedByEntity, IModifiedByEntity
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }
        public Gender Gender { get; set; }

        public UserSetting UserSetting { get; set; }

        public ICollection<Recipe> Recipes {  get; set; }
        public ICollection<LikedRecipe> LikedRecipes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string? ModifiedByUserId { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}
