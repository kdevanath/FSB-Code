#include "MulticastStreamAdapter.h"
#include <iostream>

namespace Yammer {

  MulticastStreamAdapter::MulticastStreamAdapter() {}

  MulticastStreamAdapter::MulticastStreamAdapter(const ACE_SOCK_Dgram_Mcast
    &socket, const ACE_INET_Addr &addr, u_char ttl,u_char loopback) 
	: socket_(socket),
    addr_(addr)
  {
	  int paketsize=0;
	  int psz=sizeof(int);
	  int res = socket_.ACE_SOCK::get_option(SOL_SOCKET,SO_RCVBUF,&paketsize,&psz);
	  std::cout << " Result " << paketsize << " " << psz << "  " << res << std::endl;
	  /*if (socket_.ACE_SOCK::set_option(IPPROTO_IP, IP_MULTICAST_TTL, &ttl, sizeof(ttl)) == -1)
      std::cout << "Unable to set TTL option on socket!" << std::endl;

	if (socket_.ACE_SOCK::set_option(IPPROTO_IP, IP_MULTICAST_LOOP, &loopback, sizeof(loopback)) == -1)
      std::cout << "Unable to set loopback option on socket!" << std::endl;*/

  }

  int MulticastStreamAdapter::read(void *buffer, size_t len)
    throw (NetworkError)
  {
	 // std::cout << " trying to recv " << std::endl;
    int ret = socket_.recv(buffer, len, addr_);
	std::string msg(reinterpret_cast<char*>(buffer));	 
	//std::cout << __FILE__ << " " << ret << " " << msg << std::endl;
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  int MulticastStreamAdapter::write(const void *buffer, size_t len)
    throw (NetworkError)
  {
    int ret = socket_.send(buffer, len);
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  int MulticastStreamAdapter::readv(const iovec *vec, int len)
    throw (NetworkError)
  {
    // perform const cast due to ACE's non-standard interface
    iovec *v = const_cast<iovec*>(vec);
    int ret = socket_.recv(v, len, addr_);
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  int MulticastStreamAdapter::writev(const iovec *vec, int len)
    throw (NetworkError)
  {
    int ret = socket_.send(vec, len);
    if (ret == -1)
      throw SocketError();

    return ret;
  }

  void MulticastStreamAdapter::close()
  {
    socket_.close();
  }

}