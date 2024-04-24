using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ContentLanguageAttribute : Attribute
{
    public ContentLanguageAttribute(string propertyName)
    {
            PropertyName = propertyName;
    }

    public ContentLanguageAttribute()
    {

    }

    public string PropertyName { get; set; }
}