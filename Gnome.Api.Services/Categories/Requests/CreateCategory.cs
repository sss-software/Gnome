﻿using Gnome.Core.Model;
using MediatR;

namespace Gnome.Api.Services.Categories.Requests
{
    public class CreateCategory : IRequest<Category>
    {
        public int ParentId { get; }
        public string Name { get; }
        public int UserId { get; set; }

        public CreateCategory(int parentId, string name, int userId)
        {
            this.ParentId = parentId;
            this.Name = name;
            this.UserId = userId;
        }
    }
}