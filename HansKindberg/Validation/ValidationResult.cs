﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HansKindberg.Validation
{
	public class ValidationResult : IValidationResult
	{
		#region Fields

		private IList<Exception> _exceptions;

		#endregion

		#region Properties

		public virtual IList<Exception> Exceptions
		{
			get { return this._exceptions ?? (this._exceptions = new List<Exception>()); }
		}

		public virtual bool IsValid
		{
			get { return !this.Exceptions.Any(); }
		}

		#endregion

		#region Methods

		public virtual void AddExceptions(IEnumerable<Exception> exceptions)
		{
			if(exceptions == null)
				throw new ArgumentNullException("exceptions");

			List<Exception> exceptionList = exceptions.ToList();

			if(exceptionList.Contains(null))
				throw new ArgumentException("The exception-collection can not contain null values", "exceptions");

			foreach(Exception exception in exceptionList)
			{
				this.Exceptions.Add(exception);
			}
		}

		#endregion
	}

	public class ValidationResult<TValidatedObject> : ValidationResult, IValidationResult<TValidatedObject>
	{
		#region Properties

		public virtual TValidatedObject ValidatedObject { get; set; }

		#endregion
	}
}