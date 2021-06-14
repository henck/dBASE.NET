using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
    /// <summary>
    /// Defines a field within an entity
    /// </summary>
    public class DbfFieldAttribute : Attribute
    {
        /// <summary>
        /// Name of the property in the DBF
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Create an attribute to be able to bind to a DBF field.
        /// </summary>
        /// <param name="name">
        ///     The name of the field. Can be omitted, in that case it takes the name of the property.
        /// </param>
        public DbfFieldAttribute([CallerMemberName] string name = null)
        {
            Name = name;
        }
    }
}
