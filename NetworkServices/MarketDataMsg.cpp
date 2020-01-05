#include "MarketDataMsg.h"
#include "NetworkServices/TaskFactory.h"
#include "NetworkServices/MsgHelper.h"

namespace fsbfeeds
{	
	int fsbfeeds::QuoteMessage::_incomingQuoteCount = 0;
	std::vector<fsb::Quote> fsbfeeds::QuoteMessage::_incomingQuotes(1000);
	//fsb::MarketdataQ* fsbfeeds::QuoteMessage::_realTimeQ = (fsb::MarketdataQ*)0;

	using std::vector;
	using Yammer::operator <<;

	QuoteMessage::QuoteMessage() {}

	void QuoteMessage::toStream(Stream& stream) const
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
			<< _outdata._bid._price
			<< _outdata._bid._size
			<< _outdata._ask._price
			<< _outdata._ask._size;
		stream << buffer;
		/*char tmpbuf[10];
			_strtime_s( tmpbuf, 10 );
			std::cout <<  _outdata._symbol
			<< " " << _outdata._exchange
			<< " " << _outdata._bid._price
			<< " " << _outdata._ask._price
			<< " " << tmpbuf
			<< " " << buffer.size()
			<< std::endl;*/
		//buffer.clear();

		/*if( _outdata._symbol == "C_GASM09" ||
			_outdata._symbol == "C_BRNM09" )
		{
			char tmpbuf[10];
			_strtime_s( tmpbuf, 10 );
			std::cout <<  _outdata._symbol
			<< " " << _outdata._exchange
			<< " " << _outdata._bid._price
			<< " " << _outdata._ask._price
			<< " " << tmpbuf
			<< std::endl;
		}*/
	}

	void QuoteMessage::fromStream(Stream& stream) throw (NetworkError)
	{
		if( _incomingQuoteCount == 999) _incomingQuoteCount = 0;

		stream     >> _incomingQuotes[_incomingQuoteCount]._messageType;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._timestamp;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._symbol;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._exchange;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._feed;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._instrType;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._bid._price;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._bid._size;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._ask._price;
		stream	   >> _incomingQuotes[_incomingQuoteCount]._ask._size;
		/*char tmpbuf[10];
		_strtime_s( tmpbuf, 10 );
		std::cout << std::endl;
		std::cout << "MDM: " << _incomingQuotes[_incomingQuoteCount]._symbol
			<< " " << _incomingQuotes[_incomingQuoteCount]._exchange
			<< " " << _incomingQuotes[_incomingQuoteCount]._bid._price
			<< " " << _incomingQuotes[_incomingQuoteCount]._ask._price
			<< " " << _incomingQuoteCount
			<< " " << tmpbuf
			<< std::endl;
		if( _incomingQuotes[_incomingQuoteCount]._symbol == "C_GASM09" ||
			_incomingQuotes[_incomingQuoteCount]._symbol == "C_BRNN09" ) {
				static double gasbid, gasask, prevBid,prevAsk = 0.0;
			char tmpbuf[10];
			_strtime_s( tmpbuf, 10 );
			if( _incomingQuotes[_incomingQuoteCount]._symbol == "C_BRNN09" && 
				(fabs(_incomingQuotes[_incomingQuoteCount]._bid._price - prevBid ) >
				0.1*_incomingQuotes[_incomingQuoteCount]._bid._price ||
				fabs(_incomingQuotes[_incomingQuoteCount]._ask._price - prevAsk ) >
				0.1*_incomingQuotes[_incomingQuoteCount]._ask._price )) {
		std::cout << " ERROR/MDM: " << _incomingQuotes[_incomingQuoteCount]._symbol
			<< " " << _incomingQuotes[_incomingQuoteCount]._exchange
			<< " " << _incomingQuotes[_incomingQuoteCount]._bid._price
			<< " " << _incomingQuotes[_incomingQuoteCount]._ask._price
			<< " " << _incomingQuoteCount
			<< " " << tmpbuf
			<< std::endl;
		prevBid = _incomingQuotes[_incomingQuoteCount]._bid._price;
		prevAsk= _incomingQuotes[_incomingQuoteCount]._ask._price;
			}
			else if( _incomingQuotes[_incomingQuoteCount]._symbol == "C_GASM09" && (fabs(_incomingQuotes[_incomingQuoteCount]._bid._price - gasbid ) >
				0.1*_incomingQuotes[_incomingQuoteCount]._bid._price ||
				fabs(_incomingQuotes[_incomingQuoteCount]._ask._price - gasask ) >
				0.1*_incomingQuotes[_incomingQuoteCount]._ask._price )) {
					std::cout << "ERROR/MDM: " << _incomingQuotes[_incomingQuoteCount]._symbol
			<< " " << _incomingQuotes[_incomingQuoteCount]._exchange
			<< " " << _incomingQuotes[_incomingQuoteCount]._bid._price
			<< " " << _incomingQuotes[_incomingQuoteCount]._ask._price
			<< " " << _incomingQuoteCount
			<< " " << tmpbuf
			<< std::endl;
		gasbid = _incomingQuotes[_incomingQuoteCount]._bid._price;
		gasask= _incomingQuotes[_incomingQuoteCount]._ask._price;
			}
		}*/
		_incomingQuoteCount++;
	}
//	Yammer::TaskRegistrar<fsbfeeds::QuoteMessage> QuoteMessagePrototype;
}
REGISTER_TASK(fsbfeeds::QuoteMessage)