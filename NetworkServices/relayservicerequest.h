#ifndef __RELAY_SERVICE_REQUEST_H__
#define __RELAY_SERVICE_REQUEST_H__

#include <NetworkServices/Task.h>
#include <NetworkServices/Stream.h>

#include <Utilities/FSBMsgsId.h>

namespace fsb {

    class MYLIB_DLLAPI RelayServiceRequest : public Yammer::Task {
    public:

      RelayServiceRequest();

      RelayServiceRequest *clone() const
      { return new RelayServiceRequest(*this); }

      void run() {}

      int type() const { return RelayServiceRequestId; }

      std::string name() const { return "RelayServiceRequest"; }

      void toStream(Yammer::Stream &stream) const throw (Yammer::NetworkError);

      void fromStream(Yammer::Stream &stream) throw (Yammer::NetworkError);

      bool sendAll_;
	  std::string _token;
	  int _relayId;

    private:

      Yammer::TaskSync *sync_;

    };
}

#endif