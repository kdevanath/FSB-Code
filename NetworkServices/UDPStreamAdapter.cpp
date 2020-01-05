#include "UDPStreamAdapter.h"
#include <iostream>
namespace Yammer {

  UDPStreamAdapter::UDPStreamAdapter() {}

  UDPStreamAdapter::UDPStreamAdapter(const ACE_SOCK_Dgram &socket,
    const ACE_INET_Addr &addr,u_char ttl,u_char loopback) : socket_(socket), addr_(addr) 
  {
    
    if (socket_.ACE_SOCK::set_option(IPPROTO_IP, IP_MULTICAST_TTL, &ttl, sizeof(ttl)) == -1)
      std::cout << "Unable to set TTL option on socket!" << std::endl;

	if (socket_.ACE_SOCK::set_option(IPPROTO_IP, IP_MULTICAST_LOOP, &loopback, sizeof(loopback)) == -1)
      std::cout << "Unable to set loopback option on socket!" << std::endl;

	int paketsize=0;
	  int psz=sizeof(int);
	  int res = socket_.ACE_SOCK::get_option(SOL_SOCKET,SO_SNDBUF, (char *)&paketsize,&psz);
	  std::cout << " Result UDP " << paketsize << " " << psz <<  " " << res << std::endl;

	  std::cout << socket_.get_handle() << std::endl;
  }

  int UDPStreamAdapter::read(void *buffer, size_t len) throw (NetworkError)
  {
    int ret = socket_.recv(buffer, len, addr_);
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  int UDPStreamAdapter::write(const void *buffer, size_t len)
    throw (NetworkError)
  {  
    int ret = socket_.send(buffer, len, addr_);
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  int UDPStreamAdapter::readv(const iovec *vec, int len)
    throw (NetworkError)
  {
    // perform const cast due to ACE's non-standard interface
    iovec *v = const_cast<iovec*>(vec);
    int ret = socket_.recv(v, len, addr_);
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  int UDPStreamAdapter::writev(const iovec *vec, int len)
    throw (NetworkError)
  {
    int ret = socket_.send(vec, len, addr_);
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  void UDPStreamAdapter::close()
  {
    socket_.close();
  }

}