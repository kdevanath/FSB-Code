#ifndef _RELAY_HEADER_H_
#define _RELAY_HEADER_H_

#include <list> 
#include <utility> 
#include <string>

#include <NetworkServices/Task.h> 
#include <NetworkServices/TaskSync.h> 

#include <Utilities/FSBMsgsId.h>

namespace fsb {
 
    
    class MYLIB_DLLAPI RelayHeader : public Yammer::Task
    {
    public:

		typedef enum { SEND=0,RECV=1} ConnectionType;

      RelayHeader();

      RelayHeader* clone() const {return new RelayHeader(*this); }

      void run() {}

      int type() const { return RelayHeaderId; }
      std::string name() const { return "RelayHeader"; }

      void toStream(Yammer::Stream &stream) const throw (Yammer::NetworkError);
      void fromStream(Yammer::Stream &stream) throw (Yammer::NetworkError);

      void sync(Yammer::TaskSync *sync) { sync_ = sync; }
      bool needsReply() const { return false; }

	  std::string token() const {return _token;}
	  void token(const std::string& i) {_token = i;}

	  int relayId() const { return _relayId;}
	  void relayId(int r) { _relayId = r;}

    private:
		ConnectionType _connectionType;
		std::string _token;
		int _relayId;

      Yammer::TaskSync *sync_;
    };

}

#endif