﻿using System;
using System.Web;

namespace Nameless.Spending.Web {
	public class Global : HttpApplication {

		#region Protected Methods

		protected void Application_Start(object sender, EventArgs e) { }
		protected void Session_Start(object sender, EventArgs e) { }
		protected void Application_BeginRequest(object sender, EventArgs e) { }
		protected void Application_AuthenticateRequest(object sender, EventArgs e) { }
		protected void Application_Error(object sender, EventArgs e) { }
		protected void Session_End(object sender, EventArgs e) { }
		protected void Application_End(object sender, EventArgs e) { }

		#endregion
	}
}