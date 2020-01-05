#include "RelayHeader.h"

#include "MsgHelper.h"
#include "TaskFactory.h"

namespace fsb {
  
    using Yammer::operator<<;

    RelayHeader::RelayHeader() : sync_(0),_relayId(-1) {}

    void RelayHeader::toStream(Yammer::Stream &stream) const
      throw (Yammer::NetworkError) 
    {
      // build the message using a vector<char> as the buffer
      // be sure to send the type first
      std::vector<char> buffer;

      buffer << type()
	     << _token
		 << _relayId;

      // send the message
      stream << buffer;
    }

    void RelayHeader::fromStream(Yammer::Stream &stream) throw (Yammer::NetworkError)
    {
      // read in values in the same order as sent excluding the type; the type
      // is read by the TaskFactory
      std::cout << "fromStream " << std::endl;

      stream >> _token;
	  stream >> _relayId;

    }

    Yammer::TaskRegistrar<RelayHeader> RelayHeaderPrototype;

}