#include "OrderReplyClient.h"

#include <Utilities/FSBLogger.h>

#include "MsgHelper.h"
#include "TaskFactory.h"

namespace fsb {

    using Yammer::operator<<;

    OrderReplyClient::OrderReplyClient() : sync_(0) {}

    void OrderReplyClient::run() 
    {
    }

    void OrderReplyClient::toStream(Yammer::Stream &stream) const
      throw (Yammer::NetworkError) 
    {

    }

	void OrderReplyClient::fromStream(Yammer::Stream &stream) throw (Yammer::NetworkError)
	{
		// read in values in the same order as sent excluding the type; the type
		// is read by the TaskFactory

		int numOrders = 0;
		fsb::OrderReplyInfo replyInfo;

		stream >> numOrders;

		for (int i=0; i < numOrders; i++) {

			stream >> replyInfo._symbol
				>> replyInfo._exchange
				>> replyInfo._odId
				>> replyInfo._orderStatus
				>> replyInfo._extOrderId
				>> replyInfo._orderSource
				>> replyInfo._memo
				>> replyInfo._executionId
				>> replyInfo._qtyFilled
				>> replyInfo._fillPrice
				>> replyInfo._transactTime
				>> replyInfo._replay
				>> replyInfo._counterParty;

			addOrder(replyInfo);
		} 
	}

	void OrderReplyClient::addOrder(const fsb::OrderReplyInfo& replyInfo)
    {
      orderInfos_.push_back(replyInfo);
    }

	Yammer::TaskRegistrar<fsb::OrderReplyClient> OrderReplyClientPrototype;
}