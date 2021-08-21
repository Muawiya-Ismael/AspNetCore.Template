using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using MvcTemplate.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTemplate.Components.Mvc
{
    public class DisplayMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context.Key.MetadataKind == ModelMetadataKind.Property && context.Key.ContainerType is Type view)
            {
                if (Resource.ForProperty(view, context.Key.Name!) is String { Length: > 0 } title)
                    context.DisplayMetadata.DisplayName = () => title;
            }
            else if (context.Key.MetadataKind == ModelMetadataKind.Type)
            {
                Type type = Nullable.GetUnderlyingType(context.Key.ModelType) ?? context.Key.ModelType;

                if (type.IsEnum)
                {
                    IDictionary<String, String?> titles = Resource.ForEnum(type.Name, "Titles");
                    IDictionary<String, String?> groups = Resource.ForEnum(type.Name, "Groups");

                    context.DisplayMetadata.EnumGroupedDisplayNamesAndValues = Enum
                        .GetNames(type)
                        .Select(name =>
                        {
                            String group = groups.ContainsKey(name) ? groups[name] ?? "" : "";
                            String title = titles.ContainsKey(name) ? titles[name] ?? name : name;
                            String value = (type.GetField(name)!.GetValue(null) as Enum)!.ToString("d");

                            return new KeyValuePair<EnumGroupAndName, String>(new EnumGroupAndName(group, title), value);
                        })
                        .ToArray();
                }
            }
        }
    }
}
