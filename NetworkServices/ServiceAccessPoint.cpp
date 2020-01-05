#include "ServiceAccessPoint.h" 
#include <stdlib.h> 
#include <memory> 
#include <string> 
#include "Task.h" 
#include "Service.h" 
#include "ServiceFactory.h" 
#include "TaskStreams.h" 
#include "BufferedStream.h" 
#include "TCPStreamAdapter.h" 
#include "TCPLogHelper.h" 
#include "NetworkExceptions.h" 
#include <Utilities/FsbLogger.h>

namespace Yammer {

  using std::auto_ptr;
  using std::string;

  ServiceAccessPoint::ServiceAccessPoint(int port)
    throw (ServiceAccessPoint::BindFailed)
  {
    // enable socket address reuse, then bind
    if (acceptor_.open(ACE_INET_Addr(port), 1) == -1)  
      throw BindFailed();

	std::cout << " Bound to port " << port << std::endl;
    ACE_Thread::spawn(run, this, THR_BOUND|THR_DETACHED);
  }
#ifdef _MSC_VER
  DWORD ServiceAccessPoint::run(void *This)
  {
    ServiceAccessPoint *that = reinterpret_cast<ServiceAccessPoint*>(This);
    auto_ptr<ServiceAccessPoint> deleter(that);
    ServiceFactory *serviceFactory = ServiceFactorySingleton::instance();
    ACE_SOCK_Acceptor acceptor = that->socket();
    ACE_SOCK_Stream peer;
	FSB_LOG("Waiting for connections");
    for (;;) {
      if (acceptor.accept(peer) == -1) {  // blocks
        FSB_LOG("accept failed");
        exit(0);
      }

      auto_ptr<Stream> tcpStream(new TCPStreamAdapter(peer));
      BufferedStream stream(tcpStream.get());

      string peerInfo = tcpPeerInfo(peer);
      FSB_LOG("A client is connecting from " << peerInfo);

      try {
        auto_ptr<Task> task;
        stream >> task;

        Service *service = serviceFactory->createService(task.get());
        service->spawn(tcpStream.release(), task.release());

      } catch (Task &exception) {  // UnknownTask or UnknownServiceRequest 
        FSB_LOG(exception.name() << ": client=" << peerInfo);
        try { stream << exception; } catch (NetworkError&) {}
        FSB_LOG("Disconnecting from client " << peerInfo);
        stream.close();
      } catch (NetworkError &exception) {
        FSB_LOG(exception.what() << ": client=" << peerInfo);
        FSB_LOG("Disconnecting from client " << peerInfo);
        stream.close();
      }
    }

    return 0;
  }
#else
  void *ServiceAccessPoint::run(void *This)
  {
    ServiceAccessPoint *that = reinterpret_cast<ServiceAccessPoint*>(This);
    auto_ptr<ServiceAccessPoint> deleter(that);
    ServiceFactory *serviceFactory = ServiceFactorySingleton::instance();
    ACE_SOCK_Acceptor acceptor = that->socket();
    ACE_SOCK_Stream peer;

    for (;;) {
      if (acceptor.accept(peer) == -1) {  // blocks
        FSB_LOG("accept failed");
        exit(0);
      }

      auto_ptr<Stream> tcpStream(new TCPStreamAdapter(peer));
      BufferedStream stream(tcpStream.get());

      string peerInfo = tcpPeerInfo(peer);
      FSB_LOG("A client is connecting from " << peerInfo);

      try {
        auto_ptr<Task> task;
        stream >> task;

        Service *service = serviceFactory->createService(task.get());
        service->spawn(tcpStream.release(), task.release());

      } catch (Task &exception) {  // UnknownTask or UnknownServiceRequest 
        FSB_LOG(exception.name() << ": client=" << peerInfo);
        try { stream << exception; } catch (NetworkError&) {}
        FSB_LOG("Disconnecting from client " << peerInfo);
        stream.close();
      } catch (NetworkError &exception) {
        FSB_LOG(exception.what() << ": client=" << peerInfo);
        FSB_LOG("Disconnecting from client " << peerInfo);
        stream.close();
      }
    }

    return 0;
  }
#endif
}