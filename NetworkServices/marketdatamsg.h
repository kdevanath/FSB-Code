#ifndef __MARKET_DATA_MSG_H__
#define __MARKET_DATA_MSG_H__

#ifdef _MSC_VER
#pragma once
#endif

#include <iostream>
#include <vector>
#include "NetworkServicesExport.h"
#include <Data/RealTimeMsg.h>
#include <Utilities/FSBMsgsId.h>

#include <NetworkServices/Stream.h>
#include <NetworkServices/Task.h>
#include <NetworkServices/TaskSync.h>

using Yammer::Stream;
using Yammer::Task;
using Yammer::TaskSync;
using Yammer::NetworkError;
using namespace fsb;

namespace fsbfeeds {

//template <int=0>
//struct statics
//{
//	static int _incomingQuoteCount;
//	//static std::vector<fsb::Quote> _incomingQuotes;
//};
//
//template<int dummy> int statics<dummy>::_incomingQuoteCount;

class MYLIB_DLLAPI QuoteMessage : public Yammer::Task
{
public:
	QuoteMessage();
	QuoteMessage* clone() const { return new QuoteMessage(*this); }

	void run() { }

	int type() const { return fsb::QuoteMessageId; }
	 std::string name() const { return "QuoteMessage";}

	void toStream(Stream& stream) const throw (NetworkError);
    void fromStream(Stream& stream) throw (NetworkError);

    void sync(TaskSync *sync) { sync_ = sync; }
    bool needsReply() const { return false; }

	const int count() const { return _incomingQuoteCount; }
    void count(int i) { _incomingQuoteCount = i; }

	const int index() const {  return (_incomingQuoteCount != 0 ? _incomingQuoteCount - 1 : 0);}

	fsb::Quote _outdata;

	const fsb::Quote& getCurrentData() const { return _incomingQuotes[index()];}
	
	 static int _incomingQuoteCount;
	//void  setMarketdataQ(MarketdataQ* q) { _realTimeQ = q;}
private:
    TaskSync *sync_;
	//static MarketdataQ* _realTimeQ;
	  static std::vector<fsb::Quote> _incomingQuotes;
};

class MYLIB_DLLAPI TradeMessage : public Yammer::Task
{
public:
	TradeMessage();
	TradeMessage* clone() const { return new TradeMessage(*this); }

	void run() { }

	int type() const { return fsb::TradeMessageId; }
	 std::string name() const { return "TradeMessage";}

	void toStream(Stream& stream) const throw (NetworkError);
    void fromStream(Stream& stream) throw (NetworkError);

    void sync(TaskSync *sync) { sync_ = sync; }
    bool needsReply() const { return true; }

	const int count() const { return _incomingTradeCount; }
    void count(int i) { _incomingTradeCount = i; }

	const int index() const {return (_incomingTradeCount != 0 ? _incomingTradeCount - 1 : 0);}
	const fsb::Trade& getCurrentData() const { return _incomingTrades[index()];}

	fsb::Trade _outdata;

	  static int _incomingTradeCount;
private:
    TaskSync *sync_;

	  static std::vector<fsb::Trade> _incomingTrades;
	
};

};
#endif