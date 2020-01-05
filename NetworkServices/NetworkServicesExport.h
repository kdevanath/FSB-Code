#ifndef _NETWORK_SERVICES_EXPORT_H_
#define _NETWORK_SERVICES_EXPORT_H_

#include <iostream>

//#define _MYLIB_NOAUTOLIB_

// The following will ensure that we are exporting our C++ classes when 
// building the DLL and importing the classes when build an application 
// using this DLL.

#ifdef NETWORKSERVICES_EXPORTS
#define MYLIB_DLLAPI  __declspec( dllexport )
#define EXPIMP_NS_TEMPLATE 
#else
#define MYLIB_DLLAPI  __declspec( dllimport )
#define EXPIMP_NS_TEMPLATE extern
#endif

#if !defined (_WIN32_WINNT)
# define _WIN32_WINNT 0x0400
#endif

#ifdef _MSC_VER
#pragma warning( disable : 4996 )
#endif 
// The following will ensure that when building an application (or another
// DLL) using this DLL, the appropriate .LIB file will automatically be used
// when linking.

//#ifndef _MYLIB_NOAUTOLIB_
//#ifdef _DEBUG
//#pragma comment(lib, "NetworkServices.lib","Utilities.lib")
//#else
//#pragma comment(lib, "NetworkServices.lib","Utilities.lib")
//#endif
//#endif

#endif