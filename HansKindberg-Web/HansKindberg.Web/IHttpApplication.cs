using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web;

namespace HansKindberg.Web
{
	public interface IHttpApplication : IHttpAsyncHandler, IComponent
	{
		#region Events

		event EventHandler AcquireRequestState;
		event EventHandler AuthenticateRequest;
		event EventHandler AuthorizeRequest;
		event EventHandler BeginRequest;
		event EventHandler EndRequest;

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
		event EventHandler Error;

		event EventHandler LogRequest;
		event EventHandler MapRequestHandler;
		event EventHandler PostAcquireRequestState;
		event EventHandler PostAuthenticateRequest;
		event EventHandler PostAuthorizeRequest;
		event EventHandler PostLogRequest;
		event EventHandler PostMapRequestHandler;
		event EventHandler PostReleaseRequestState;
		event EventHandler PostRequestHandlerExecute;
		event EventHandler PostResolveRequestCache;
		event EventHandler PostUpdateRequestCache;
		event EventHandler PreRequestHandlerExecute;
		event EventHandler PreSendRequestContent;
		event EventHandler PreSendRequestHeaders;
		event EventHandler ReleaseRequestState;
		event EventHandler ResolveRequestCache;
		event EventHandler UpdateRequestCache;

		#endregion

		#region Properties

		HttpApplicationStateBase Application { get; }
		HttpContextBase Context { get; }
		IEnumerable<IHttpModule> Modules { get; }
		HttpRequestBase Request { get; }
		HttpResponseBase Response { get; }
		HttpServerUtilityBase Server { get; }
		HttpSessionStateBase Session { get; }
		IPrincipal User { get; }

		#endregion
	}
}