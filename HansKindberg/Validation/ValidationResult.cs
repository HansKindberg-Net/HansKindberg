using System;
using System.Collections.Generic;
using System.Linq;

namespace HansKindberg.Validation
{
	public class ValidationResult : IValidationResult
	{
		#region Fields

		private IList<Exception> _exceptions;
		private bool _isValid;

		#endregion

		#region Constructors

		public ValidationResult() : this(true, new List<Exception>()) {}

		public ValidationResult(IValidationResult validationResultToCopy)
		{
			if(validationResultToCopy == null)
				throw new ArgumentNullException("validationResultToCopy");

			this._exceptions = validationResultToCopy.Exceptions;
			this._isValid = validationResultToCopy.IsValid;
		}

		public ValidationResult(bool isValid, IList<Exception> exceptions)
		{
			this._exceptions = exceptions;
			this._isValid = isValid;
		}

		#endregion

		#region Properties

		public virtual IList<Exception> Exceptions
		{
			get { return this._exceptions ?? (this._exceptions = new List<Exception>()); }
		}

		public virtual bool IsValid
		{
			get { return this._isValid; }
			set { this._isValid = value; }
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
		#region Fields

		private TValidatedObject _validatedObject;

		#endregion

		#region Constructors

		public ValidationResult() {}
		public ValidationResult(IValidationResult validationResultToCopy) : base(validationResultToCopy) {}
		public ValidationResult(bool isValid, IList<Exception> exceptions) : base(isValid, exceptions) {}

		public ValidationResult(bool isValid, IList<Exception> exceptions, TValidatedObject validatedObject) : base(isValid, exceptions)
		{
			this._validatedObject = validatedObject;
		}

		#endregion

		#region Properties

		public virtual TValidatedObject ValidatedObject
		{
			get { return this._validatedObject; }
			set { this._validatedObject = value; }
		}

		#endregion
	}
}