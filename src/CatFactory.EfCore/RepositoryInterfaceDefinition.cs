﻿using System;
using System.Collections.Generic;
using CatFactory.DotNetCore;
using CatFactory.Mapping;
using CatFactory.OOP;

namespace CatFactory.EfCore
{
    public class RepositoryInterfaceDefinition : CSharpInterfaceDefinition
    {
        public RepositoryInterfaceDefinition(ProjectFeature projectFeature)
        {
            Namespaces.Add("System");
            Namespaces.Add("System.Collections.Generic");

            Name = projectFeature.GetInterfaceRepositoryName();

            foreach (var dbObject in projectFeature.DbObjects)
            {
                Methods.Add(new MethodDefinition(String.Format("IEnumerable<{0}>", dbObject.GetSingularName()), String.Format("Get{0}", dbObject.GetPluralName())));

                Methods.Add(new MethodDefinition(dbObject.GetSingularName(), String.Format("Get{0}", dbObject.GetSingularName()))
                {
                    Parameters = new List<ParameterDefinition>()
                    {
                        new ParameterDefinition(dbObject.GetSingularName(), "entity")
                    }
                });

                if (!projectFeature.IsView(dbObject))
                {
                    Methods.Add(new MethodDefinition("void", String.Format("Add{0}", dbObject.GetSingularName()))
                    {
                        Parameters = new List<ParameterDefinition>()
                        {
                            new ParameterDefinition(dbObject.GetSingularName(), "entity")
                        }
                    });

                    Methods.Add(new MethodDefinition("void", String.Format("Update{0}", dbObject.GetSingularName()))
                    {
                        Parameters = new List<ParameterDefinition>()
                        {
                            new ParameterDefinition(dbObject.GetSingularName(), "changes")
                        }
                    });

                    Methods.Add(new MethodDefinition("void", String.Format("Delete{0}", dbObject.GetSingularName()))
                    {
                        Parameters = new List<ParameterDefinition>()
                        {
                            new ParameterDefinition(dbObject.GetSingularName(), "entity")
                        }
                    });
                }
            }
        }
    }
}
