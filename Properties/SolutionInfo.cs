using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("Hans Kindberg - open source")]
[assembly: AssemblyConfiguration(
#if DEBUG
	"Debug"
#else
	"Release"
#endif
)]
[assembly: AssemblyInformationalVersion("1.0")]
[assembly: AssemblyVersion("1.0.*")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]