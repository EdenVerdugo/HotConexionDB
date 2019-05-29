using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotConexionDB
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ConexionColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public ConexionColumnAttribute(string name)
        {
            this.Name = name;
        }
    }
}
