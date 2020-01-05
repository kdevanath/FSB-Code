#ifndef _YAMMER_SERVICE_ACCESS_POINT_H_
#define _YAMMER_SERVICE_ACCESS_POINT_H_

#ifdef _MSC_VER
#include "NetworkservicesExport.h"
#endif

#include <ace/SOCK_Acceptor.h> 

namespace Yammer {

  class MYLIB_DLLAPI ServiceAccessPoint {
  public: 
    class BindFailed {};

    ServiceAccessPoint(int port) throw (BindFailed);
#ifdef _MSC_VER
	static DWORD run(void* This);
#else
    static void *run(void *This);
#endif

    ACE_SOCK_Acceptor socket() { return acceptor_; }

  private: 
    ACE_SOCK_Acceptor acceptor_;
  }; 

}

#endif