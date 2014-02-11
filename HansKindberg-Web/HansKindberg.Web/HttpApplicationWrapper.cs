using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace HansKindberg.Web
{
	public class HttpApplicationWrapper : HttpApplicationWrapper<HttpApplication>
	{
		#region Constructors

		public HttpApplicationWrapper(HttpApplication httpApplication) : base(httpApplication) {}

		#endregion

		#region Methods

		public static HttpApplicationWrapper FromHttpApplication(HttpApplication httpApplication)
		{
			return httpApplication;
		}

		#endregion

		#region Implicit operator

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The wrapped http-application should be disposed by the .NET framework.")]
		public static implicit operator HttpApplicationWrapper(HttpApplication httpApplication)
		{
			return httpApplication != null ? new HttpApplicationWrapper(httpApplication) : null;
		}

		#endregion
	}

	public abstract class HttpApplicationWrapper<T> : IHttpApplication where T : HttpApplication
	{
		#region Fields

		private readonly T _httpApplication;

		#endregion

		#region Constructors

		protected HttpApplicationWrapper(T httpApplication)
		{
			if(httpApplication == null)
				throw new ArgumentNullException("httpApplication");

			this._httpApplication = httpApplication;
		}

		#endregion

		#region Events

		public virtual event EventHandler AcquireRequestState
		{
			add { this.HttpApplication.AcquireRequestState += value; }
			remove { this.HttpApplication.AcquireRequestState -= value; }
		}

		public virtual event EventHandler AuthenticateRequest
		{
			add { this.HttpApplication.AuthenticateRequest += value; }
			remove { this.HttpApplication.AuthenticateRequest -= value; }
		}

		public virtual event EventHandler AuthorizeRequest
		{
			add { this.HttpApplication.AuthorizeRequest += value; }
			remove { this.HttpApplication.AuthorizeRequest -= value; }
		}

		public virtual event EventHandler BeginRequest
		{
			add { this.HttpApplication.BeginRequest += value; }
			remove { this.HttpApplication.BeginRequest -= value; }
		}

		public virtual event EventHandler Disposed
		{
			add { this.HttpApplication.Disposed += value; }
			remove { this.HttpApplication.Disposed -= value; }
		}

		public virtual event EventHandler EndRequest
		{
			add { this.HttpApplication.EndRequest += value; }
			remove { this.HttpApplication.EndRequest -= value; }
		}

		public virtual event EventHandler Error
		{
			add { this.HttpApplication.Error += value; }
			remove { this.HttpApplication.Error -= value; }
		}

		public virtual event EventHandler LogRequest
		{
			add { this.HttpApplication.LogRequest += value; }
			remove { this.HttpApplication.LogRequest -= value; }
		}

		public virtual event EventHandler MapRequestHandler
		{
			add { this.HttpApplication.MapRequestHandler += value; }
			remove { this.HttpApplication.MapRequestHandler -= value; }
		}

		public virtual event EventHandler PostAcquireRequestState
		{
			add { this.HttpApplication.PostAcquireRequestState += value; }
			remove { this.HttpApplication.PostAcquireRequestState -= value; }
		}

		public virtual event EventHandler PostAuthenticateRequest
		{
			add { this.HttpApplication.PostAuthenticateRequest += value; }
			remove { this.HttpApplication.PostAuthenticateRequest -= value; }
		}

		public virtual event EventHandler PostAuthorizeRequest
		{
			add { this.HttpApplication.PostAuthorizeRequest += value; }
			remove { this.HttpApplication.PostAuthorizeRequest -= value; }
		}

		public virtual event EventHandler PostLogRequest
		{
			add { this.HttpApplication.PostLogRequest += value; }
			remove { this.HttpApplication.PostLogRequest -= value; }
		}

		public virtual event EventHandler PostMapRequestHandler
		{
			add { this.HttpApplication.PostMapRequestHandler += value; }
			remove { this.HttpApplication.PostMapRequestHandler -= value; }
		}

		public virtual event EventHandler PostReleaseRequestState
		{
			add { this.HttpApplication.PostReleaseRequestState += value; }
			remove { this.HttpApplication.PostReleaseRequestState -= value; }
		}

		public virtual event EventHandler PostRequestHandlerExecute
		{
			add { this.HttpApplication.PostRequestHandlerExecute += value; }
			remove { this.HttpApplication.PostRequestHandlerExecute -= value; }
		}

		public virtual event EventHandler PostResolveRequestCache
		{
			add { this.HttpApplication.PostResolveRequestCache += value; }
			remove { this.HttpApplication.PostResolveRequestCache -= value; }
		}

		public virtual event EventHandler PostUpdateRequestCache
		{
			add { this.HttpApplication.PostUpdateRequestCache += value; }
			remove { this.HttpApplication.PostUpdateRequestCache -= value; }
		}

		public virtual event EventHandler PreRequestHandlerExecute
		{
			add { this.HttpApplication.PreRequestHandlerExecute += value; }
			remove { this.HttpApplication.PreRequestHandlerExecute -= value; }
		}

		public virtual event EventHandler PreSendRequestContent
		{
			add { this.HttpApplication.PreSendRequestContent += value; }
			remove { this.HttpApplication.PreSendRequestContent -= value; }
		}

		public virtual event EventHandler PreSendRequestHeaders
		{
			add { this.HttpApplication.PreSendRequestHeaders += value; }
			remove { this.HttpApplication.PreSendRequestHeaders -= value; }
		}

		public virtual event EventHandler ReleaseRequestState
		{
			add { this.HttpApplication.ReleaseRequestState += value; }
			remove { this.HttpApplication.ReleaseRequestState -= value; }
		}

		public virtual event EventHandler ResolveRequestCache
		{
			add { this.HttpApplication.ResolveRequestCache += value; }
			remove { this.HttpApplication.ResolveRequestCache -= value; }
		}

		public virtual event EventHandler UpdateRequestCache
		{
			add { this.HttpApplication.UpdateRequestCache += value; }
			remove { this.HttpApplication.UpdateRequestCache -= value; }
		}

		#endregion

		#region Properties

		public virtual HttpApplicationStateBase Application
		{
			get { return this.HttpApplication.Application != null ? new HttpApplicationStateWrapper(this.HttpApplication.Application) : null; }
		}

		public virtual HttpContextBase Context
		{
			get { return this.HttpApplication.Context != null ? new HttpContextWrapper(this.HttpApplication.Context) : null; }
		}

		protected internal virtual T HttpApplication
		{
			get { return this._httpApplication; }
		}

		public virtual bool IsReusable
		{
			get { return ((IHttpHandler) this.HttpApplication).IsReusable; }
		}

		public virtual IEnumerable<IHttpModule> Modules
		{
			get { return this.HttpApplication.Modules.Cast<IHttpModule>(); }
		}

		public virtual HttpRequestBase Request
		{
			get
			{
				// ReSharper disable ConditionIsAlwaysTrueOrFalse
				return this.HttpApplication.Request != null ? new HttpRequestWrapper(this.HttpApplication.Request) : null;
				// ReSharper restore ConditionIsAlwaysTrueOrFalse
			}
		}

		public virtual HttpResponseBase Response
		{
			get
			{
				// ReSharper disable ConditionIsAlwaysTrueOrFalse
				return this.HttpApplication.Response != null ? new HttpResponseWrapper(this.HttpApplication.Response) : null;
				// ReSharper restore ConditionIsAlwaysTrueOrFalse
			}
		}

		public virtual HttpServerUtilityBase Server
		{
			get
			{
				// ReSharper disable ConditionIsAlwaysTrueOrFalse
				return this.HttpApplication.Server != null ? new HttpServerUtilityWrapper(this.HttpApplication.Server) : null;
				// ReSharper restore ConditionIsAlwaysTrueOrFalse
			}
		}

		public virtual HttpSessionStateBase Session
		{
			get
			{
				// ReSharper disable ConditionIsAlwaysTrueOrFalse
				return this.HttpApplication.Session != null ? new HttpSessionStateWrapper(this.HttpApplication.Session) : null;
				// ReSharper restore ConditionIsAlwaysTrueOrFalse
			}
		}

		public virtual ISite Site
		{
			get { return this.HttpApplication.Site; }
			set { this.HttpApplication.Site = value; }
		}

		public virtual IPrincipal User
		{
			get { return this.HttpApplication.User; }
		}

		#endregion

		#region Methods

		public virtual IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			return ((IHttpAsyncHandler) this.HttpApplication).BeginProcessRequest(context, cb, extraData);
		}

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			this.HttpApplication.Dispose();
		}

		public virtual void EndProcessRequest(IAsyncResult result)
		{
			((IHttpAsyncHandler) this.HttpApplication).EndProcessRequest(result);
		}

		public virtual void ProcessRequest(HttpContext context)
		{
			((IHttpHandler) this.HttpApplication).ProcessRequest(context);
		}

		#endregion
	}
}