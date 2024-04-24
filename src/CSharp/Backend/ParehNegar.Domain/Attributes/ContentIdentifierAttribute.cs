using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ContentIdentifierAttribute : Attribute
{
    public ContentIdentifierAttribute(string identifier)
    {
            Identifier = identifier;
        }

    public string Identifier { get; set; }
}