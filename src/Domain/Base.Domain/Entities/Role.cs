using Base.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities;

public class Role : IdentityRole
{
    public override string? Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    protected Role() { }

    public Role(string name)
    {
        Name = name;
    }
}