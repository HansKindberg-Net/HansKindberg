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
/*
	The third position in the version (AssemblyVersion) number is used for the status of the release:
	0 for alpha (status)
	1 for beta (status)
	2 for release candidate
	3 for (final) release

	For instance:
	1.2.0.1 instead of 1.2-a1
	1.2.1.2 instead of 1.2-b2 (beta with some bug fixes)
	1.2.2.3 instead of 1.2-rc3 (release candidate)
	1.2.3.0 instead of 1.2-r (commercial distribution)
	1.2.3.5 instead of 1.2-r5 (commercial distribution with many bug fixes)

	http://en.wikipedia.org/wiki/Software_versioning#Designating_development_stage

	The AssemblyFileVersion uses the format: major.minor.year.date
*/

[assembly: AssemblyFileVersion("1.0.2012.0501")]
//[assembly: AssemblyInformationalVersion("1.0")]
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]