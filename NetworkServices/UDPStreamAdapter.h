#ifndef _YAMMER_UDP_STREAM_ADAPTER_H_
#define _YAMMER_UDP_STREAM_ADAPTER_H_

#include "Stream.h" 
#include <ace/SOCK_Dgram.h> 
#include <ace/INET_Addr.h> 


namespace Yammer {

  // This class adapts an ACE UDP socket to the Stream interface.

  class MYLIB_DLLAPI UDPStreamAdapter : public Stream {  // Adapter pattern
  public:
    UDPStreamAdapter();
    UDPStreamAdapter(const ACE_SOCK_Dgram &socket, const ACE_INET_Addr &addr,
		u_char ttl=1,u_char loopback = 1);
	
    int read(void *buffer, size_t len) throw (NetworkError);
    int write(const void *buffer, size_t len) throw (NetworkError);

    int readv(const iovec *vec, int len) throw (NetworkError);
    int writev(const iovec *vec, int len) throw (NetworkError);

    void flush() {}
    void close();
    int handle() { return *reinterpret_cast<int*>(socket_.get_handle()); }

    ACE_SOCK_Dgram &getSocket() { return socket_; }
    void setSocket(const ACE_SOCK_Dgram &socket) { socket_ = socket; }

    ACE_INET_Addr &getAddress() { return addr_; }
    void setAddress(const ACE_INET_Addr &addr) { addr_ = addr; }

  private:
    ACE_SOCK_Dgram socket_;
    ACE_INET_Addr addr_;
  };

}

#endif