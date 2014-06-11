//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.DirectoryServices;
//using System.Globalization;
//using System.Text;

//namespace HansKindberg.DirectoryServices
//{
//	public class DistinguishedName : IDistinguishedName
//	{
//		#region Fields

//		public const char DefaultNameValueDelimiter = '=';
//		public const char DefaultComponentDelimiter = ',';

//		private const StringComparison _nameComparison = StringComparison.OrdinalIgnoreCase;
//		private const StringComparison _valueComparison = StringComparison.OrdinalIgnoreCase;
//		private const bool _persistNameCaseWhenConvertingToString = false;


//		private readonly IList<KeyValuePair<string, string>> _components = new List<KeyValuePair<string, string>>();

//		#endregion

//		#region Properties

//		protected internal virtual bool PersistNameCaseWhenConvertingToString
//		{
//			get { return _persistNameCaseWhenConvertingToString; }
//		}

//		protected internal virtual StringComparison KeyComparison { get { return _nameComparison; } }



//		protected internal virtual char NameValueDelimiter { get { return DefaultNameValueDelimiter; } }
//		protected internal virtual char ComponentDelimiter{get { return DefaultComponentDelimiter; } }

//		protected internal virtual StringComparison ValueComparison { get { return _valueComparison; } }

//		public virtual IList<KeyValuePair<string, string>> Components
//		{
//			get { return this._components; }
//		}




//		#endregion

//		#region Methods

//		public override bool Equals(object obj)
//		{
//			//return base.Equals(obj);
//		}

//		public override int GetHashCode()
//		{
//			int hashCode = 0;

//			foreach(var keyValuePair in this.Components)
//			{
//				num = (num + this.components[i].Name.ToUpperInvariant().GetHashCode()) + this.components[i].Value.ToUpperInvariant().GetHashCode();
//			}

//			return hashCode;
//		}

//		protected internal virtual int GetHashCode(string value)
//		{
//			if(value == null)
//				throw new ArgumentNull
//		}

//		public bool Equals(IDistinguishedName other)
//		{
//			throw new NotImplementedException();
//		}

//		public override string ToString()
//		{
//			return null;



//			//var stringBuilder = new StringBuilder();

//			//if(this.AuthenticationTypes.HasValue)
//			//	this.AddParameter(stringBuilder, "AuthenticationTypes", this.AuthenticationTypes.Value);

//			//if(!string.IsNullOrEmpty(this.DistinguishedName))
//			//	this.AddParameter(stringBuilder, "DistinguishedName", this.DistinguishedName);

//			//if(!string.IsNullOrEmpty(this.Host))
//			//	this.AddParameter(stringBuilder, "Host", this.Host);

//			//if(!string.IsNullOrEmpty(this.Password))
//			//	this.AddParameter(stringBuilder, "Password", this.Password);

//			//if(this.Port.HasValue)
//			//	this.AddParameter(stringBuilder, "Port", this.Port.Value);

//			//if(this.Scheme.HasValue)
//			//	this.AddParameter(stringBuilder, "Scheme", this.Scheme.Value);

//			//if(!string.IsNullOrEmpty(this.UserName))
//			//	this.AddParameter(stringBuilder, "UserName", this.UserName);

//			//return stringBuilder.ToString();
//		}

//		#endregion


//	}
//}