
#include "RelayServiceRequest.h"
#include "TaskFactory.h"
#include "MsgHelper.h"

namespace fsb {
  
    using Yammer::operator<<;

    RelayServiceRequest::RelayServiceRequest() : sync_(0), sendAll_(false),_relayId(-1) {}

    void RelayServiceRequest::toStream(Yammer::Stream &stream) const
      throw (Yammer::NetworkError) 
    {
      // build the message using a vector<char> as the buffer
      // be sure to send the type first
      std::vector<char> buffer;

      buffer << type()
	     << sendAll_
         << _token
		 << _relayId;

      // send the message
      stream << buffer;
    }

    void RelayServiceRequest::fromStream(Yammer::Stream &stream) throw (Yammer::NetworkError)
    {
      // read in values in the same order as sent excluding the type; the type
      // is read by the TaskFactory
      stream >> sendAll_;
      stream >> _token;
	  stream >> _relayId;
    }

    Yammer::TaskRegistrar<RelayServiceRequest> RelayServiceRequestPrototype;
}