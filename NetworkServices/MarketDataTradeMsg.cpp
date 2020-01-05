#include "MarketDataMsg.h"
#include "NetworkServices/TaskFactory.h"
#include "NetworkServices/MsgHelper.h"

int fsbfeeds::TradeMessage::_incomingTradeCount = 0;
std::vector<fsb::Trade> fsbfeeds::TradeMessage::_incomingTrades(1000);

namespace fsbfeeds
{	
	using std::vector;
	using Yammer::operator <<;

	TradeMessage::TradeMessage() { }

	void TradeMessage::toStream(Stream& stream) const
    throw (NetworkError)
	{
		vector<char> buffer;
		
		buffer << type()
			<< _outdata._messageType
			<< _outdata._timestamp
			<< _outdata._symbol
			<< _outdata._exchange
			<< _outdata._feed
			<< _outdata._instrType
			<< _outdata._trade._price
			<< _outdata._trade._size
			<< _outdata._volume;

		stream << buffer;
	}

	void TradeMessage::fromStream(Stream& stream) throw (NetworkError)
	{
		if(_incomingTradeCount == 999) _incomingTradeCount = 0;
		stream	   >> _incomingTrades[_incomingTradeCount]._messageType;
		stream	   >> _incomingTrades[_incomingTradeCount]._timestamp;
		stream	   >> _incomingTrades[_incomingTradeCount]._symbol;
		stream	   >> _incomingTrades[_incomingTradeCount]._exchange;
		stream	   >> _incomingTrades[_incomingTradeCount]._feed;
		stream	   >> _incomingTrades[_incomingTradeCount]._instrType;
		stream	   >> _incomingTrades[_incomingTradeCount]._trade._price;
		stream	   >> _incomingTrades[_incomingTradeCount]._trade._size;
		stream	   >> _incomingTrades[_incomingTradeCount]._volume;
		_incomingTradeCount++;		
	}

	Yammer::TaskRegistrar<TradeMessage> TradeMessagePrototype;
}