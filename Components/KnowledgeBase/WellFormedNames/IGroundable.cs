/** 
 * Copyright (C) 2006 GAIPS/INESC-ID 
 *  
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 * 
 * Company: GAIPS/INESC-ID
 * Project: FAtiMA
 * Created: 12/07/2006
 * Ported to C#: 17/06/2015
 **/

using System.Collections.Generic;

namespace KnowledgeBase.WellFormedNames
{
	/// <summary>
	/// Interface that specifies methods applicable to classes that can be grounded,
	/// i.e. have WellFormed Names
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	public interface IGroundable<T> where T : IGroundable<T>
	{
		/// <summary>
		/// Replaces all unbound variables in the object by applying a numeric
		/// identifier to each one. For example, the variable [x] becomes [x4]
		/// if the received ID is 4. 
		/// Attention, this method modifies the original object.
		/// </summary>
		/// <param name="variableID">the identifier to be applied</param>
		T ReplaceUnboundVariables(int variableID);

		/// <summary>
		/// Applies a set of substitutions to the object, grounding it.
		/// Example: Applying the substitution "[X]/John" in the name "Weak([X])" returns "Weak(John)". 
		/// </summary>
		/// @warning: this method modifies the original object.
		/// <see cref="FAtiMA.Core.WellFormedName.Substitution"/>
		/// <param name="bindings">A set of substitutions of the type "[Variable]/value"</param>
		T MakeGround(IEnumerable<Substitution> bindings);

	    /// <summary>
		/// Indicates if the name is grounded (no unbound variables in it's WFN)
		/// Example: Stronger(Luke,John) is grounded while Stronger(John,[X]) is not.
		/// </summary>
		bool IsGrounded { get; }
	}
}
