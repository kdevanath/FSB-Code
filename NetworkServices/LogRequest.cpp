#include "LogRequest.h"
#include "TaskFactory.h"
#include "MsgHelper.h"

# if defined(_MSC_VER)
# ifndef _CRT_SECURE_NO_DEPRECATE
# define _CRT_SECURE_NO_DEPRECATE (1)
# endif
# pragma warning(disable : 4996)
# endif

namespace fsb
{
	using std::vector;
	using Yammer::operator <<;

	LogMARequest::LogMARequest()
	{
	}

	void LogMARequest::addMA(const LogMA& d)
	{
		_maInfos.push_back(d);
	}

	void LogMARequest::toStream(Stream& stream) const
    throw (NetworkError)
	{
		vector<char> buffer;
		
		buffer << type()
				<< _maInfos.size();

		for (LogMA::VectConstItr i = _maInfos.begin(); i != _maInfos.end(); ++i) {

			const LogMA& maInfo = (*i);
			buffer << maInfo._symbol
				<< maInfo._exchange
				<< maInfo._elapsedTime
				<< maInfo._ma1
				<< maInfo._ma2;
		}
	}


	void LogMARequest::fromStream(Stream& stream)
    throw (NetworkError)
	{
		int numMAInfos=0;
		stream >> numMAInfos;
		for(int i = 0; i<numMAInfos;i++ ) {
			stream >> _maInfo._symbol
				>> _maInfo._exchange
				>> _maInfo._elapsedTime
				>> _maInfo._ma1
				>> _maInfo._ma2;
			_maInfos.push_back(_maInfo);
		}
	}

	//********************************************************
	LogRequest::LogRequest()
	{
	}

	void LogRequest::addLogData(const RTDataLog& d)
	{
		_rtDataLogs.push_back(d);
	}

	void LogRequest::toStream(Stream& stream) const
    throw (NetworkError)
	{
		vector<char> buffer;
		
		buffer << type()
				<< _rtDataLogs.size();

		for (RTDataLog::VectConstItr i = _rtDataLogs.begin(); i != _rtDataLogs.end(); ++i) {

			const RTDataLog& logInfo = (*i);
			buffer << logInfo._symbol
				<< logInfo._exchange
				<< logInfo._timestamp
				<< logInfo._bid
				<< logInfo._bidSize
				<< logInfo._ask
				<< logInfo._askSize
				<< logInfo._last
				<< logInfo._volume
				<< logInfo._fvBid
				<< logInfo._fvAsk
				<< logInfo._ma
				<< logInfo._elapsedTime
				<< logInfo._buyPos
				<< logInfo._sellPos
				<< logInfo._avgBuyPrice
				<< logInfo._avgSellPrice
				<< logInfo._odId
				<< logInfo._orderId;
		}
	}


	void LogRequest::fromStream(Stream& stream)
    throw (NetworkError)
	{
		int numLogInfos=0;
		stream >> numLogInfos;
		for(int i = 0; i<numLogInfos;i++ ) {
			stream >> logInfo._symbol
				>> logInfo._exchange
				>> logInfo._timestamp
				>> logInfo._bid
				>> logInfo._bidSize
				>> logInfo._ask
				>> logInfo._askSize
				>> logInfo._last
				>> logInfo._volume
				>> logInfo._fvBid
				>> logInfo._fvAsk
				>> logInfo._ma
				>> logInfo._elapsedTime
				>> logInfo._buyPos
				>> logInfo._sellPos
				>> logInfo._avgBuyPrice
				>> logInfo._avgSellPrice
				>> logInfo._odId
				>> logInfo._orderId;
		}

	}

}

	

