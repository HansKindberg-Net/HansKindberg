using System;
using System.Globalization;
using System.Text;

namespace HansKindberg.DirectoryServices.Connections
{
	public abstract class GeneralDirectoryConnection
	{
		#region Fields

		public const char DefaultNameValueDelimiter = '=';
		public const char DefaultParameterDelimiter = ';';
		private readonly DirectoryAuthentication _authentication = new DirectoryAuthentication();

		#endregion

		#region Properties

		public virtual DirectoryAuthentication Authentication
		{
			get { return this._authentication; }
		}

		protected internal virtual char NameValueDelimiter
		{
			get { return DefaultNameValueDelimiter; }
		}

		protected internal virtual char ParameterDelimiter
		{
			get { return DefaultParameterDelimiter; }
		}

		#endregion

		#region Methods

		protected internal virtual void AddParameter(StringBuilder value, string parameterName, object parameterValue)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(string.IsNullOrEmpty(parameterName))
				throw new ArgumentException("The parameter-name can not be empty.", "parameterName");

			if(string.IsNullOrEmpty(parameterName.Trim()))
				throw new ArgumentException("The parameter-name can not only contain white-spaces.", "parameterName");

			var parameterDelimiter = !string.IsNullOrEmpty(value.ToString().Trim()) ? this.ParameterDelimiter.ToString(CultureInfo.InvariantCulture) : string.Empty;

			value.Append(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new[] {parameterDelimiter, parameterName, this.NameValueDelimiter, parameterValue ?? string.Empty}));
		}

		#endregion
	}
}