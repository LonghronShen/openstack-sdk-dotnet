// /* ============================================================================
// Copyright 2014 Hewlett Packard
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ============================================================================ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenStack.Common
{
    /// <summary>
    /// Static class for extending reflection classes.
    /// </summary>
    internal static class ReflectionExtentions
    {
        /// <summary>
        /// Gets the assembly that contains the extended type.
        /// </summary>
        /// <param name="input">The given Type</param>
        /// <returns>The assembly that contains the given type.</returns>
        public static Assembly GetAssembly(this Type input)
        {
#if NETFX_40 || NETSTANDARD2_0
            return input.Assembly;
#else
            return input.GetTypeInfo().Assembly;
#endif
        }

        /// <summary>
        /// Determines if the given type is an interface.
        /// </summary>
        /// <param name="input">The given type.</param>
        /// <returns>A value indicating if the given type is an interface.</returns>
        public static bool IsInterface(this Type input)
        {
#if PROFILE_111 || NETSTANDARD1_3 || NETSTANDARD1_6 || NETSTANDARD2_0
            return input.GetTypeInfo().IsInterface;
#else
            return input.IsInterface;
#endif
        }

        /// <summary>
        /// Gets a list of types that are defined in the given assembly.
        /// </summary>
        /// <param name="input">The given assembly.</param>
        /// <returns>A list of types defined in the given assembly.</returns>
        public static IEnumerable<Type> GetDefinedTypes(this Assembly input)
        {
#if NETSTANDARD1_6 || NETSTANDARD2_0
            return input.GetTypes().ToList();
#elif NETFX_40
            return input.GetExportedTypes();
#else
            return input.ExportedTypes.ToList();
#endif
        }

        /// <summary>
        /// Gets a list of defined constructors for the given type.
        /// </summary>
        /// <param name="input">The given type.</param>
        /// <returns>A list of constructors for the given type.</returns>
        public static IEnumerable<ConstructorInfo> GetDefinedConstructors(this Type input)
        {
#if NETFX_40 || NETSTANDARD2_0
            return input.GetConstructors();
#else
            return input.GetTypeInfo().DeclaredConstructors;
#endif
        }

#if NETSTANDARD1_3 || NETSTANDARD1_6 || PROFILE_111
        /// <summary>Determines whether an instance of a specified type can be assigned to an instance of the current type.</summary>
        /// <param name="input"></param>
        /// <param name="c">The type to compare with the current type. </param>
        /// <returns>
        /// <see langword="true" /> if any of the following conditions is true:
        ///     <paramref name="c" /> and the current instance represent the same type.
        ///     <paramref name="c" /> is derived either directly or indirectly from the current instance. <paramref name="c" /> is derived directly from the current instance if it inherits from the current instance; <paramref name="c" /> is derived indirectly from the current instance if it inherits from a succession of one or more classes that inherit from the current instance.  The current instance is an interface that <paramref name="c" /> implements.
        ///     <paramref name="c" /> is a generic type parameter, and the current instance represents one of the constraints of <paramref name="c" />. In the following example, the current instance is a <see cref="T:System.Type" /> object that represents the <see cref="T:System.IO.Stream" /> class. GenericWithConstraint is a generic type whose generic type parameter must be of type    <see cref="T:System.IO.Stream" />. Passing its generic type parameter to the <see cref="M:System.Type.IsAssignableFrom(System.Type)" /> indicates that  an instance of the generic type parameter can be assigned to an <see cref="T:System.IO.Stream" /> object. System.Type.IsAssignableFrom#2
        /// 
        ///     <paramref name="c" /> represents a value type, and the current instance represents Nullable&lt;c&gt; (Nullable(Of c) in Visual Basic).
        /// <see langword="false" /> if none of these conditions are true, or if <paramref name="c" /> is <see langword="null" />. </returns>
        public static bool IsAssignableFrom(this Type input, Type c)
        {
            return input.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }
#endif
    }
}
