using System;
using System.Collections.Generic;

namespace HansKindberg.Validation
{
	public interface IValidationResult
	{
		#region Properties

		IList<Exception> Exceptions { get; }
		bool IsValid { get; set; }

		#endregion

		#region Methods

		void AddExceptions(IEnumerable<Exception> exceptions);

		#endregion
	}

	public interface IValidationResult<TValidatedObject> : IValidationResult
	{
		#region Properties

		TValidatedObject ValidatedObject { get; set; }

		#endregion
	}
}