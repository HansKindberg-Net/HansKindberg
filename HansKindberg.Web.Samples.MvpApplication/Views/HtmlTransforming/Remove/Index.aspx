<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Layout.master" AutoEventWireup="false" CodeBehind="Index.aspx.cs" Inherits="HansKindberg.Web.Samples.MvpApplication.Views.HtmlTransforming.Remove.Index"
%><asp:Content ContentPlaceHolderID="TitleContent" runat="server">Remove html-transforming</asp:Content>
<asp:Content ContentPlaceHolderID="HeadingContent" runat="server">Remove html-transforming</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><p>This is a sample for the HansKindberg.Web.HtmlTransforming namespace.</p>
	<p>In web.config some html-transformers have been removed for this location path.</p>
	<h2>Html-transforming result</h2>
	<div id="html-transforming-result" class="alert alert-info"></div>
	<h2>Active html-transformers</h2>
	<div class="alert alert-info x-scroll">
		<p><strong>On this page the following html-transformers are executed:</strong></p>
		<asp:Repeater id="HtmlTransformerRepeater" DataSource="<%# this.Model.HtmlTransformersSection.HtmlTransformers.Any() ? this.Model.HtmlTransformersSection.HtmlTransformers : null %>" runat="server">
			<HeaderTemplate><ul></HeaderTemplate>
			<ItemTemplate><li><strong>Name</strong>="<%# Eval("Name") %>",&nbsp;<strong>Type</strong>="<%# Eval("Type") %>"</li></ItemTemplate>
			<FooterTemplate></ul></FooterTemplate>
		</asp:Repeater>
	</div>
	<h2>Web.config parts involved in the html-transforming</h2>
	<code class="x-scroll">
		&lt;?xml&nbsp;version="1.0"?&gt;<br />
		&lt;configuration&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;configSections&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;sectionGroup&nbsp;name="hansKindberg.web"&nbsp;type="HansKindberg.Web.Configuration.WebSectionGroup,&nbsp;HansKindberg.Web"&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;section&nbsp;name="htmlTransformers"&nbsp;type="HansKindberg.Web.Configuration.HtmlTransformersSection,&nbsp;HansKindberg.Web"&nbsp;/&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/sectionGroup&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;/configSections&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;location&nbsp;path="."&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;system.web&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;httpModules&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name="HtmlTransformingInitializerModule"&nbsp;type="HansKindberg.Web.HttpModules.HtmlTransformingInitializerModule,&nbsp;HansKindberg.Web"&nbsp;/&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/httpModules&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/system.web&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;system.webServer&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;modules&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name="HtmlTransformingInitializerModule"&nbsp;type="HansKindberg.Web.HttpModules.HtmlTransformingInitializerModule,&nbsp;HansKindberg.Web"&nbsp;/&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/modules&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;validation&nbsp;validateIntegratedModeConfiguration="false"&nbsp;/&gt;&lt;!--&nbsp;We&nbsp;want&nbsp;to&nbsp;be&nbsp;able&nbsp;to&nbsp;run&nbsp;this&nbsp;with&nbsp;both&nbsp;IIS&nbsp;Express&nbsp;and&nbsp;Visual&nbsp;Studio&nbsp;Development&nbsp;Server&nbsp;--&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/system.webServer&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;/location&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;location&nbsp;path="Views/HtmlTransforming"&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;hansKindberg.web&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;htmlTransformers&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name="First"&nbsp;type="HansKindberg.Web.Samples.MvpApplication.Business.Web.HtmlTransforming.FirstHtmlTransformer,&nbsp;HansKindberg.Web.Samples.MvpApplication"&nbsp;/&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name="Second"&nbsp;type="HansKindberg.Web.Samples.MvpApplication.Business.Web.HtmlTransforming.SecondHtmlTransformer,&nbsp;HansKindberg.Web.Samples.MvpApplication"&nbsp;/&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name="Third"&nbsp;type="HansKindberg.Web.Samples.MvpApplication.Business.Web.HtmlTransforming.ThirdHtmlTransformer,&nbsp;HansKindberg.Web.Samples.MvpApplication"&nbsp;/&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/htmlTransformers&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/hansKindberg.web&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;/location&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;location&nbsp;path="Views/HtmlTransforming/Clear"&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;hansKindberg.web&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;htmlTransformers&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;clear&nbsp;/&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/htmlTransformers&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/hansKindberg.web&gt;<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&lt;/location&gt;<br />
		<strong>
			&nbsp;&nbsp;&nbsp;&nbsp;&lt;location&nbsp;path="Views/HtmlTransforming/Remove"&gt;<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;hansKindberg.web&gt;<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;htmlTransformers&gt;<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;remove&nbsp;name="First"&nbsp;/&gt;<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;remove&nbsp;name="Third"&nbsp;/&gt;<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/htmlTransformers&gt;<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/hansKindberg.web&gt;<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&lt;/location&gt;
		</strong><br />
		&lt;/configuration&gt;<br />
	</code>
</asp:Content>