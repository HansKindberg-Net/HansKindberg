using System;
using System.Web;
using HansKindberg.IO;
using HtmlAgilityPack;

namespace HansKindberg.Web.HtmlTransforming
{
	[CLSCompliant(false)]
	public class DefaultHtmlTransformingInitializer : IHtmlTransformingInitializer
	{
		#region Fields

		private readonly IHtmlDocumentFactory _htmlDocumentFactory;
		private readonly IHtmlInvestigator _htmlInvestigator;
		private readonly IHtmlTransformingContext _htmlTransformingContext;

		#endregion

		#region Constructors

		public DefaultHtmlTransformingInitializer(IHtmlInvestigator htmlInvestigator, IHtmlDocumentFactory htmlDocumentFactory, IHtmlTransformingContext htmlTransformingContext)
		{
			if(htmlInvestigator == null)
				throw new ArgumentNullException("htmlInvestigator");

			if(htmlDocumentFactory == null)
				throw new ArgumentNullException("htmlDocumentFactory");

			if(htmlTransformingContext == null)
				throw new ArgumentNullException("htmlTransformingContext");

			this._htmlDocumentFactory = htmlDocumentFactory;
			this._htmlInvestigator = htmlInvestigator;
			this._htmlTransformingContext = htmlTransformingContext;
		}

		#endregion

		#region Properties

		protected internal virtual IHtmlDocumentFactory HtmlDocumentFactory
		{
			get { return this._htmlDocumentFactory; }
		}

		protected internal virtual IHtmlInvestigator HtmlInvestigator
		{
			get { return this._htmlInvestigator; }
		}

		protected internal virtual IHtmlTransformingContext HtmlTransformingContext
		{
			get { return this._htmlTransformingContext; }
		}

		#endregion

		#region Methods

		public virtual void Initialize(IHttpApplication httpApplication)
		{
			if(httpApplication == null)
				throw new ArgumentNullException("httpApplication");

			httpApplication.PostRequestHandlerExecute += (sender, e) => this.OnPostRequestHandlerExecute((HttpApplicationWrapper) (HttpApplication) sender);
		}

		#endregion

		#region Eventhandlers

		public virtual void OnPostRequestHandlerExecute(IHttpApplication httpApplication)
		{
			if(httpApplication == null)
				throw new ArgumentNullException("httpApplication");

			if(!this.HtmlInvestigator.IsHtmlRequest(httpApplication.Context))
				return;

			TransformableStream transformableStream = new TransformableStream(httpApplication.Response.Filter, httpApplication.Response.ContentEncoding);
			transformableStream.Transform += this.OnTransform;

			httpApplication.Response.Filter = transformableStream;
		}

		protected internal virtual void OnTransform(object sender, StreamTransformingEventArgs streamTransformingEventArgs)
		{
			if(streamTransformingEventArgs == null)
				throw new ArgumentNullException("streamTransformingEventArgs");

			HtmlDocument htmlDocument = this.HtmlDocumentFactory.Create();
			htmlDocument.LoadHtml(streamTransformingEventArgs.Content);
			HtmlNode htmlNode = htmlDocument.DocumentNode;

			foreach(var htmlTransformer in this.HtmlTransformingContext.GetTransformers())
			{
				htmlTransformer.Transform(htmlNode);
			}

			streamTransformingEventArgs.TransformedContent = htmlNode.OuterHtml;
		}

		#endregion
	}
}