#include "TCPStreamAdapter.h" 
#include <iostream>
namespace Yammer {

  TCPStreamAdapter::TCPStreamAdapter() {}

  TCPStreamAdapter::TCPStreamAdapter(const ACE_SOCK_Stream &stream)
    : stream_(stream)
  {
	  int nodelay = 1;
	  if (stream.set_option(ACE_IPPROTO_TCP, TCP_NODELAY, (void*)&nodelay, sizeof(nodelay)) == -1)
		  std::cout << "Unable to set socket TCP_NODELAY option!" << std::endl;
	  int rcvBufsize=0;
	  int psz=sizeof(int);
	  int result = stream.get_option(SOL_SOCKET,SO_RCVBUF,(void*)&rcvBufsize,&psz);
	  std::cout << " result/TCP "<< result << " " << rcvBufsize << std::endl;

  }

  int TCPStreamAdapter::read(void *buffer, size_t len) throw (NetworkError)
  {
	  //std::cout << " TCP before len: " << len << std::endl;
    int ret = stream_.recv(buffer, len);
	//std::cout << " TCP after len : " << len << std::endl;
    if (ret == 0)
      throw PeerClosed();
    else if (ret == -1)
      if (errno == ECONNRESET)
        throw PeerClosed();
      else
        throw SocketError();

    return ret;
  }

  int TCPStreamAdapter::write(const void *buffer, size_t len)
    throw (NetworkError)
  {
    int ret = stream_.send_n(buffer, len);
    if (ret == -1)
      if (errno == EPIPE)
        throw BrokenPipe();
      else
        throw SocketError();

    return ret;
  }

  int TCPStreamAdapter::readv(const iovec *vec, int len)
    throw (NetworkError)
  {
    // perform const cast due to ACE's non-standard interface
    iovec *v = const_cast<iovec*>(vec);
    int ret = stream_.recvv_n(v, len);  // scatter read
    if (ret == 0)
      throw PeerClosed();
    else if (ret == -1)
      if (errno == ECONNRESET)
        throw PeerClosed();
      else
        throw SocketError();

    return ret;
  }
  
  // this ultimately calls the writev system call
  // note: Linux has a maximum iovec length of 1024, for Solaris, 16
  int TCPStreamAdapter::writev(const iovec *vec, int len)
    throw (NetworkError)
  {
    int ret = stream_.sendv_n(vec, len);  // gather write
    if (ret == -1)
      if (errno == EPIPE)
        throw BrokenPipe();
      else
        throw SocketError();

    return ret;
  }

  void TCPStreamAdapter::close()
  {
    stream_.close();
  }

}